using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Timeline : MonoBehaviour
{
    public readonly float TimingOffset = 0.75f;
        
    [SerializeField] 
    protected BeatKeeper _BeatKeeper;
    
    [FormerlySerializedAs("_ButtonPrefab")] [SerializeField]
    protected GameObject _EnemyPrefab;
    
    [SerializeField]
    protected Transform _HitPosition;
    
    [SerializeField]
    protected Transform _StartPosition;
    
    [SerializeField]
    protected PlayerCharacter _PlayerCharacter;
    
    [SerializeField]
    protected ProgressBar _ProgressBar;

    protected List<Enemy> ActiveEnemies;
    protected List<Bar> Stage;
    protected bool IsPlaying;
    protected int HitCount;
    protected float LengthBetweenCrotchetSeg;
    protected float LengthBetweenEightsSeg;
    protected float PrevValue;
    protected float Clock;
    protected float InputTimeout;
    protected bool Stabbed;
    
    private void Start()
    {
        ActiveEnemies = new List<Enemy>();
        Stage = new List<Bar>();
        
        var dist = Vector3.Distance(_StartPosition.position, _HitPosition.position);
        LengthBetweenCrotchetSeg = dist / 4.0f;
        LengthBetweenEightsSeg = dist / 8.0f;

        _BeatKeeper.NextBar += OnBarChanged;
        _BeatKeeper.StageEnded += Stop;
        _BeatKeeper.QuaverUpdate += EightUpdate;
        GameManager.Instance.OnInput += PlayerInput;
    }

    private void OnDestroy()
    {
        _BeatKeeper.NextBar -= OnBarChanged;
        _BeatKeeper.StageEnded -= Stop;
        _BeatKeeper.QuaverUpdate -= EightUpdate;
        GameManager.Instance.OnInput -= PlayerInput;
    }

    private void LateUpdate()
    {
        if (!IsPlaying)
        {
            return;
        }
        
        Clock = PrevValue == _BeatKeeper.SongPosition
            ? Clock + Time.unscaledDeltaTime
            : _BeatKeeper.SongPosition;
        
        var fourthTime = (Clock - ( (_BeatKeeper.CurrentCrotchetHit) * _BeatKeeper.Crotchet)) / _BeatKeeper.Crotchet;
        var eightTime = (Clock - ( (_BeatKeeper.CurrentEigthHit) * (_BeatKeeper.Crotchet / 2) )) / (_BeatKeeper.Crotchet/2);
        var fourthBeat = _BeatKeeper.CurrentCrotchetHit % 4;
        var eightBeat = _BeatKeeper.CurrentEigthHit % 8;
        
        if(_BeatKeeper.CurrentBar < _BeatKeeper.MaxBars)
        {
            Bar bar = Stage[_BeatKeeper.CurrentBar];
            for (int i = 0; i < bar.Enemies.Count; i++)
            {
                var enemy = bar.Enemies[i];

                if (enemy.Crotchet && fourthBeat == enemy.BeatPosition && !enemy.Active ||
                    !enemy.Crotchet && eightBeat == enemy.BeatPosition && !enemy.Active)
                {
                    enemy.Active = true;
                    enemy.Object.SetActive(true);
                    enemy.InternalPos = enemy.Crotchet ? _BeatKeeper.CurrentCrotchetHit : _BeatKeeper.CurrentEigthHit;
                    ActiveEnemies.Add(enemy);
                }
            }
        }
        
        for(int i = ActiveEnemies.Count - 1; i >= 0; i--)
        {
            var enemy = ActiveEnemies[i];
            var enemyObject = enemy.Object;
            var pos = enemyObject.transform.position;

            if (!enemy.Crotchet)
            {
                var beatPos = (_BeatKeeper.CurrentEigthHit - enemy.InternalPos);
                pos.x = Mathf.Lerp(_StartPosition.position.x - LengthBetweenEightsSeg * beatPos, _StartPosition.position.x - LengthBetweenEightsSeg * (beatPos + 1), eightTime);
            }
            else
            {
                
                var beatPos = (_BeatKeeper.CurrentCrotchetHit - enemy.InternalPos);
                pos.x = Mathf.Lerp(_StartPosition.position.x - LengthBetweenCrotchetSeg * beatPos, _StartPosition.position.x - LengthBetweenCrotchetSeg * (beatPos + 1), fourthTime);
            }
            
            enemyObject.transform.position = pos;
            if (pos.x <= _HitPosition.position.x - TimingOffset)
            {
                ActiveEnemies.RemoveAt(i);
                Destroy(enemyObject);
                GameManager.Instance.PlayerGotHit();
            }
        }
        
        _ProgressBar.UpdatePosition(_BeatKeeper.SongPosition);
        PrevValue = _BeatKeeper.SongPosition;
        InputTimeout -= Time.deltaTime;
    }

    private void EightUpdate(int beat)
    {
        // _PlayerCharacter.EightTickUpate(beat);
    }

    public void Play(List<Bar> stage)
    {
        _ProgressBar.Init(_BeatKeeper.MaxBars * _BeatKeeper.Crotchet * 4);
        this.Stage = stage;
        PrepBar(Stage[0]);
        PrepBar(Stage[1]);
        IsPlaying = true;
    }

    public void Stop()
    {
        IsPlaying = false;
        _ProgressBar.Stop();
        CleanUp();
    }

    protected void CleanUp()
    {
        for (int i = ActiveEnemies.Count - 1; i >= 0; i--)
        {
            var enemy = ActiveEnemies[i];
            ActiveEnemies.RemoveAt(i);
            Destroy(enemy.Object);
        }

        Stage = null;
    }
    
    protected void OnBarChanged(int bar)
    {
        if (bar < Stage.Count - 1)
        {
            PrepBar(Stage[bar + 1]);
        }
    }

    protected void PlayerInput(InputType input)
    {
        if (ActiveEnemies.Count <= 0 || InputTimeout > 0)
        {
            return;    
        }

        if (!IsEnemyInRange(ActiveEnemies[0]) || input != ActiveEnemies[0].RequiredInput)
        {
            Debug.Log("TODO:: Need to figure out penalty");
            InputTimeout = 0.66f;
            _PlayerCharacter.Stab(0.66f);
            return;
        }
        
        GameManager.TriggerImpulse();
        Stabbed = true;
        _PlayerCharacter.Stab(0.22f);
        Destroy(ActiveEnemies[0].Object);
        ActiveEnemies.RemoveAt(0);
    }

    protected bool IsEnemyInRange(Enemy enemy)
    {
        var position = enemy.Object.transform.position;
        return _HitPosition.position.x - TimingOffset <= position.x && position.x <= _HitPosition.position.x + TimingOffset;
    }
    
    protected void PrepBar(Bar bar)
    {
        foreach (var enemy in bar.Enemies)
        {
            enemy.Object = GameObject.Instantiate(_EnemyPrefab, this.transform);
            enemy.Object.transform.position = _StartPosition.position;
            enemy.Object.GetComponent<EnemyWorldObject>().Init(enemy);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Timeline : MonoBehaviour
{
    public readonly float TimingOffset = 1.35f;
        
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
                    enemy.Object.gameObject.SetActive(true);
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
                _PlayerCharacter.Stab(false);
                ActiveEnemies.RemoveAt(i);
                enemyObject.AttackAnimation();
                GameManager.Instance.PlayerGotHit();
                InputTimeout = 0;
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
    }
    
    protected void OnBarChanged(int bar)
    {
        if (bar > Stage.Count)
        {
            GameManager.Instance.OnSongFinished?.Invoke();
            return;
        }
        
        if (bar < Stage.Count - 1)
        {
            var selectBat = Stage[bar + 1];
            PrepBar(selectBat);
        }
    }

    private void PlayerInput(InputType input)
    {
        if (ActiveEnemies.Count <= 0 || InputTimeout > 0 || input != InputType.ANY)
        {
            return;    
        }

        if (!IsEnemyInRange(ActiveEnemies[0]))
        {
            InputTimeout = 0.75f;
            _PlayerCharacter.Stab(true);
            _PlayerCharacter.Lockout(0.75f);    
            return;
        }
        
        GameManager.Instance.OnPlayerStab?.Invoke();
        GameManager.TriggerImpulse();
        _PlayerCharacter.Stab(false);
        ActiveEnemies[0].Object.Death();
        ActiveEnemies.RemoveAt(0);
    }

    protected bool IsEnemyInRange(Enemy enemy)
    {
        var position = enemy.Object.transform.position;
        return _HitPosition.position.x - TimingOffset <= position.x && position.x <= _HitPosition.position.x + (TimingOffset/4);
    }
    
    protected void PrepBar(Bar bar)
    {
        foreach (var enemy in bar.Enemies)
        {
            enemy.Object = Instantiate(_EnemyPrefab, transform).GetComponent<EnemyWorldObject>();
            enemy.Object.transform.position = _StartPosition.position;
            enemy.Object.Init(enemy);
        }
    }
}

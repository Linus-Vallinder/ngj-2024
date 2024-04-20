using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Timeline : MonoBehaviour
{
    [SerializeField] 
    protected BeatKeeper _BeatKeeper;
    
    [FormerlySerializedAs("_ButtonPrefab")] [SerializeField]
    protected GameObject _EnemyPrefab;
    
    [SerializeField]
    protected Transform _HitPosition;
    
    [SerializeField]
    protected Transform _StartPosition;

    protected List<Enemy> ActiveEnemies;
    protected List<Bar> Stage;
    protected bool IsPlaying;
    protected int HitCount;
    protected float LengthBetweenCrotchetSeg;
    protected float LengthBetweenEightsSeg;
    protected float PrevValue;
    protected float Clock;
    
    private void Start()
    {
        ActiveEnemies = new List<Enemy>();
        Stage = new List<Bar>();
        
        var dist = Vector3.Distance(_StartPosition.position, _HitPosition.position);
        LengthBetweenCrotchetSeg = dist / 4.0f;
        LengthBetweenEightsSeg = dist / 8.0f;

        _BeatKeeper.NextBar += OnBarChanged;
        _BeatKeeper.StageEnded += Stop;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _BeatKeeper.MaxBars = 16;
            _BeatKeeper.Play();
            Play(Bar.GetRandomStage(16));
        }
        
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
            if (pos.x <= _HitPosition.position.x - 0.2f)
            {
                Debug.Log("Player get poked :OOOO");
                ActiveEnemies.RemoveAt(i);
                Destroy(enemyObject);
            }
        }

        PrevValue = _BeatKeeper.SongPosition;
    }

    public void Play(List<Bar> stage)
    {
        this.Stage = stage;
        PrepBar(Stage[0]);
        PrepBar(Stage[1]);
        IsPlaying = true;
    }

    public void Stop()
    {
        IsPlaying = false;
    }
    
    protected void OnBarChanged(int bar)
    {
        if (bar < Stage.Count - 1)
        {
            PrepBar(Stage[bar + 1]);
        }
    }

    protected void PrepBar(Bar bar)
    {
        foreach (var enemy in bar.Enemies)
        {
            enemy.Object = GameObject.Instantiate(_EnemyPrefab, this.transform);
            enemy.Object.transform.position = _StartPosition.position;
        }
    }
}

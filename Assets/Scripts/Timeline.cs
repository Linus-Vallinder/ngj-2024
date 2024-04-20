using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

    protected List<GameObject> Buttons;
    protected List<Enemy> Pattern;
    protected bool IsPlaying;
    protected int HitCount;
    protected float LengthBetweenCrotchetSeg;
    protected float LengthBetweenEightsSeg;
    protected float PrevValue;
    protected float Clock;
    
    private void Start()
    {
        Buttons = new List<GameObject>();
        Pattern = new List<Enemy>();

        var dist = Vector3.Distance(_StartPosition.position, _HitPosition.position);
        LengthBetweenCrotchetSeg = dist / 4.0f;
        LengthBetweenEightsSeg = dist / 8.0f;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _BeatKeeper.Play();
            Play();
        }
        
        if (!IsPlaying)
        {
            return;
        }

        Clock = PrevValue == _BeatKeeper.SongPosition
            ? Clock + Time.unscaledDeltaTime
            : _BeatKeeper.SongPosition;
        
        for (int i = 0; i < 4; i++)
        {
            var t = Pattern[i];
            var fourthBeat = _BeatKeeper.CurrentCrotchetHit % 4;
            var eightBeat = _BeatKeeper.CurrentEigthHit % 8;
            var fourthTime = (Clock - ( (_BeatKeeper.CurrentCrotchetHit) * _BeatKeeper.Crotchet)) / _BeatKeeper.Crotchet;
            var eightTime = (Clock - ( (_BeatKeeper.CurrentEigthHit) * (_BeatKeeper.Crotchet / 2) )) / (_BeatKeeper.Crotchet/2);
            var enemy = t.Object;
            
            if (t.Crotchet && fourthBeat == t.BeatPosition || enemy.activeInHierarchy)
            {
                t.InternalPos = (_BeatKeeper.CurrentCrotchetHit - t.BeatPosition) % (4 + 1);
                var pos = enemy.transform.position;
                pos.x = Mathf.Lerp(_StartPosition.position.x - LengthBetweenCrotchetSeg * t.InternalPos, _StartPosition.position.x - LengthBetweenCrotchetSeg * (t.InternalPos + 1), fourthTime);
                enemy.transform.position = pos;
                enemy.SetActive(true);

                if (pos.x <= _HitPosition.position.x - 0.2f)
                {
                    Debug.Log("Player get poked :OOOO");
                    enemy.SetActive(false);
                }
            }

            if (!t.Crotchet && eightBeat == t.BeatPosition || enemy.activeInHierarchy)
            {
                t.InternalPos = (_BeatKeeper.CurrentEigthHit - t.BeatPosition) % (8 + 1);
                var pos = enemy.transform.position;
                pos.x = Mathf.Lerp(_StartPosition.position.x - LengthBetweenEightsSeg * t.InternalPos, _StartPosition.position.x - LengthBetweenEightsSeg * (t.InternalPos + 1), eightTime);
                enemy.transform.position = pos;
                enemy.SetActive(true);

                if (pos.x <= _HitPosition.position.x - 0.2f)
                {
                    Debug.Log("Player get poked :OOOO");
                    enemy.SetActive(false);
                }
            }
        }

        PrevValue = _BeatKeeper.SongPosition;
    }

    public void Play()
    {
        IsPlaying = true;
    }
}

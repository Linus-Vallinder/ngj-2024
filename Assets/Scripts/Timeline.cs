using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Timeline : MonoBehaviour
{
    [SerializeField] 
    protected BeatKeeper _BeatKeeper;
    
    [SerializeField]
    protected GameObject _ButtonPrefab;
    
    [SerializeField]
    protected Transform _HitPosition;
    
    [SerializeField]
    protected Transform _StartPosition;

    protected List<GameObject> Buttons;
    protected List<Thing> Pattern;
    protected bool IsPlaying;
    protected int HitCount;
    protected float LengthBetweenCrotchetSeg;
    protected float LengthBetweenEightsSeg;
    protected float Length;
    protected float PrevValue;
    protected float Clock;
    
    private void Start()
    {
        Buttons = new List<GameObject>();
        Pattern = new List<Thing>();

        var dist = Vector3.Distance(_StartPosition.position, _HitPosition.position);
        LengthBetweenCrotchetSeg = dist / 4.0f;
        LengthBetweenEightsSeg = dist / 8.0f;
        Length = Mathf.Abs(_StartPosition.position.y - _HitPosition.position.y);

        for (int i = 0; i < 8; i++)
        {
            var btn = GameObject.Instantiate(_ButtonPrefab, this.transform);
            btn.transform.position = _StartPosition.position + new Vector3(0, LengthBetweenCrotchetSeg, 0) * (i +1);
            Buttons.Add(btn);
        }

        Thing thing = new Thing();
        thing.Crotchet = true;
        thing.BeatPosition = 0;
        thing.Button = Buttons[0];
        Pattern.Add(thing);
        
        thing = new Thing();
        thing.BeatPosition = 4;
        thing.Button = Buttons[1];
        Pattern.Add(thing);
        
        thing = new Thing();
        thing.BeatPosition = 5;
        thing.Button = Buttons[2];
        Pattern.Add(thing);
        
        thing = new Thing();
        thing.Crotchet = true;
        thing.BeatPosition = 2;
        thing.Button = Buttons[3];
        Pattern.Add(thing);
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
            var fourthTime = (Clock - ((_BeatKeeper.CurrentCrotchetHit) * _BeatKeeper.Crotchet) ) / _BeatKeeper.Crotchet;
            var eightTime = (Clock - ((_BeatKeeper.CurrentEigthHit) * (_BeatKeeper.Crotchet / 2)) ) / (_BeatKeeper.Crotchet/2);
            var btn = t.Button;
            
            if (t.Crotchet && fourthBeat == t.BeatPosition || btn.activeInHierarchy)
            {
                t.InternalPos = (_BeatKeeper.CurrentCrotchetHit - t.BeatPosition) % 4;
                var pos = btn.transform.position;
                pos.y = Mathf.Lerp(_StartPosition.position.y - LengthBetweenCrotchetSeg * t.InternalPos, _StartPosition.position.y - LengthBetweenCrotchetSeg * (t.InternalPos + 1), fourthTime);
                btn.transform.position = pos;
                btn.SetActive(true);

                if (pos.y == _HitPosition.position.y)
                {
                    btn.SetActive(false);
                }
            }

            if (!t.Crotchet && eightBeat == t.BeatPosition || btn.activeInHierarchy)
            {
                t.InternalPos = (_BeatKeeper.CurrentEigthHit - t.BeatPosition) % 8;
                var pos = btn.transform.position;
                pos.y = Mathf.Lerp(_StartPosition.position.y - LengthBetweenEightsSeg * t.InternalPos, _StartPosition.position.y - LengthBetweenEightsSeg * (t.InternalPos + 1), eightTime);
                btn.transform.position = pos;
                btn.SetActive(true);
                
                
                if (pos.y == _HitPosition.position.y)
                {
                    btn.SetActive(false);
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

public struct Thing
{
    public GameObject Button;
    public int BeatPosition;
    public bool Crotchet;
    public int InternalPos;
}
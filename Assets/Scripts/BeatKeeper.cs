using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BeatKeeper : MonoBehaviour
{
    public event Action StageEnded;
    public event Action<int> QuaverUpdate;
    public event Action<int> NextBar;

    [SerializeField]
    protected MusicController _MainSource;

    protected double _SongDSPTime;
    protected bool IsPlaying;

    public int BarLength = 4;
    public float BPM;
    public float Offset;
    public int CurrentCrotchetHit;
    public int CurrentEigthHit;
    public int CurrentBar;
    public int MaxBars;
    public float SongPosition;

    public float Crotchet
    {
        get
        {
            return 60.0f / BPM;
        }
    }

    public float Quaver
    {
        get
        {
            return Crotchet / 2;
        }
    }

    public void Play()
    {
        _SongDSPTime = AudioSettings.dspTime;
        _MainSource.Play();

        CurrentBar = 0;
        CurrentCrotchetHit = 0;
        CurrentEigthHit = 0;
        IsPlaying = true;
    }

    public void Stop()
    {
        //TODO::Fade out song here
        IsPlaying = false;
        
        _MainSource.Stop();
        StageEnded?.Invoke();
    }
    
    void Update()
    {
        if (!IsPlaying)
        {
            return;
        }

        SongPosition = (float)((AudioSettings.dspTime - _SongDSPTime) * 1/*_MainSource.pitch*/ - Offset);
        if (SongPosition > (CurrentCrotchetHit + 1) * Crotchet)
        {
            CurrentCrotchetHit += 1;

            if (CurrentCrotchetHit > 4 && CurrentCrotchetHit % 4 == 1)
            {
                CurrentBar += 1;

                if (CurrentBar > MaxBars)
                {
                    Stop();
                }
                
                NextBar?.Invoke(CurrentBar);
            }
        }
        
        if (SongPosition > (CurrentEigthHit + 1) * (Quaver))
        {
            CurrentEigthHit += 1;
            QuaverUpdate?.Invoke(CurrentEigthHit);
        }
        
        _MainSource.Tick(CurrentCrotchetHit);
    }
}

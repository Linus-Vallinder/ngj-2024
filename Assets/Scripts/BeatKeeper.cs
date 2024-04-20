using UnityEngine;
using UnityEngine.Serialization;

public class BeatKeeper : MonoBehaviour
{
    [SerializeField]
    protected AudioSource _MainSource;

    protected double _SongDSPTime;
    protected bool IsPlaying;

    public int BarLength = 4;
    public float BPM;
    public float Offset;
    public int CurrentCrotchetHit;
    public int CurrentEigthHit;
    public int CurrentBar;

    public float SongPosition;

    public float Crotchet
    {
        get
        {
            return 60.0f / BPM;
        }
    }

    public int BarAmount
    {
        get
        {
            return (int)(_MainSource.clip.length / (BarLength * Crotchet));
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
    
    
    //Shit thing, change for better input handling
    void Update()
    {
        if (!IsPlaying)
        {
            return;
        }

        SongPosition = (float)((AudioSettings.dspTime - _SongDSPTime) * _MainSource.pitch - Offset);
        if (SongPosition > (CurrentCrotchetHit + 1) * Crotchet)
        {
            CurrentCrotchetHit += 1;

            if (CurrentCrotchetHit > 4 && CurrentCrotchetHit % 4 == 1)
            {
                CurrentBar += 1;
            }
        }
        
        if (SongPosition > (CurrentEigthHit + 1) * (Crotchet/2))
        {
            CurrentEigthHit += 1;
        }
    }
}

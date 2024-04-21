using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    protected List<AudioSource> AudioTrcks;
    
    [Range(1,5)]
    public int Intensity;

    public int IntensityThreshold = 16;
    public TweenerCore<float, float, FloatOptions> TweenInProgress;

    private void Awake()
    {
        Play();
        Stop();
    }

    public void Tick(int Crotchet)
    {
        for (int i = 0; i < AudioTrcks.Count; i++)
        {
            var target = i < Intensity ? 1 : 0;
            if (AudioTrcks[i].volume != target && (TweenInProgress == null) )
            {
                TweenInProgress = AudioTrcks[i].DOFade(target, 1.2f).OnComplete(() => TweenInProgress = null);
            }
        }

        if (Intensity * IntensityThreshold <= Crotchet)
        {
            Intensity += 1;
        }
    }
    
    public void Play()
    {
        for (int i = 0; i < AudioTrcks.Count; i++)
        {
            AudioTrcks[i].Play();
            AudioTrcks[i].volume = i < Intensity ? 1 : 0;
        }
    }

    public void Stop()
    {
        for (int i = 0; i < AudioTrcks.Count; i++)
        {
            AudioTrcks[i].Stop();
        }
    }
}

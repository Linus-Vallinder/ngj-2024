using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    protected List<AudioSource> AudioTrcks;
    
    [Range(0,5)]
    public int Intensity;

    private void Awake()
    {
        Play();
        Stop();
    }

    private void Update()
    {
        for (int i = 0; i < AudioTrcks.Count; i++)
        {
            AudioTrcks[i].volume = i <= Intensity ? 1 : 0;
        }
    }
    
    public void Play()
    {
        for (int i = 0; i < AudioTrcks.Count; i++)
        {
            AudioTrcks[i].Play();
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

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SoundEffectData
{
    [Header("Sound Effect Data & Options")]
    public float PlayStartPosition = 0.0f;
    [Space] public AudioClip SoundEffectClip;
}

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] 
    private List<SoundEffectData> _sfxClips = new();
    
    private AudioSource _sfxSource;

    #region Unity Methods

    private void Awake()
    {
        _sfxSource = GetComponent<AudioSource>();
        GameManager.Instance.OnPlayerStab += PlayRandomSound;
    }

    private void OnDisable() =>
        GameManager.Instance.OnPlayerStab -= PlayRandomSound;

    #endregion

    private void PlaySound(SoundEffectData sfx)
    {
        _sfxSource.Stop();
        
        _sfxSource.clip = sfx.SoundEffectClip;
        
        if (sfx.SoundEffectClip.length >= sfx.PlayStartPosition)
            _sfxSource.time = sfx.PlayStartPosition;
        
        _sfxSource.Play();
    }

    private void PlayRandomSound()
    {
        var clipIndex = Random.Range(0, _sfxClips.Count);
        var clip = _sfxClips[clipIndex];
        
        if (clip == null)
            return;
        
        PlaySound(clip);
    }
}

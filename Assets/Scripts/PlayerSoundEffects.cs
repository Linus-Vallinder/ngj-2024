using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] 
    private List<AudioClip> _sfxClips = new();
    
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

    private void PlaySound(AudioClip clip)
    {
        _sfxSource.Stop();
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    private void PlayRandomSound()
    {
        var clipIndex = Random.Range(0, _sfxClips.Count);
        var clip = _sfxClips[clipIndex];
        
        if (clip == null)
            return;
        
        //TODO: THIS IS PLAYING SOMETHING IDK IF WE SHOULD THOUGH
        Debug.LogWarning("FINDING SFX BUT NOT PLAYING CUS TIMING");
        
        PlaySound(clip);
    }
}

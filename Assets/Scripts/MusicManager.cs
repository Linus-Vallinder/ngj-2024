using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private float currentSongPosition;
    
    public enum NoteType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    #region Unity Methods

    private void Awake()
    {
            
    }

    private void Update()
    {
        
    }

    #endregion
    
    #region Input Handling

    private void TryHitNote(NoteType noteType)
    {
        Debug.LogWarning($"Trying to hit a note, {noteType}");
    }

    public void OnUp() =>
        TryHitNote(NoteType.UP);

    public void OnDown() =>
        TryHitNote(NoteType.DOWN);

    public void OnLeft() =>
        TryHitNote(NoteType.LEFT);

    public void OnRight() => 
        TryHitNote(NoteType.RIGHT);

    #endregion
}

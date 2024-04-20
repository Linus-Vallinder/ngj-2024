using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _Renderer;
    
    [SerializeField]
    private Sprite _IdleFrame;
    
    [SerializeField]
    private Sprite _StabFrame;

    private float TimeOut;

    void Awake()
    {
        _Renderer.sprite = _IdleFrame;
    }

    public void Stab(float timeOut)
    {
        _Renderer.sprite = _StabFrame;
        TimeOut = timeOut;
    }

    public void Idle()
    {
        _Renderer.sprite = _IdleFrame;
    }

    void Update()
    {
        TimeOut -= Time.deltaTime;
        if (TimeOut <= 0)
        {
            Idle();
        }
    }

}

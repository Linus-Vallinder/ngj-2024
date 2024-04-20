using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Effects;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    List<Sprite> AnimationFrames;
        
    [SerializeField]
    protected Image Renderer;
    
    [SerializeField]
    protected Rigidbody2D RightRenderer;
    
    [SerializeField]
    protected Rigidbody2D LeftRenderer;

    protected Coroutine Anim;
    private Vector3 RightRenderStartingPos;
    private Vector3 LeftRenderStartingPos;

    private void Awake()
    {
        RightRenderStartingPos = RightRenderer.position;
        LeftRenderStartingPos = LeftRenderer.position;
    }

    public void BreakHeart()
    {
        Anim = StartCoroutine(Animation());
    }

    public void Resotre()
    {
        Renderer.color = Color.white;
        Renderer.sprite = AnimationFrames[0];
        
        RightRenderer.velocity = Vector2.zero;
        RightRenderer.angularVelocity = 0;
        RightRenderer.position = RightRenderStartingPos;
        
        LeftRenderer.velocity = Vector2.zero;
        LeftRenderer.angularVelocity = 0;
        LeftRenderer.position = LeftRenderStartingPos;
    }

    public void Hide()
    {
        Renderer.color = Color.clear;

    }

    IEnumerator Animation()
    {
        float animTime = 0.16f;
        Renderer.sprite = AnimationFrames[1];

        yield return new WaitForSeconds(animTime);
        
        Renderer.sprite = AnimationFrames[2];
        yield return new WaitForSeconds(animTime/5);
        Renderer.color = Color.clear;

        LeftRenderer.gameObject.SetActive(true);
        RightRenderer.gameObject.SetActive(true);
        // yield return new WaitForSeconds(animTime/2);
        LeftRenderer.AddForce(new Vector2(-1, 0.5f).normalized * 200, ForceMode2D.Impulse);
        RightRenderer.AddForce(new Vector2(1, 0.5f).normalized * 200, ForceMode2D.Impulse);

        yield return new WaitForSeconds(10);
        LeftRenderer.gameObject.SetActive(false);
        RightRenderer.gameObject.SetActive(false);
    }
}

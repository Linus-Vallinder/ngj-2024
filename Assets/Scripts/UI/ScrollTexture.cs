using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0.25f;

    private float currentSpeed;
    
    private Renderer _renderer;

    #region Unity Methods

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        StopScroll();
    }

    public void Update()
    {
        var offset = Time.time * currentSpeed;
        _renderer.material.mainTextureOffset = new Vector2(offset, 0); 
    }
    
    #endregion

    public void StartScroll()
    {
        currentSpeed = .2f;
    }

    public void StopScroll()
    {
        currentSpeed = .0f;
    }
}
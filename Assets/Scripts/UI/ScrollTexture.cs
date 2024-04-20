using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0.2f;

    private Renderer _renderer;

    #region Unity Methods

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        var offset = Time.time * scrollSpeed;
        _renderer.material.mainTextureOffset = new Vector2(offset, 0); 
    }
    
    #endregion
}
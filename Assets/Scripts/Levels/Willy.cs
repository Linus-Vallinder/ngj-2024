using UnityEngine;

public class Willy : MonoBehaviour
{
    public float lerpDuration = 2f;
    public Vector3 target = Vector3.zero;
    private Vector3 targetPosition;
    private float startTime;

    #region Unity Methods

    void Start()
    {
        targetPosition = new Vector3(target.x, target.y, transform.position.z);
        startTime = Time.time;
    }

    void Update()
    {
        var t = (Time.time - startTime) / lerpDuration;
        
        t = Mathf.Clamp01(t);
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
    }
    
    #endregion
}

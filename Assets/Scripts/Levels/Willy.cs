using UnityEngine;

public class Willy : MonoBehaviour
{
    public float lerpDuration = 650f;
    public Vector3 target = Vector3.zero;
    private Vector3 targetPosition;
    private float startTime;

    private bool isInCenter;
    
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

        if (!(transform.position.x < 1.62f + .01f)) 
            return;
        
        Debug.LogWarning("WE ARE IN THE MIDDLE!");
        FindObjectOfType<SpinSphere>().StopSpin();
        
        GameManager.Instance.TriggerEnd();
    }
    
    #endregion
}

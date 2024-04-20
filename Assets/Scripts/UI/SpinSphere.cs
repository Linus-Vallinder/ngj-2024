using UnityEngine;

public class SpinSphere : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 5;

    private float currentSpeed = 0;

    public void StartSpin() =>
        currentSpeed = spinSpeed;

    public void StopSpin() => 
        currentSpeed = 0f;
    
    private void LateUpdate()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0f, transform.localEulerAngles.y + (currentSpeed * Time.deltaTime), 0f)));
    }
}

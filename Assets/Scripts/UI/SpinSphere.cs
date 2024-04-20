using UnityEngine;

public class SpinSphere : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    
    private void LateUpdate()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0f, transform.localEulerAngles.y + (spinSpeed * Time.deltaTime), 0f)));
    }
}

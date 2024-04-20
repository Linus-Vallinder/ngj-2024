using UnityEngine;

public class StartBox : MonoBehaviour
{
    public void HideBox()
    {
        foreach (var child in transform.GetComponentsInChildren<Transform>())
            child.gameObject.SetActive(false);
    }

    public void ShowBox()
    {
        foreach (var child in transform.GetComponentsInChildren<Transform>())
            child.gameObject.SetActive(true);
    }
}

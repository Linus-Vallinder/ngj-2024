using UnityEngine;

public class StartBox : MonoBehaviour
{
    public void HideBox()
    {
        foreach (var child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name == gameObject.name)
                continue;
            
            child.gameObject.SetActive(false);
        }
    }

    public void ShowBox()
    {
        foreach (var child in transform.GetComponentsInChildren<Transform>(true))
            child.gameObject.SetActive(true);
    }
}

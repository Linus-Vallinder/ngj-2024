using UnityEngine;
using UnityEngine.UI;

public class StartBox : MonoBehaviour
{
    public void HideBox()
    {
        GetComponent<Image>().color = Color.clear;
        
        foreach (var child in transform.GetComponentsInChildren<Transform>())
            child.gameObject.SetActive(false);
    }

    public void ShowBox()
    {
        GetComponent<Image>().color = Color.white;
        
        foreach (var child in transform.GetComponentsInChildren<Transform>())
            child.gameObject.SetActive(true);
    }
}

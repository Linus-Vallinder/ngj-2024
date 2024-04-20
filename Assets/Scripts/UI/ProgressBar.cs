using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform StartPoint;
    
    [SerializeField]
    private RectTransform EndPoint;
    
    [SerializeField]
    private RectTransform DickIcon;

    private float EndTime;

    public void Init(float endTime)
    {
        Debug.Log("End Time " + endTime);
        EndTime = endTime;
        DickIcon.anchoredPosition = StartPoint.anchoredPosition;
    }
    
    public void UpdatePosition(float time)
    {
        Debug.Log(" Time " + time);

        DickIcon.position =
            Vector2.Lerp(StartPoint.position, EndPoint.position, time / EndTime);
    }
    
}

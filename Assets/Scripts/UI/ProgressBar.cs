using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform StartPoint;
    
    [SerializeField]
    private RectTransform EndPoint;
    
    [SerializeField]
    private RectTransform DickIcon;
    
    [SerializeField]
    private CanvasGroup CanvasGroup;

    private float EndTime;

    void Awake()
    {
        CanvasGroup.alpha = 0;
    }

    public void Init(float endTime)
    {
        EndTime = endTime;
        DickIcon.anchoredPosition = StartPoint.anchoredPosition;
        CanvasGroup.alpha = 1;
    }
    
    public void UpdatePosition(float time)
    {
        DickIcon.position = Vector2.Lerp(StartPoint.position, EndPoint.position, time / EndTime);
    }

    public void Stop()
    {
        CanvasGroup.alpha = 0;
    }
    
}

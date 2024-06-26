using System;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] 
    private Heart[] hearts = new Heart[4];

    #region Unity Methods

    private void Awake()
    {
        GameManager.Instance.OnHealthUpdate += UpdateUI;
    }

    private void Start()
    {
        HideUI();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHealthUpdate -= UpdateUI;
    }

    #endregion

    public void ShowUI()
    {
        hearts[0].Restore();
        hearts[1].Restore();
        hearts[2].Restore();
        hearts[3].Restore();
    }

    public void HideUI()
    {
        hearts[0].Hide();
        hearts[1].Hide();
        hearts[2].Hide();
        hearts[3].Hide();
    }
    
    //UGLY BUT WORKS WE NEED TO BE QUICK
    private void UpdateUI(int heartAmount)
    {
        switch (heartAmount)
        {
            case 0:
                hearts[0].BreakHeart();
                hearts[1].Hide();
                hearts[2].Hide();
                hearts[3].Hide();
                break;
            case 1:
                hearts[0].Restore();
                hearts[1].BreakHeart();
                hearts[2].Hide();
                hearts[3].Hide();
                break;
            case 2:
                hearts[0].Restore();
                hearts[1].Restore();
                hearts[2].BreakHeart();
                hearts[3].Hide();
                break;
            case 3:
                hearts[0].Restore();
                hearts[1].Restore();
                hearts[2].Restore();
                hearts[3].BreakHeart();
                break;
            case 4:
                hearts[0].Restore();
                hearts[1].Restore();
                hearts[2].Restore();
                hearts[3].Restore();
                break;
        }
    }
}

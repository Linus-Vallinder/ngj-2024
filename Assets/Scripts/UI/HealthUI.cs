using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] 
    private Image[] hearts = new Image[3];

    #region Unity Methods

    private void Awake()
    {
        GameManager.Instance.OnHealthUpdate += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHealthUpdate -= UpdateUI;
    }

    #endregion

    //UGLY BUT WORKS WE NEED TO BE QUICK
    private void UpdateUI(int heartAmount)
    {
        switch (heartAmount)
        {
            case 0:
                hearts[0].color = Color.clear;
                hearts[1].color = Color.clear;
                hearts[2].color = Color.clear;
                hearts[2].color = Color.clear;
                break;
            case 1:
                hearts[0].color = Color.white;
                hearts[1].color = Color.clear;
                hearts[2].color = Color.clear;
                hearts[3].color = Color.clear;
                break;
            case 2:
                hearts[0].color = Color.white;
                hearts[1].color = Color.white;
                hearts[2].color = Color.clear;
                hearts[3].color = Color.clear;
                break;
            case 3:
                hearts[0].color = Color.white;
                hearts[1].color = Color.white;
                hearts[2].color = Color.white;
                hearts[3].color = Color.clear;
                break;
            case 4:
                hearts[0].color = Color.white;
                hearts[1].color = Color.white;
                hearts[2].color = Color.white;
                hearts[3].color = Color.white;
                break;
        }
    }
}

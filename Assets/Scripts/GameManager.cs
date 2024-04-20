using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelton

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance != null) 
                return instance;
            
            instance = FindObjectOfType<GameManager>();
                
            if (instance != null) 
                return instance;
               
            var singletonObject = new GameObject("GameManager");
            instance = singletonObject.AddComponent<GameManager>();
            
            DontDestroyOnLoad(singletonObject);
           
            return instance;
        }
    }

    #endregion

    [field: SerializeField] 
    public int MaxLives { get; private set; } = 4;
    
    public int CurrentLives { get; set; }
    
    #region Unity Methods

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    private void Init()
    {
        CurrentLives = MaxLives;
    }
    
}

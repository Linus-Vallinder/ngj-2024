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

    public int CurrentLives { get; private set; }

    [Space, SerializeField] 
    private Timeline timeLine;
    public Timeline Timeline
    {
        get => timeLine;
        private set => timeLine = value;
    }
    
    [SerializeField]
    private BeatKeeper beatKeeper;
    
    public BeatKeeper BeatKeeper
    {
        get => beatKeeper;
        private set => beatKeeper = value;
    }
    
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

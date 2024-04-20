using System;
using UnityEngine;

public enum InputType
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

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

    public Action<InputType> OnInput;
    public Action<int> OnHealthUpdate;
    
    [field: SerializeField] 
    public int MaxLives { get; private set; } = 4;

    private int currentLives;
    public int CurrentLives
    {
        get => currentLives;

        private set
        {
            currentLives = value;
            OnHealthUpdate?.Invoke(currentLives);
        }
    }

    [Space, SerializeField] 
    private Timeline timeLine;
    public Timeline Timeline
    {
        get => timeLine;
        private set => timeLine = value;
    }
    
    [SerializeField]
    private BeatKeeper beatKeeper;

    public GameManager(Action<InputType> onInput)
    {
        OnInput = onInput;
    }

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

    private void Start()
    {
        Init();
    }

    #endregion

    private void Init()
    {
        CurrentLives = MaxLives;
    }

    #region Input Handling

    private void OnInputHandler(InputType type)
    {
        CurrentLives -= 1;

        if (CurrentLives <= 0)
            CurrentLives = 4;
        
        OnInput?.Invoke(type);
    }

    public void OnUp() =>
        OnInputHandler(InputType.UP);
    
    public void OnDown() =>
        OnInputHandler(InputType.DOWN);
    
    public void OnLeft() =>
        OnInputHandler(InputType.LEFT);
    
    public void OnRight() =>
        OnInputHandler(InputType.RIGHT);

    #endregion
}

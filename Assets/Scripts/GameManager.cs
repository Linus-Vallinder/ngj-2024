using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Cinemachine;

public enum GameState
{
    IDLE,
    OPENING,
    PLAYING,
    WIN,
    LOSE,
}

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
    public Action<GameState> OnGameStateUpdate;
    
    public List<Bar> stageTest = new();
    
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
    private HealthUI _healthUI;

    [SerializeField] 
    private ScrollTexture _scrollTexture;
    
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

    private GameState gameState;

    public GameState GameState
    {
        get => gameState;

        set
        {
            gameState = value;
            OnGameStateUpdate?.Invoke(gameState);
        }        
    }

    private TextBox _textBox;
    private bool OpeningIsDone;
    
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

        _textBox = FindObjectOfType<TextBox>();
    }

    private void Start()
    {
        GameState = GameState.IDLE;
    }

    #endregion

    private void Init()
    {
        GameState = GameState.PLAYING;
        CurrentLives = MaxLives;
        
        _healthUI.ShowUI();
        _scrollTexture.StartScroll();
    }

    public static void TriggerImpulse()
    {
        var impulseSource = FindObjectOfType<CinemachineImpulseSource>();
        
        if (impulseSource != null)
            impulseSource.GenerateImpulse(.2f);
    }
    
    public void HandleOnAny()
    {
        if (GameState == GameState.IDLE && OpeningIsDone)
        {
            Init();

            BeatKeeper.MaxBars = 16;
            BeatKeeper.Play();
            Timeline.Play(Bar.GetRandomStage(ref BeatKeeper.MaxBars));
        }
        else if (GameState == GameState.IDLE)
        {
            GameState = GameState.OPENING;
            _textBox.ShowTextBox();

            _textBox.OnEventDone += @event =>
            {
                if (!_textBox.IsOpeningEvent(@event))
                    return;
                
                GameState = GameState.IDLE;
                OpeningIsDone = true;
                _textBox.HideTextBox();
            };
        }  
    }

    public void PlayerGotHit()
    {
        CurrentLives -= 1;
        TriggerImpulse();
        
        if (CurrentLives <= 0)
        {
            OnReset();
        }
        else if (GameState == GameState.IDLE)
        {
            GameState = GameState.OPENING;
            _textBox.ShowTextBox();

            _textBox.OnEventDone += @event =>
            {
                if (!_textBox.IsOpeningEvent(@event))
                    return;
                
                GameState = GameState.IDLE;
                OpeningIsDone = true;
                _textBox.HideTextBox();
            };
        } 
    }
    
    #region Input Handling

    private void OnInputHandler(InputType type)
    {
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

    public void OnAny() => 
        HandleOnAny();
    
    public void OnReset()
    {
        _scrollTexture.StopScroll();
        _healthUI.HideUI();
        beatKeeper.Stop();
        
        GameState = GameState.IDLE;
        // var scene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(scene.name);
    }
    
    #endregion
}

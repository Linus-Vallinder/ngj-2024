using System;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public DialougeEvent OpeningCutscene;
    public DialougeEvent EndingCutscene;

    private int currentEventIndex;
    public DialougeEvent currentEvent;

    [Space, SerializeField]
    private TypewriterByCharacter _typewriterByCharacter;
    
    [SerializeField] 
    private TextMeshProUGUI _titleTextMeshProUGUI;
    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    public Action<DialougeEvent> OnEventDone;
    
    #region Unity Methods
    
    private void Start()
    {
        currentEvent = OpeningCutscene;
    }

    private void OnEnable() =>
        GameManager.Instance.OnInput += GoToNext;

    private void OnDisable() =>
        GameManager.Instance.OnInput -= GoToNext;

    #endregion

    public void ShowTextBox(DialougeEvent nextEvent = null)
    {
        if (nextEvent != null)
            currentEvent = nextEvent;
        
        GetComponent<Image>().color = Color.white;
        
        _textMeshProUGUI.gameObject.SetActive(true);
        _titleTextMeshProUGUI.gameObject.SetActive(true);
    }
    
    public void HideTextBox()
    {
        GetComponent<Image>().color = Color.clear;

        _titleTextMeshProUGUI.text = "";
        _textMeshProUGUI.text = "";
        
        _textMeshProUGUI.gameObject.SetActive(false);
        _titleTextMeshProUGUI.gameObject.SetActive(false);
    }

    #region Compare

    public bool IsOpeningEvent(DialougeEvent @event) =>
        @event == OpeningCutscene;

    public bool IsEndingEvent(DialougeEvent @event) =>
        @event == EndingCutscene;

    #endregion
    
    private void GoToNext(InputType _)
    {
        if (currentEvent == null)
            return;

        var instance = currentEvent.GetNextInstance(currentEventIndex++);

        if (instance == null)
        {
            Debug.Log("NO INSTANCE WE NULL");
            
            HideTextBox();
            OnEventDone?.Invoke(currentEvent);
            currentEvent = null;
            currentEventIndex = 0;
            return;
        }
        
        _typewriterByCharacter.SkipTypewriter();
        _titleTextMeshProUGUI.text = instance.SpeakerTitle;
        _textMeshProUGUI.text = instance.Text;
        _typewriterByCharacter.ShowText(_textMeshProUGUI.text);
    }
}

using System;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public DialougeEvent OpeningCutscene;

    private int currentEventIndex = 0;
    private DialougeEvent currentEvent;

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

    public void ShowTextBox()
    {
        GetComponent<Image>().color = Color.white;
        foreach (var child in gameObject.GetComponentsInChildren<Transform>())
            child.gameObject.SetActive(true);
    }
    
    public void HideTextbox()
    {
        GetComponent<Image>().color = Color.clear;
        foreach (var child in gameObject.GetComponentsInChildren<Transform>())
            child.gameObject.SetActive(false);
    }

    public bool IsOpeningEvent(DialougeEvent @event) =>
        @event == OpeningCutscene;
    
    private void GoToNext(InputType _)
    {
        if (currentEvent == null)
            return;

        var instance = currentEvent.GetNextInstance(currentEventIndex++);

        if (instance == null)
        {
            HideTextbox();
            OnEventDone?.Invoke(currentEvent);
            currentEvent = null;
            return;
        }
            
        _titleTextMeshProUGUI.text = instance.SpeakerTitle;
        _textMeshProUGUI.text = instance.Text;
        _typewriterByCharacter.ShowText(_textMeshProUGUI.text);
    }
}

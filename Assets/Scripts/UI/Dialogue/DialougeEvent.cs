using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Event", fileName = "New DialogueEvent", order = 0)]
public class DialougeEvent : ScriptableObject
{
    public List<BoxInstance> Instances = new();
 
    public BoxInstance GetNextInstance(int index)
    {
        return index > Instances.Count - 1 ? null : Instances[index];
    }
}

[System.Serializable]
public class BoxInstance
{
    [Header("BoxInstance Options")]
    public string SpeakerTitle;
    [Space, TextArea]
    public string Text;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EMOTION
{
    A,
    B,
    C,
    D,
    E,
}

public class Choice
{
    public EMOTION emotion = EMOTION.A;
    public string text = string.Empty;
    public CallbackEvent callback = null;
    public Choice(EMOTION _emotion, string _text, CallbackEvent _callback)
    {
        this.emotion = _emotion;
        this.text = _text;
        this.callback = _callback;
    }
}

public class ChoicePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject choiceButtonPrefab = null;

    public void Init(List<Choice> _choices)
    {
        this.StartCoroutine(this.Create(_choices));
    }
    
    private IEnumerator Create(List<Choice> _choices)
    {
        foreach (var choice in _choices)
        {
            this.CreateButton(choice);
            yield return new WaitForSeconds(0.15f);
        }
    }
    
    private void CreateButton(Choice _choice)
    {
        ChoiceButton choiceButton = Instantiate(this.choiceButtonPrefab).GetComponent<ChoiceButton>();
        choiceButton.transform.SetParent(this.transform, false);
        choiceButton.Init(_choice);
    }
}

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
    NONE,
}

public class Choice
{
    public EMOTION emotion = EMOTION.A;
    public string text = string.Empty;
    public CallbackEvent callback = null;
    public Vector3 position = new Vector3();
    public Choice(EMOTION _emotion, string _text, CallbackEvent _callback, Vector3 _position)
    {
        this.emotion = _emotion;
        this.text = _text;
        this.callback = _callback;
        this.position = _position;
    }
}

public class ChoicePanel : MonoBehaviour
{
    [SerializeField]
    private Transform choiceButtonParent = null;
    [SerializeField]
    private Transform iconParent = null;
    [SerializeField]
    private GameObject iconPrefab = null;

    [SerializeField]
    private GameObject choiceButtonPrefab = null;


    private List<ChoiceButton> choiceButtons = new List<ChoiceButton>();
    private List<Icon> icons = new List<Icon>();

    private ChoiceButton finishButton = null;
    private Icon finishIcon = null;

    private Vector3[] buttonPositions = {new Vector3(360f,-100f,0f), new Vector3(360f,-40f,0f) , new Vector3(360f,20f,0f), 
                                        new Vector3(360f,80f,0f), new Vector3(360f,140f,0f)};
    public void Init(List<Choice> _choices)
    {
        this.StartCoroutine(this.Create(_choices));
        
    }
    
    private IEnumerator Create(List<Choice> _choices)
    {
        for (int i = 0; i < _choices.Count; i++)
        {
            this.CreateButton(_choices[i], i);
            this.CreateIcon(_choices[i].emotion, i, _choices[i].position);
            yield return new WaitForSeconds(0.15f);
        }
    }
    
    private void CreateButton(Choice _choice, int _index)
    {
        ChoiceButton choiceButton = Instantiate(this.choiceButtonPrefab).GetComponent<ChoiceButton>();
        choiceButton.transform.SetParent(this.choiceButtonParent, false);
        choiceButton.transform.localPosition = this.buttonPositions[_index];
        choiceButton.Init(_choice, this.Finish);
        this.choiceButtons.Add(choiceButton);
    }

    private void CreateIcon(EMOTION _emotion, int _index, Vector3 _targetPosition)
    {
        Icon icon = Instantiate(this.iconPrefab).GetComponent<Icon>();
        icon.transform.SetParent(this.iconParent, false);
        icon.transform.localPosition =  this.buttonPositions[_index] + new Vector3(-30f, 0f, 0f);
        icon.Init(_emotion, _targetPosition);
        this.icons.Add(icon);
    }
    public void Finish(ChoiceButton _button)
    {

        for (int i = 0; i < this.choiceButtons.Count; i++)
        {
            if(_button != this.choiceButtons[i])
            {
                this.choiceButtons[i].Finish();
                this.icons[i].Finish();
            }
            else
            {
                this.finishButton = this.choiceButtons[i];
                this.finishIcon =  this.icons[i];
            }
        }
        this.choiceButtons.Clear();
        this.icons.Clear();
        this.StartCoroutine(this.SelectButtonFinish());
    }

    private IEnumerator SelectButtonFinish()
    {
        Choice choice = this.finishButton.choice;
        yield return new WaitForSeconds(2f);
        this.finishButton.Finish();
        yield return new WaitForSeconds(0.2f);
        this.finishIcon.Trash(this.finishButton.choice);
    }


    
    private void OnDestroy() {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject choiceButtonPrefab = null;

    public void Init(List<string> _texts)
    {
        foreach (var text in _texts)
        {
            this.CreateButton(text, this.DEBUG);
        }
    }
    
    private void DEBUG()
    {
        Debug.Log("선택 콜백");
    }
    
    private void CreateButton(string _text, CallbackEvent _callback)
    {
        ChoiceButton choiceButton = Instantiate(this.choiceButtonPrefab).GetComponent<ChoiceButton>();
        choiceButton.transform.SetParent(this.transform,false);
        choiceButton.Init(_text, _callback);
    }
}

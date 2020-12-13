using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndingCanvas : MonoBehaviour
{
    [SerializeField]
    private Image slime = null;
    [SerializeField]
    private CanvasGroup fadePanel = null;
    [SerializeField]
    private ChoicePanel choicePanel = null;

    public bool isBad = false;

    void Start()
    {
        this.slime.transform.localPosition = new Vector3(0f,500f,0f);
        this.fadePanel.alpha = 1f;
        this.StartCoroutine(this.Fade(false, 0f, () => { }));
        AudioManager.SetVolumeBgm(0.5f);
        AudioManager.PlayBgm(BGM.INTRO);
        EventManager.on(EVENT_TYPE.GO_MAIN, this.GoMainScene);
        EventManager.on(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
    }

    public void ShowChoicePanel(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        List<Choice> choices = (List<Choice>)Param;

        this.choicePanel.Init(choices);
    }
    public void GoMainScene(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        this.StartCoroutine(this.Fade(true, 1f, () => {
            SceneManager.LoadScene("IntroGame");
        }));
    }
    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
        EventManager.off(EVENT_TYPE.GO_MAIN, this.GoMainScene);
    }
    private IEnumerator Fade(bool _isUp ,  float _value, CallbackEvent _callback)
    {
        float value = (_isUp)? 0.05f : -0.05f;

        if(_isUp)
        {
            while (this.fadePanel.alpha < _value)
            {
                this.fadePanel.alpha += value;
                yield return new WaitForSeconds(0.05f);
            }
            if(this.isBad)
            {
                this.slime.transform.DOMoveX(0f,1f).SetEase(Ease.OutBounce);
                yield return new WaitForSeconds(1f);
            }
            
            yield return new WaitForSeconds(1f);
            _callback();
        }
        else
        {
            while (this.fadePanel.alpha > _value)
            {
                this.fadePanel.alpha += value;
                yield return new WaitForSeconds(0.05f);
            }
            _callback();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : SingletonMonoBehaviour<TextController>
{
    [SerializeField]
    private Canvas _ScenarioCanvas = null;
    [SerializeField]
    public Text mainText = null;
    [SerializeField]
    private GameObject _BranchArea = null;
    [SerializeField]
    private Text _trueText = null;
    [SerializeField]
    private Text _falseText = null;
    public bool isEnter = false;
    //public bool isConfirmed = false;
    public bool answer = false;
    public MessageState messageState = MessageState.None;
    public enum MessageState : byte
    {
        None,
        Displaying,
        Choosing
    }

    private void Start()
    {
        _ScenarioCanvas.gameObject.SetActive(false);
    }

    public bool InputReception()
    {
        // SEÇ»Ç«ÇÃèàóù?
        return Input.GetButtonDown("Jump");
    }

    public bool Choose()
    {
        // Y/N ëIë
        if (Input.GetAxis("Horizontal") < 0)
            answer = true;
        else if (0 < Input.GetAxis("Horizontal"))
            answer = false;

        if (answer)
        {
            _trueText.color = Color.red;
            _falseText.color = Color.black;
        }
        else
        {
            _trueText.color = Color.black;
            _falseText.color = Color.red;
        }

        return answer;
    }

    public void ActivateChoice(string y, string n)
    {
        _BranchArea.SetActive(true);
        messageState = MessageState.Choosing;
        _trueText.text = y;
        _falseText.text = n;
        answer = true;
    }

    public void DeactivateChoice()
    {
        _BranchArea.SetActive(false);
        messageState = MessageState.Displaying;
    }

    public void ActivateCanvas()
    {
        _ScenarioCanvas.gameObject.SetActive(true);
        messageState = MessageState.Displaying;
        _BranchArea.SetActive(false);
    }

    public void DeactivateCanvas()
    {
        _ScenarioCanvas.gameObject.SetActive(false);
        messageState = MessageState.None;
    }
}

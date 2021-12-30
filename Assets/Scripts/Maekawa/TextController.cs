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
    private bool _answer = false;
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
        // SEなどの処理?
        return Input.GetButtonDown("Jump");
    }

    public bool Choose()
    {
        // Y/N 選択
        if (Input.GetAxis("Horizontal") < 0)
            _answer = true;
        else if (0 < Input.GetAxis("Horizontal"))
            _answer = false;

        if (_answer)
        {
            _trueText.color = Color.red;
            _falseText.color = Color.black;
        }
        else
        {
            _trueText.color = Color.black;
            _falseText.color = Color.red;
        }

        return _answer;
    }

    public void ActivateChoice(string y, string n)
    {
        _BranchArea.SetActive(true);
        messageState = MessageState.Choosing;
        _trueText.text = y;
        _falseText.text = n;
        _answer = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    //選択されたボタンの色を変える
    
    EventMessage eventMessage;

    [SerializeField, Header("選んでいるときの色")]
    private Color ChooseColor = new Color();
    [SerializeField, Header("選んでいないときの色")]
    private Color UnChooseColor = new Color();
    
    //ボタンの色を変えるやつ
    private Image ButtonImage;

    //最終的にどの色になったかの判定
    private bool Decide = true;

    private void Start()
    {
        ButtonImage = GetComponent<Image>();
        eventMessage = GetComponent<EventMessage>();
    }

    public void OnChoose()
    {
        ButtonImage.color = ChooseColor;
        Debug.Log("呼ばれたよ");
    }
    public void OnUnChoose()
    {
        ButtonImage.color = UnChooseColor;
    }
    public void OnChooseButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnChoose();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnChoose();
        }
    }
}

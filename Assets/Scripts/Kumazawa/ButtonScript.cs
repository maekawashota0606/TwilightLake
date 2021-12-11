using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    //�I�����ꂽ�{�^���̐F��ς���
    
    EventMessage eventMessage;

    [SerializeField, Header("�I��ł���Ƃ��̐F")]
    private Color ChooseColor = new Color();
    [SerializeField, Header("�I��ł��Ȃ��Ƃ��̐F")]
    private Color UnChooseColor = new Color();
    
    //�{�^���̐F��ς�����
    private Image ButtonImage;

    //�ŏI�I�ɂǂ̐F�ɂȂ������̔���
    private bool Decide = true;

    private void Start()
    {
        ButtonImage = GetComponent<Image>();
        eventMessage = GetComponent<EventMessage>();
    }

    public void OnChoose()
    {
        ButtonImage.color = ChooseColor;
        Debug.Log("�Ă΂ꂽ��");
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

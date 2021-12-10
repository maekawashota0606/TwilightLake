using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    //�{�^����傫������
    public bool isButtonClickYes = false;
    public bool isButtonClickNo = false;
    EventMessage eventMessage;

    private void Start()
    {
        eventMessage = GetComponent<EventMessage>();
    }

    public void OnClickYes()
    {
        isButtonClickYes = true;
    }
    public void OnClickNo()
    {
        isButtonClickNo = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    //�V�i���I���i�[
    private string[] scenarios2;

    //uiText�ւ̎Q�Ƃ�ۂ�
    private Text uitext;

    //���݂̍s�ԍ�
    int currentLine;

    //�Z���t��\������e�L�X�g
    public Text message;
    int flag = 0;

    //�e�L�X�g�E�B���h�E
    public GameObject panel;

    public void StartText(string[] scenarios)
    {
        flag = 1;
        scenarios2 = scenarios;
        currentLine = 0;
        uitext = message;

        panel.SetActive(true);

        uitext.gameObject.SetActive(true);
        TextUpdate();
    }
    void TextUpdate()
    {
        uitext.text = scenarios2[currentLine];
        currentLine++;
    }

}

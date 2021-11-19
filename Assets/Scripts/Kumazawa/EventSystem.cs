using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystem : MonoBehaviour
{
    public EventManager eventManager;

    //�C�x���g�̃e�L�X�g���e
    public string[] scenarios;

    //�V�i���I���i�[
    private string[] scenarios2;//[]�̓��X�g�ɂȂ���

    //UIText�ւ̎Q�Ƃ�ۂ�
    private Text uiText;

    //���݂̍s�ԍ�
    int currentLine;

    //�Z���t��\������e�L�X�g
    public Text message;
    int flag = 0;

    //�e�L�X�g�E�B���h�E
    public GameObject sentence;

    public void StartText(string[] scenarios)
    {
        flag = 1;
        scenarios2 = scenarios;
        currentLine = 0;
        uiText = message;

    }

    public void Click()
    {
        if (flag == 1)
        {
            //���݂̍s�ԍ������X�g�܂ōs���ĂȂ���ԂŃ{�^���������ƃe�L�X�g���X�V����
            if (currentLine < scenarios2.Length)
            {
                TextUpdate();
            }

            else
            {
                //�Ō�܂ōs������A�e�L�X�g�ƃe�L�X�g�E�B���h�E������
                uiText.gameObject.SetActive(false);
                sentence.SetActive(false);
                flag = 0;
            }
        }
    }

    void TextUpdate()
    {
        //���݂̍s�ԍ���UIText�ɗ������݁A���݂̍s�ԍ�����ǉ�����
        uiText.text = scenarios2[currentLine];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            Debug.Log("�Ă΂ꂽ");
        }
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EventMessage : MonoBehaviour
{
    public Image canvas;
    public SignBoard signBoard;
    public Timer timer;
    public SearchText searchText;

    public GameObject ButtonYes;
    public GameObject ButtonNo;
    
    [Header("���b�Z�[�WUI")]
    public Text messageText;

    //unity���ŕ\������e�L�X�g������
    [SerializeField]
    private string[] allMessage;

    //���Ԗڂ̃��b�Z�[�W��
    private int messsageNum;

    //�e�L�X�g�X�s�[�h
    [SerializeField]
    private float textspeed = 0.05f;

    //�A�C�R���̓_�ł̌o�ߎ���
    private float elapsedTime = 0f;

    //�����Ă��镶���ԍ�
    private int nowTextNum = 0;

    //�}�E�X�N���b�N�𑣂��A�C�R��
    public GameObject clickIcon;

    //�N���b�N�A�C�R���̓_�ŕb��
    [SerializeField]
    private float clickFlashTime = 0.2f;

    //���b�Z�[�W��S�ĕ\���������ǂ���
    private bool isEndMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        messageText.enabled = false;
        clickIcon.SetActive(false);
        ButtonYes.SetActive(false);
        ButtonNo.SetActive(false);

        //switch(messsageNum)
        //{
        //    case 0:
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //    case 5:
        //        break;
        //    case 6:
        //        break;
        //    case 7:
        //        clickIcon.enabled = false;
        //        ButtonYes.SetActive(true);
        //        ButtonNo.SetActive(true);
        //        break;
        //    case 8:
        //        break;
        //    case 9:
        //        break;
        //    case 10:
        //        break;
        //    case 11:
        //        break;
        //    case 12:
        //        break;
        //    defult:
        //        break;

        //}
    }

    public void Event1()
    {
        //kanban�������Ă�SihnBoard�X�N���v�g�̒��̃t���O�ϐ���true�̎�
        if (signBoard.KanbanHit && Input.GetKeyDown(KeyCode.Space))
        {
            canvas.enabled = true;
            clickIcon.SetActive(true);
            messageText.enabled = true;
            searchText.HideSearch();

            //messageNum��allMessage�̐�����菭�Ȃ��Ȃ�
            if (messsageNum < allMessage.Length - 1)
            {
                //�����܂łɕ\�����Ă���e�L�X�g�Ɏc��̃��b�Z�[�W�ɑΉ����������𑫂�
                messsageNum++;
                messageText.text = allMessage[messsageNum];

                if (messsageNum == 2)
                {
                    Debug.Log("KUMAAAAAAAAAAAAAAAA");
                    clickIcon.SetActive(false);
                    ButtonYes.SetActive(true);
                    ButtonNo.SetActive(true);
                }
                //7�s�ڂɗ����番��
                if (messsageNum == 7)
                {
                    clickIcon.SetActive(false);
                    ButtonYes.SetActive(true);
                    ButtonNo.SetActive(true);
                }

                else
                {
                    clickIcon.SetActive(true);
                    ButtonYes.SetActive(false);
                    ButtonNo.SetActive(false);
                }
            }

            //messageNum��allMessage�̐�����葽���Ȃ��b���I������
            else
            {
                nowTextNum = 0;
                messsageNum++;
                messageText.enabled = false;
                clickIcon.SetActive(false);
                canvas.enabled = false;

                elapsedTime = 0f;

                //���b�Z�[�W���S���\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                if (messsageNum >= allMessage.Length)
                {
                    isEndMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;
        }
        //�N���b�N�A�C�R���̓_�ŏ���
        if (messsageNum < allMessage.Length - 1 && canvas.enabled == true)
        {
            elapsedTime += Time.deltaTime;

            //�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������
            if (elapsedTime >= clickFlashTime)
            {
                //clickIcon.SetActive = !clickIcon.SetActive;
                //elapsedTime = 0f;
            }
        }

    }
}

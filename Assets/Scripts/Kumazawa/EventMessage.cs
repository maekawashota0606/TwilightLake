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
    public CSVReader cSV;

    public GameObject ButtonYes;
    public GameObject ButtonNo;
    
    [Header("���b�Z�[�WUI")]
    public Text messageText;

    //unity���ŕ\������e�L�X�g������
    [SerializeField]
    private List<string> allMessage;

    //���Ԗڂ̃��b�Z�[�W��
    private int messsageNum = 0;

    //�e�L�X�g�X�s�[�h
    [SerializeField]
    private float textspeed = 0.05f;

    //�A�C�R���̓_�ł̌o�ߎ���
    private float elapsedTime = 0f;

    //�����Ă��镶���ԍ�
    private int nowTextNum = 0;

    //�}�E�X�N���b�N�𑣂��A�C�R��
    public GameObject clickIcon;

    ////�N���b�N�A�C�R���̓_�ŕb��
    //[SerializeField]
    //private float clickFlashTime = 0.2f;

    //���b�Z�[�W��S�ĕ\���������ǂ���
    private bool isEndMessage = false;

    //��b���ēx�\�����邩�ǂ����̂��
    private bool returnMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        //UI��S�Ĕ�\���ɂ���
        canvas.enabled = false;//enabled��Image���\���Ȃ̂�
        messageText.enabled = false;
        clickIcon.SetActive(false);
        ButtonYes.SetActive(false);
        ButtonNo.SetActive(false);

        cSV.GetComponent<CSVReader>().Read();
        allMessage = cSV.GetComponent<CSVReader>().csvDatas;
    }

    public void Event1()
    {
        //kanban�������Ă�SihnBoard�X�N���v�g�̒��̃t���O�ϐ���true�̎�
        if (signBoard.KanbanHit == true && Input.GetKeyDown(KeyCode.Space))
        {
            canvas.enabled = true;
            clickIcon.SetActive(true);
            messageText.enabled = true;
            searchText.HideSearch();

            //messageNum��allMessage�̐�����菭�Ȃ��Ȃ�
            if (messsageNum < allMessage.Count)
            {
                //�����܂łɕ\�����Ă���e�L�X�g�Ɏc��̃��b�Z�[�W�ɑΉ����������𑫂�
                //messsageNum++;

                messageText.text = allMessage[messsageNum];
                string currentEvent = allMessage[messsageNum];
                string[] datas = currentEvent.Split(',');
                Debug.Log("�e�L�X�g�o����");

                //messageNum��case�̐����܂ŗ����Ƃ�
                switch (messsageNum)
                {
                    //2�s�ڂɗ����番��̃{�^����\��(case�̌��̐�����ς���ƐF��ȍs�ŕ���̃{�^����\���ł���)
                    case 2:
                        Debug.Log("KUMAAAAAAAAAAAAAAAA");
                        clickIcon.SetActive(false);
                        ButtonYes.SetActive(true);
                        ButtonNo.SetActive(true);
                        break;
                    //7�s�ڂɗ����番��̃{�^����\��(case�̌��̐�����ς���ƐF��ȍs�ŕ���̃{�^����\���ł���)
                    case 7:
                        clickIcon.SetActive(false);
                        ButtonYes.SetActive(true);
                        ButtonNo.SetActive(true);
                        break;
                    //���̍s���ɗ������b��i�߂�
                    default:
                        clickIcon.SetActive(true);
                        ButtonYes.SetActive(false);
                        ButtonNo.SetActive(false);
                        break;
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
                if (messsageNum >= allMessage.Count)
                {
                    isEndMessage = true;
                }
            }
        }
        ////�N���b�N�A�C�R���̓_�ŏ���
        //if (messsageNum < allMessage.Length - 1 && canvas.enabled == true)
        //{
        //    elapsedTime += Time.deltaTime;

        //    //�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������
        //    if (elapsedTime >= clickFlashTime)
        //    {
        //        clickIcon.SetActive = !clickIcon.SetActive;
        //        elapsedTime = 0f;
        //    }
        //}
    }
}

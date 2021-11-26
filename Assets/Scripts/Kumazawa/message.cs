using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class message : MonoBehaviour
{
    public Image canvas;
    public SignBoard signBoard;
    public Timer timer;
    public SearchText searchText;

    //���b�Z�[�WUI
    public Text messageText;

    [SerializeField]
    private string[] allMessage;

    //���b�Z�[�W�̉��Ԗڂ�
    private int messsageNum;

    //�e�L�X�g�X�s�[�h
    [SerializeField]
    private float textspeed = 0.05f;

    //���̃��b�Z�[�W��\������܂ł̌o�ߎ��ԁA�A�C�R���̓_�ł̌o�ߎ���
    private float elapsedTime = 0f;

    //�����Ă��镶���ԍ�
    private int nowTextNum = 0;

    //�}�E�X�N���b�N�𑣂��A�C�R��
    [SerializeField]
    private Image clickIcon;

    //�N���b�N�A�C�R���̓_�ŕb��
    [SerializeField]
    private float clickFlashTime = 0.2f;

    //���b�Z�[�W��S�ĕ\���������ǂ���
    private bool isEndMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        clickIcon.enabled = false;
        messageText.enabled = false;
        messageText = GetComponentInChildren<Text>();
        searchText = GetComponent<SearchText>();
    }

    // Update is called once per frame
    void Update()
    {
        ////���b�Z�[�W���I����Ă��邩�A���b�Z�[�W�������ꍇ�͂���ȍ~�������Ȃ�
        //if (isEndMessage || allMessage == null)
        //{
        //    return;
        //}

        //kanban�������Ă�SihnBoard�X�N���v�g�̒��̃t���O�ϐ���true�̎�
        if (signBoard.GetComponent<SignBoard>().KanbanHit == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.enabled = true;
                clickIcon.enabled = true;
                messageText.enabled = true;
            }
            elapsedTime += Time.deltaTime;

            //���b�Z�[�W�\�����ɃV�t�g�L�[����������ꊇ�\��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //messageNum��allMessage�̐�����菭�Ȃ��Ȃ�
                if (messsageNum < allMessage.Length - 1)
                {
                    //�����܂łɕ\�����Ă���e�L�X�g�Ɏc��̃��b�Z�[�W�ɑΉ����������𑫂�
                    messsageNum++;
                    messageText.text = allMessage[messsageNum];
                }
                //messageNum��allMessage�̐�����葽���Ȃ��b���I������
                else
                {
                    nowTextNum = 0;
                    messsageNum++;
                    messageText.enabled = false;
                    clickIcon.enabled = false;
                    canvas.enabled = false;

                    elapsedTime = 0f;

                    //���b�Z�[�W���S���\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                    if (messsageNum >= allMessage.Length)
                    {
                        isEndMessage = true;
                    }
                }
            }

            //�N���b�N�A�C�R���̓_�ŏ���(�����ł��K�v�Ȃ̂�������Ȃ��Ȃ��Ă܂�)
            if (messsageNum < allMessage.Length - 1 && canvas.enabled == true)
            {
                elapsedTime += Time.deltaTime;

                //�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������
                if (elapsedTime >= clickFlashTime)
                {
                    clickIcon.enabled = !clickIcon.enabled;
                    elapsedTime = 0f;
                }
            }
        }
    }
}

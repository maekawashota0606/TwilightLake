using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMessage : MonoBehaviour
{
    [Header("�匳�̃L�����o�X")]
    public Image canvas;
    [Header("�Ŕ̎擾")]
    public SignBoard signBoard;
    [Header("�^�C�}�[�̎擾")]
    public Timer timer;
    [Header("�Z�{�^���Œ��ׂ��UI�̎擾")]
    public SearchText searchText;
    [Header("CSV�̎擾")]
    public CSVReader cSV;

    [Header("�X�N���v�g{Yes�{�^��}")]
    private ButtonScript buttonScriptYes;
    [Header("�X�N���v�g{No�{�^��}")]
    private ButtonScript buttonScriptNo;

    [Header("Yes�{�^���̃e�L�X�g")]
    [SerializeField]
    private Text ButtonTextYes;

    [Header("No�{�^���̃e�L�X�g")]
    [SerializeField]
    private Text ButtonTextNo;

    [Header("Yes�{�^���������ꂽ�Ƃ���isButtonClickYes��؂�ւ���X�C�b�`�̔�")]
    [SerializeField]
    private bool SwitchYes;
    [Header("No�{�^���������ꂽ�Ƃ���isButtonClickNo��؂�ւ���X�C�b�`�̔�")]
    [SerializeField]
    private bool SwitchNo;

    [Header("Yes�{�^���̎擾")]
    public Image ButtonYes;
    [Header("No�{�^���̎擾")]
    public Image ButtonNo;

    ////�͂��������ꂽ����bool��0��1��
    //private int yesid = 0;

    ////�������������ꂽ����bool��0��1��
    //private int noid = 0;

    [Header("���b�Z�[�WUI")]
    public Text messageText;

    //unity���ŕ\������e�L�X�g������
    [Header("CSV�f�[�^������string�̔�")]
    [SerializeField]
    private List<string> allMessage;

    //���Ԗڂ̃��b�Z�[�W��
    private int messsageNum = 0;

    //�{�^���̑傫���𑀂�ꏊ�̔�
    private Vector2 TFBaseYes;

    //�{�^���̑傫���𑀂�ꏊ�̔�
    private Vector2 TFBaseNo;

    //���ꂼ��̃{�^����傫������l��Unity�ォ����ݒ�ł���悤��
    [Header("�{�^����X���̑傫���ύX�l")]
    public float ButtonBigerX = 1;

    [Header("�{�^����X���̑傫���ύX�l")]
    public float ButtonBigerY = 1;

    ////�e�L�X�g�X�s�[�h
    //[SerializeField]
    //private float textspeed = 0.05f;

    ////�A�C�R���̓_�ł̌o�ߎ���
    //private float elapsedTime = 0f;

    ////�����Ă��镶���ԍ�
    //private int nowTextNum = 0;

    //�}�E�X�N���b�N�𑣂��A�C�R��
    [Header("�N���b�N�A�C�R���̃Q�[���I�u�W�F�N�g���擾")]
    public GameObject clickIcon;

    ////�N���b�N�A�C�R���̓_�ŕb��
    //[SerializeField]
    //private float clickFlashTime = 0.2f;

    ////���b�Z�[�W��S�ĕ\���������ǂ���
    //private bool isEndMessage = false;

    ////��b���ēx�\�����邩�ǂ����̂��
    //private bool returnMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        //������UI��S�Ĕ�\���ɂ���B(enabled��Image���\���Ȃ̂Ŏ擾�͏o����)
        canvas.enabled = false;//
        messageText.enabled = false;
        clickIcon.SetActive(false);
        ButtonYes.enabled = false;
        ButtonNo.enabled = false;
        ButtonTextYes.enabled = false;
        ButtonTextNo.enabled = false;

        cSV.GetComponent<CSVReader>().Read();
        allMessage = cSV.GetComponent<CSVReader>().csvDatas;
        buttonScriptYes = ButtonYes.GetComponent<ButtonScript>();
        buttonScriptNo = ButtonNo.GetComponent<ButtonScript>();
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
                messageText.text = allMessage[messsageNum];
                string currentEvent = allMessage[messsageNum];
                string[] csvdatas = currentEvent.Split(',');

                Debug.Log("�e�L�X�g�o����");

                //messageNum��case�̐����܂ŗ����Ƃ�(�\���֌W)
                switch (messsageNum)
                {
                    //2�s�ڂɗ����番��̃{�^����\��(case�̌��̐�����ς���ƐF��ȍs�ŕ���̃{�^����\���ł���)
                    case 1:
                        clickIcon.SetActive(false);
                        ButtonYes.enabled = true;
                        ButtonNo.enabled = true;
                        ButtonTextYes.enabled = true;
                        ButtonTextNo.enabled = true;

                        ////���ꂼ��̃{�^������������(datas[����])�̐����Ɠ����s���ɔ��
                        //if (SwitchYes == true)
                        //{
                        //    yesid = int.Parse(cSV.csvDatas[2]);
                        //    messsageNum = yesid;
                        //}
                        //if (SwitchNo == true)
                        //{
                        //    isEndMessage = true;
                        //}
                        break;
                    //7�s�ڂɗ����番��̃{�^����\��(case�̌��̐�����ς���ƐF��ȍs�ŕ���̃{�^����\���ł���)
                    case 6:
                        clickIcon.SetActive(false);
                        ButtonYes.enabled = true;
                        ButtonNo.enabled = true;
                        ButtonTextYes.enabled = true;
                        ButtonTextNo.enabled = true;

                        ////���ꂼ��̃{�^������������(datas[����])�̐����Ɠ����s���ɔ��
                        //if (SwitchYes == true)
                        //{
                        //    yesid = int.Parse(cSV.csvDatas[10]);
                        //    messsageNum = yesid;
                        //}
                        //if (SwitchNo == true)
                        //{
                        //    noid = int.Parse(cSV.csvDatas[8]);
                        //    messsageNum = noid;
                        //}
                        break;
                    //���̍s���ɗ������b��i�߂�
                    default:
                        clickIcon.SetActive(true);
                        ButtonYes.enabled = false;
                        ButtonNo.enabled = false;
                        messsageNum++;
                        break;
                }
            }
            //messageNum��allMessage�̐�����葽���Ȃ��b���I������
            else
            {
                //nowTextNum = 0;
                messsageNum++;
                messageText.enabled = false;
                clickIcon.SetActive(false);
                canvas.enabled = false;

                //elapsedTime = 0f;

                ////���b�Z�[�W���S���\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                //if (messsageNum >= allMessage.Count)
                //{
                //    isEndMessage = true;
                //}
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

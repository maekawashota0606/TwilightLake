using UnityEngine;

public abstract class NPC : MonoBehaviour, IInspectible
{
    [SerializeField]
    public int eventID = 0;
    [SerializeField]
    public int eventIndex = 0;
    [SerializeField]
    public int eventState = 0;
    [SerializeField]
    protected int[] eventFinishState = new int[3];
    [SerializeField]
    protected int[] addKarmaPoint = new int[3];

    public void Inspected()
    {
        StartCoroutine(EventController.Instance.DisplayScenario(this));
    }

    public virtual void ChangeState(int state)
    {
        eventState = state;
    }

    /// <summary>
    /// ����O�̏���
    /// </summary>
    /// <returns>����ł��邩</returns>
    public abstract bool PreBranching();
    /// <summary>
    /// �����̏���
    /// </summary>
    /// <param name="answer">�v���C���[�̑I��</param>
    public abstract void OnBranched(bool answer);
}

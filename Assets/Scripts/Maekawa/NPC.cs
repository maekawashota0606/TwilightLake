using UnityEngine;

public abstract class NPC : MonoBehaviour, IInspectible
{
    [SerializeField]
    public int eventID = 0;
    [SerializeField]
    public int eventIndex = 0;
    [SerializeField]
    public int eventState = 0;

    public void Inspected()
    {
        StartCoroutine(EventController.Instance.DisplayScenario(this));
    }

    /// <summary>
    /// 分岐前の処理
    /// </summary>
    /// <returns>分岐できるか</returns>
    public abstract bool PreBranching();
    /// <summary>
    /// 分岐後の処理
    /// </summary>
    /// <param name="answer">プレイヤーの選択</param>
    public abstract void OnBranched(bool answer);
}

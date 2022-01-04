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

    public abstract bool PreBranching();
    public abstract void OnBranched();
}

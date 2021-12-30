using UnityEngine;

public class NPC : MonoBehaviour, IInspectible
{
    [SerializeField]
    public readonly int eventID = 0;
    [SerializeField]
    public int eventIndex = 0;
    public int eventState = 0;

    public void Inspected()
    {
        StartCoroutine(EventController.Instance.DisplayScenario(this));
    }
}

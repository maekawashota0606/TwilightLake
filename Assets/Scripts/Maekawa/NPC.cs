using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        //Inspected();
    }

    // test
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Inspected();
    }
}

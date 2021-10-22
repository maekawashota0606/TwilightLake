using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hukidasi : MonoBehaviour
{
    public void ShowBalloon()
    {
        this.gameObject.SetActive(true);
        CancelInvoke("HideBalloon");
        Invoke("HideBalloon", 3.0f);
    }

    public void HideBalloon()
    {
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

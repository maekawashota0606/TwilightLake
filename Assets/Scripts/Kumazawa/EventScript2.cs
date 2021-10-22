using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript2 : MonoBehaviour
{
    public TextController textController;

    //キャラクタースクリプトから送られてきたAflagflagの格納用
    bool flag2 = false;
    
    //
    string[] scenarios2;

    public void StartEvent(bool flag,string[] scenarios)
    {
        flag2 = flag;
        scenarios2 = scenarios;
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

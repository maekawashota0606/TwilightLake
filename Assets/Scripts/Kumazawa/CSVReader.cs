using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        csvFile = Resources.Load("Event1/Event") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }

        Debug.Log(csvDatas[1][1]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

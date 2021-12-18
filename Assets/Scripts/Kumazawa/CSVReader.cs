 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    TextAsset csvEvent1;
    public List<string> csvDatas = new List<string>();

    // Start is called before the first frame update
    public void Read()
    {
        csvEvent1 = Resources.Load("Event1/Event") as TextAsset;
        StringReader reader = new StringReader(csvEvent1.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            if (line.Contains("#")) continue;//#�������Ă���Ƃ���͖�������
            csvDatas.Add(line);//wakeru
            
        }
    }
}

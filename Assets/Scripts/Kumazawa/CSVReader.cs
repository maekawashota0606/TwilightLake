 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    TextAsset csvFile;
    public List<string> csvDatas = new List<string>();

    // Start is called before the first frame update
    public void Read()
    {
        csvFile = Resources.Load("Event1/Event") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            if (line.Contains("#")) continue;//#‚ª“ü‚Á‚Ä‚¢‚é‚Æ‚±‚ë‚Í–³Ž‹‚·‚é
            csvDatas.Add(line);//wakeru
        }
    }
}

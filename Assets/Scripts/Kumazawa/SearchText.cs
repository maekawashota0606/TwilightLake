using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchText : MonoBehaviour
{
    //〇ボタンで調べるを出す

    public void ShowSearch()
    {
        gameObject.SetActive(true);
    }
    public void HideSearch()
    {
        gameObject.SetActive(false);
    }
}

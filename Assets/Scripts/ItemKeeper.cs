using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int haskeys = 0;
    public static int hasArrows = 0;

    
    void Start()
    {
        haskeys = PlayerPrefs.GetInt("Keys");
        hasArrows = PlayerPrefs.GetInt("Arrows");
    }

    public static void SaveItem()
    {
        PlayerPrefs.SetInt("Keys", haskeys);
        PlayerPrefs.SetInt("Arrows", hasArrows);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataMma : MonoBehaviour
{
    public static SaveDataList arrangedatalist; //세이브데ㅇ;ㅣ타ㅣ ㄹ이스
    void Start()
    {
        arrangedatalist = new SaveDataList();
        arrangedatalist.saveDatas = new SaveData[] { };


        int nArrow = PlayerPrefs.GetInt("Arrows");
        arrangedatalist = JsonUtility.FromJson<SaveDataList>("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

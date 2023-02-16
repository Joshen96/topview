using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class RoomManager : MonoBehaviour
{

    [Serializable]
    public class RoomData 
    {
        public int RoomID = 0;
        public string RoomTag = "";

    }

    public RoomData ValueData;



    public static int doorNumber = 0;

    void Start()
    {
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");  //exit 태그 오브젝트

        for (int i = 0; i < enters.Length; i++)
        {
            GameObject doorObj = enters[i];
            Exit exit = doorObj.GetComponent<Exit>();
            if(doorNumber == exit.doorNumber)
            {
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                if (exit.direction == ExitDirection.up)
                {
                    y += 1;
                }
                else if (exit.direction == ExitDirection.right)
                {
                    x += 1;
                }
                else if (exit.direction == ExitDirection.down)
                {
                    y -= 1;
                }
                else if (exit.direction == ExitDirection.left)
                {
                    x -= 1;
                }
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);
                break;
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void ChangeScene(string sceneName, int doorNum) //원하는 문의 위치로 가기위해 doorNum
    {
        doorNumber = doorNum;
        SceneManager.LoadScene(sceneName);
        Debug.Log(doorNumber);
    }

    private void OnDestroy()
    {
        string json = JsonUtility.ToJson(ValueData);
        return;


    }
}

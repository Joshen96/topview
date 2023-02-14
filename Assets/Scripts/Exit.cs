using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitDirection
{
    right,
    left,
    down,
    up,
}
public class Exit : MonoBehaviour
{
    public string sceneName = "";
    public int doorNumber = 0;
    public ExitDirection direction = ExitDirection.down;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("√‚±∏");
            RoomManager.ChangeScene(sceneName, doorNumber);
        }
    }
    
    void Update()
    {
        
    }
}

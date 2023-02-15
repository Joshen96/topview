using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPad : MonoBehaviour
{
    public float MaxLength = 70.0f;
    public bool is4Dpad = false;

    GameObject player;
    Vector2 defPos;
    Vector2 downPos;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        defPos = GetComponent<RectTransform>().localPosition;
    }

    public void Attack()
    {
        ArrowShoot shoot = GetComponent<ArrowShoot>(); //에오루샷스크립트에서 가져옴
        shoot.Attack();
    }

    public void PadDown()
    {
        downPos = Input.mousePosition;
    }
    public void PadUp()
    {
        GetComponent<RectTransform>().localPosition = defPos;
        PlayerController playercont = player.GetComponent<PlayerController>();
        playercont.SetAxisZero();

    }

    public void PadDrag()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 newTabPos = mousePosition - defPos;
        Vector2 axis = newTabPos.normalized;

        float len = Vector2.Distance(defPos, newTabPos);
        if(len> MaxLength)
        {
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;

        }

        GetComponent<RectTransform>().localPosition = newTabPos;

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetAxis(axis.x, axis.y);
    }
}

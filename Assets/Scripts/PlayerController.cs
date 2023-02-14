using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    //등록한 애니
    public string upAni = "PlayerUP";
    public string downAni = "PlayerDown";
    public string leftAni = "PlayerLeft";
    public string rightAni = "PlayerRight";
    public string deadAni = "PlayerDead";

    //현재 플레이 애니
    string nowAni = "";
    string oldAni = "";
    

    // 이동

    float axisH;// (x축)
    float axisV;// (y축)
    public float angleZ = -90.0f;

    Rigidbody2D rBody;
    bool ismoving = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        nowAni = downAni;
    }

  
    void Update()
    {
        if(ismoving == false)
        {
            
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        Vector2 fromPt = this.transform.position;
        Vector2 toPt = new Vector2(fromPt.x+axisH, fromPt.y+axisV);//움직임

        angleZ = GetAngle(fromPt, toPt);
        

        if (angleZ >= -45 && angleZ < 45)
        {
            nowAni = rightAni;
            
        }
        else if(angleZ >= 45 && angleZ <=135)
        {
            nowAni = upAni;
            
        }
        else if (angleZ >= -135 && angleZ <= -45)
        {
            nowAni = downAni;
            
        }
        else
        {
            nowAni = leftAni;
            
        }


        if (nowAni != oldAni)
        {
            oldAni = nowAni;

            GetComponent<Animator>().Play(nowAni);
            
        }

        rBody.velocity = new Vector2(axisH, axisV) * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector2(axisH, axisV) * speed;
        
    }


    float GetAngle(Vector2 p1, Vector2 p2)  //각구하는함수
    {
        float angle = angleZ;

        if (axisH != 0 || axisV != 0)
        {
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;

            float rad = Mathf.Atan2(dy, dx);
            angle = rad * Mathf.Rad2Deg;

        }

        return angle;
    }
}

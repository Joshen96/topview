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



    //데미지처리 
    public static int hp = 3;

    public static string gameState;
    public bool inDamage = false;




    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        nowAni = downAni;

        gameState = "playing";
    }

  
    void Update()
    {
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        if (ismoving == false)
        {
            
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        Vector2 fromPt = this.transform.position;
        Vector2 toPt = new Vector2(fromPt.x+axisH, fromPt.y+axisV);//움직이는 각도구하기위한

        angleZ = GetAngle(fromPt, toPt);  //각도 구함
        

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

        if (gameState != "playing") return;
        if (inDamage)
        {
            float val = Mathf.Sin(Time.time * 50);
            Debug.LogWarning(val);

            if(val > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;
        }
        rBody.velocity = new Vector2(axisH, axisV) * speed;  //실제이동 부분
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            GetDamage(collision.gameObject);
        }
    }
    void GetDamage(GameObject enemy)
    {
        if (gameState == "playing")
        {
            hp--;
            if (hp > 0)
            {
                rBody.velocity = Vector2.zero;
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rBody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
            }
            else
            {
                GameOver();
            }
        }
    }
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    void GameOver()
    {
        gameState = "gameover";
        GetComponent<CircleCollider2D>().enabled = false;
        rBody.velocity = Vector2.zero;
        rBody.gravityScale = 2;
        rBody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        GetComponent<Animator>().Play(deadAni);
        Destroy(gameObject, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{

    //애너미 정보
    [HideInInspector]
    public int hp = 2;
    public float speed = 0.5f;
    [HideInInspector]
    public float reactionDistance = 4.0f; // 플레이어 거리탐지 
    
    public string idleAnime = "EnemyDown";

    public string upAnime = "EnemyUp";
    public string downAnime = "EnemyDown";
    public string leftAnime = "EnemyLeft";
    public string rightAnime = "EnemyRight";
    public string deadAnime = "EnemyDead";

    string nowAnimation = "";
    string oldAnimation = "";

    public SaveData savedata;



    float asixH;
    float asixV;

    Rigidbody2D rigi;

    
    bool isActive = false;



    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) //검수코드 플레이어없다면
        {
            isActive = false;
            rigi.velocity = Vector2.zero; //움직멈춤 벡터2제로
            return;
        }

        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist < reactionDistance)
        {
            Debug.Log("감지");
            isActive = true;
        }
        else
        {
            Debug.Log("미감지");
            isActive = false;
        }

        if (isActive)
        {
            float dx = player.transform.position.x - transform.position.x;
            float dy = player.transform.position.y - transform.position.y; // 방향 찾기 위함 즉 적위치 기준의 플레이어 방향
            float rad = Mathf.Atan2(dy, dx); // 애너미입장에서 플레이어위치 dy dx

            float angle = rad * Mathf.Rad2Deg; //각도구하기


            if (angle > -45.0f && angle <= 45.0f)
            {
                nowAnimation = rightAnime;
            }
            else if (angle > 45.0f && angle <= 135.0f)
            {
                nowAnimation = upAnime;
            }
            else if (angle >= -135.0f && angle <= -45.0f)
            {
                nowAnimation = downAnime;
            }
            else
            {
                nowAnimation = leftAnime;
            }

            asixH = Mathf.Cos(rad) * speed;
            asixV = Mathf.Sin(rad) * speed;

        }
        
    }
    private void FixedUpdate()
    {
        Animator animator = GetComponent<Animator>();
        if (isActive && hp > 0)
        {
            rigi.velocity = new Vector2(asixH, asixV);

            if (nowAnimation != oldAnimation)
            {
                oldAnimation = nowAnimation;
                
                animator.Play(nowAnimation);
            }
            animator.Play(nowAnimation);
        }
        else
        {
            rigi.velocity = Vector2.zero;
            nowAnimation= idleAnime;
           
            animator.Play(nowAnimation);
        }

        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            hp--;
            if (hp <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                rigi.velocity = Vector2.zero;
                Animator animator = GetComponent<Animator>();
                animator.Play(deadAnime);

                Destroy(this.gameObject, 1);
            }
        }
    }
}

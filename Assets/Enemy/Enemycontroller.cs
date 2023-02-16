using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{

    //�ֳʹ� ����
    [HideInInspector]
    public int hp = 2;
    public float speed = 0.5f;
    [HideInInspector]
    public float reactionDistance = 4.0f; // �÷��̾� �Ÿ�Ž�� 
    
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
        if (player == null) //�˼��ڵ� �÷��̾���ٸ�
        {
            isActive = false;
            rigi.velocity = Vector2.zero; //�������� ����2����
            return;
        }

        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist < reactionDistance)
        {
            Debug.Log("����");
            isActive = true;
        }
        else
        {
            Debug.Log("�̰���");
            isActive = false;
        }

        if (isActive)
        {
            float dx = player.transform.position.x - transform.position.x;
            float dy = player.transform.position.y - transform.position.y; // ���� ã�� ���� �� ����ġ ������ �÷��̾� ����
            float rad = Mathf.Atan2(dy, dx); // �ֳʹ����忡�� �÷��̾���ġ dy dx

            float angle = rad * Mathf.Rad2Deg; //�������ϱ�


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

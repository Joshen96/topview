using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f;
    public float shootDelay = 0.25f;

    public GameObject bowPrefab;//활프리텝
    public GameObject arrowPrefab; // 화살 프리펩


    GameObject bowObj;

    bool inAttack = false;

    void Start()
    {
        Vector3 pos = transform.position;
        bowObj = Instantiate(bowPrefab, pos, Quaternion.identity);
        bowObj.transform.SetParent(transform);


    }

    
    void Update()
    {
        float bowZ = -1.0f;
        PlayerController player = GetComponent<PlayerController>();
        if (player.angleZ > 30 && player.angleZ < 150)
        {
            //캐릭터 위로 방향인데
            bowZ = 1;
        }
        // 활의 회전
        bowObj.transform.rotation = Quaternion.Euler(0, 0, player.angleZ);
        bowObj.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ);

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("누름");
            Attack();
        }

        
    }

    public void Attack()
    {
        
        if (ItemKeeper.hasArrows >0 && inAttack == false) 
        {
            ItemKeeper.hasArrows -= 1;
            inAttack = true;

            PlayerController player = GetComponent<PlayerController>();
            float angleZ = player.angleZ; //플레이어의 각의정보 

            Quaternion r = Quaternion.Euler(0, 0, angleZ);
            GameObject arrowOj = Instantiate(arrowPrefab, transform.position, r);


            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); // angleZ각을 알고있을때 x좌표 아는 코드
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad); //


            Vector3 vFly = new Vector3(x, y,0)*shootSpeed; //벡터정보

            Rigidbody2D rbody = arrowOj.GetComponent<Rigidbody2D>();
            rbody.AddForce(vFly, ForceMode2D.Impulse);  

            Invoke("stopAttack", shootDelay);
        }
    }
    public void stopAttack()
    {
        inAttack = false;
    }

}


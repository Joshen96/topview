using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f;
    public float shootDelay = 0.25f;

    public GameObject bowPrefab;//Ȱ������
    public GameObject arrowPrefab; // ȭ�� ������


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
            //ĳ���� ���� �����ε�
            bowZ = 1;
        }
        // Ȱ�� ȸ��
        bowObj.transform.rotation = Quaternion.Euler(0, 0, player.angleZ);
        bowObj.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ);

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("����");
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
            float angleZ = player.angleZ; //�÷��̾��� �������� 

            Quaternion r = Quaternion.Euler(0, 0, angleZ);
            GameObject arrowOj = Instantiate(arrowPrefab, transform.position, r);


            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); // angleZ���� �˰������� x��ǥ �ƴ� �ڵ�
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad); //


            Vector3 vFly = new Vector3(x, y,0)*shootSpeed; //��������

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


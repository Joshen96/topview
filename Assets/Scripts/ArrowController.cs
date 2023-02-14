using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 1.0f;
    void Start()
    {
        Destroy(this.gameObject, deleteTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.transform); //ȭ�쿡������ �������� ��������
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

    }
}
    

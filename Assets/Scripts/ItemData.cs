using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    arrow,
    key,
    lift,
}
public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (type == ItemType.key)
            {
                ItemKeeper.haskeys += count;

            }
            else if(type==ItemType.arrow)
            {
                ItemKeeper.hasArrows += count*3;


            }
            else if (type == ItemType.lift)
            {
                if (PlayerController.hp < 3)
                {
                    PlayerController.hp++;
                }
            }
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Rigidbody2D rigi = GetComponent<Rigidbody2D>();

            rigi.gravityScale = 2.5f;
            rigi.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            Destroy(gameObject,1);

        }
    }
}

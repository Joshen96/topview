using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManger : MonoBehaviour
{
    public Image hpImage;
    public Text txtArrow;
    public Text txtkey;

    public Sprite life_0;
    public Sprite life_1;
    public Sprite life_2;
    public Sprite life_3;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtkey.text = ItemKeeper.haskeys.ToString();
        txtArrow.text = ItemKeeper.hasArrows.ToString();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (PlayerController.hp == 1)
            {
                hpImage.sprite = life_1;
            }
            else if (PlayerController.hp == 2)
            {
                hpImage.sprite = life_2;
            }
            else if (PlayerController.hp == 3)
            {
                hpImage.sprite = life_3;
            }
            else
            {
                hpImage.sprite = life_0;
            }
        }

    }
}

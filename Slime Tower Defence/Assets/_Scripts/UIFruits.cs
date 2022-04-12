using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIFruits : MonoBehaviour
{
    public Text scoreR;

    void Start()
    {
        GameObject red_text = GameObject.Find("Red_Text");
        scoreR = red_text.GetComponent<Text>();
        scoreR.text = "0";
    }
    
    void Update()
    {

    }

    void OnCollision(Collision coll)
    {
        GameObject collidedwith = coll.gameObject;
        if(collidedwith.tag == "Item")
        {

        }

        int score = int.Parse(scoreR.text);
        score += 1;
        //scoreR.text = score.ToString;
    }
}
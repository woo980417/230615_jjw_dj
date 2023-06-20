using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public GameObject player;

    int step = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(step == 1)
        {
            if (player.transform.position.x <= -2f)
            {
                step = 0;
            }
            else
            {
                player.transform.position -= new Vector3(10 * Time.deltaTime, 0, 0);
            }
        }
        
        if(step == 2)
        {
            if (player.transform.position.x >= 2f)
            {
                step = 0;
            }
            else 
            { 
                player.transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
            }
        }
    }

    public void LeftMove()
    {
        step = 1;
    }

    public void RightMove()
    {
        step = 2;
    }
}

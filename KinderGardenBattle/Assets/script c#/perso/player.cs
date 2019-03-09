using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    // public ClasseduPerso Perso 
    public float speed;
    public float life = 100;
    public bool is_target_player;//     camera
    private bool premiertours = true;
     
    
    public int playerNumber;



    private  void mouvementUp()
    {
        
       transform.Translate(new Vector3(50 * speed, 0f, 0f)); // mouvement
            
        
    }

    private void mouvementdown()
    {
        transform.Translate(new Vector3(10*speed, 0f, 0f)); // mouvement
    }

    private void mouvementleft()
    {
        transform.Translate(new Vector3(0f , 0f, -20*speed)); // mouvement
    }

    private void mouvementright()
    {
        transform.Translate(new Vector3(0f, 0f, 50*speed)); // mouvement
    }
    // Update is called once per frame
    void Update()
    {
        if (is_target_player)
        {
            if (life <= 0)
            {
                Destroy(this);
            }
            else
            {
                if (premiertours)
                    premiertours = false;
                while (!Input.anyKey)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                        mouvementUp();
                    if (Input.GetKey(KeyCode.DownArrow))
                        mouvementdown();
                    if (Input.GetKey(KeyCode.LeftArrow))
                        mouvementleft();
                    if (Input.GetKey(KeyCode.RightArrow))
                        mouvementright();
                   
                }
                is_target_player = false;
              

            }
        }
        float horizontal = 1;
        float vertical = 1;
        transform.Translate(new Vector3(horizontal * speed, 0f, vertical * speed)); // mouvement
        
    }
}

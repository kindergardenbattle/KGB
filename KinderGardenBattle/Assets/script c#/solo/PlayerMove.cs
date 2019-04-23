﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMove : TacticsMove
{
    private bool la_seboul;
    public bool cible;


    void Start()
    {
        Init();
        la_seboul = false;
    }

    public void button()
    {
        la_seboul = !la_seboul;
    }
    void Update()
    {
        if (GameManagerSolo.TeamTurn==GameManagerSolo.Team.Red)
        {
            return;
        }
        //if (!la_seboul)
        //{
        //	return;	
        //}
        if (GameManagerSolo.TeamTurn == PlayerCaracteristique.TeamJoueur)
        {

            Debug.DrawRay(transform.position, transform.forward);



            anim.SetBool("Déplacement", moving);
            if (!moving)
            {
                if (GameManagerSolo.Move_Button && !GameManagerSolo.ATK_Button)
                {
                    FindSelectableTiles(gameObject.GetComponent<Perso_Generique>().Pm); //appelle les fonction si ça bouge pas 
                   CheckMouse();   
                }

                if (!GameManagerSolo.Move_Button && GameManagerSolo.ATK_Button)
                {
                    FindSelectableTiles(gameObject.GetComponent<Perso_Generique>().Distance);
                }
                
            }
            if (!moving && DebutTour == false)
            {
                return;
            }
        }
    }

    private void LateUpdate()
    {
        if (GameManagerSolo.TeamTurn== GameManagerSolo.Team.Red)
        {
            return;
        }
        if (moving)
        {
            Move();
            DebutTour = false; 
        }
            

    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile") // verifie que l'on tape une case 
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}


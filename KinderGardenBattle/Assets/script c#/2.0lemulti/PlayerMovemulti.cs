using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMovemulti : TacticsMove
{
    private bool la_seboul;
    public GameObject CibleRotate;
    public bool cible;
    public bool hasmooved;

    void Rotate()
    {
        GameObject[] listperso = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject VARIABLE in listperso)
        {
            if ((this.transform.position.x - VARIABLE.transform.position.x < 1.5 &&
                 transform.position.z - VARIABLE.transform.position.z < 1.5) &&
                (this.transform.position.x - VARIABLE.transform.position.x > -1.5 &&
                 transform.position.z - VARIABLE.transform.position.z > -1.5))
            {
                //Debug.Log("oui");
                CibleRotate = VARIABLE;
                transform.LookAt(CibleRotate.transform);
            }
        }
        //Debug.Log("le joueur: "+this.name+" a rotate");

    }

    void Start()
    {
        Init();
        la_seboul = false;
        hasmooved = false;
        CibleRotate = null;
    }

    public void button()
    {

        la_seboul = !la_seboul;
    }

    void Update()
    {
        Debug.Log(gameObject.name);
        if (GameManagermulti.TeamTurn == GameManagermulti.Team.Red)
        {
            Rotate();
            GetCurrentTile();
        }
        
        if (GameManagermulti.NomDuJoeurVersUneBool(gameObject.name))
        {
            if (GameManagermulti.has_change_classe)
            {
                hasmooved = true;
                gameObject.GetComponent<Perso_Generiquemulti>().int_to_classe(GameManagermulti.futur_classe);
                GameManagermulti.has_change_classe = false;
            }

            if (!GameManagermulti.Move_Button && !GameManagermulti.ATK_Button)
            {

                Resetalltiles();
                GetCurrentTile();
            }

            if (GameManagermulti.TeamTurn == GameManagermulti.Team.Red)
            {
                Rotate();
            }

            if (hasmooved == false)
            {


                Debug.DrawRay(transform.position, transform.forward);



                anim.SetBool("Déplacement", moving);
                if (!moving)
                {

                    if (GameManagermulti.Move_Button && !GameManagermulti.ATK_Button)
                    {
                        FindSelectableTiles(gameObject.GetComponent<Perso_Generiquemulti>().Pm); //appelle les fonction si ça bouge pas 
                        CheckMouse();
                        Rotate();
                    }
                }

                if (!moving && DebutTour == false)
                {
                    hasmooved = true;
                    Resetalltiles();
                    DebutTour = true;
                }
            }

            if (!GameManagermulti.Move_Button && GameManagermulti.ATK_Button)
            {
                Rotate();
                FindSelectableTiles(gameObject.GetComponent<Perso_Generiquemulti>().Distance);
            }

        }

        else
        {
            GetCurrentTile();
            Rotate();
            return;
        }
    }

    private void LateUpdate()
    {
        if (!GameManagermulti.Move_Button || GameManagermulti.TeamTurn == GameManagermulti.Team.Red)
        {
            moving = false;
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


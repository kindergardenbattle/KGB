﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_manager_multi : Generale_Attaque_multi
{
    public bool Want_to_move = false;
    public bool is_turn;
    public bool ancient_turn;


    // Start is called before the first frame update
    void Start()
    {
        Init();//init de tactics move
        is_turn = PhotonNetwork.IsMasterClient;//a modifier si on veut plus que deux joueurs
        ancient_turn = is_turn;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (is_turn != ancient_turn)
        {
            move = max_move;
            ancient_turn = is_turn;
        }
        if (is_turn)
        {
            if (Want_to_fight)
                GetTarget(10);
            Debug.DrawRay(transform.position, transform.forward);
            
            if (!moving && Want_to_move)
            {
                FindSelectableTiles(); //appelle les fonction si ça bouge pas 
                CheckMouse();
            }
        }
        if (moving)
            Move();
    }

    public void GetTarget(int atk)
    {

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("debut get target");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
                {
                    GameObject npc = hit.transform.gameObject;                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    Player_manager_multi ennemi = npc.GetComponent<Player_manager_multi>();
                    if (ennemi.is_turn!=is_turn)
                    { 
                        Perso_Generique cara_cible = npc.GetComponent<Perso_Generique>();                    //cara_cible.SetClasse(cara_cible.classe);
                        Debug.Log(cara_cible.ClasseToString());                    //Debug.Log("cible acquise :" +cara_cible.ClasseToString());
                        Debug.Log(cara_cible.Hp);
                        cara_cible.NewPV(atk);
                        Debug.Log(cara_cible.Hp);                    //return cara_cible;
                    }
                }
            }            //return null;
        }        //return null;
    }
}

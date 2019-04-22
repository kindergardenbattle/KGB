using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_manager_multi : Generale_Attaque_multi
{
    public bool Want_to_move;
    public bool is_turn;
    public bool ancient_turn;
    public bool has_attack;
    public bool distance;
    public Tile current;
    public GameObject joueur;


    // Start is called before the first frame update
    void Start()
    {
        Init();//init de tactics move
        if (photonView.IsMine)
            is_turn = PhotonNetwork.IsMasterClient;//a modifier si on veut plus que deux joueurs
        else
            is_turn = false;
        ancient_turn = is_turn;
        has_attack = false;
        Want_to_move = false;
        joueur = GameObject.FindGameObjectWithTag("Player");//todo faudra revoir quand on aura plus de perso
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Déplacement", moving);
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (is_turn != ancient_turn)
        {
            move = max_move;
            ancient_turn = is_turn;
            has_attack = false;
        }
        if (is_turn)
        {
            if (Want_to_fight)
            {
                GetCurrentTile();
                current = GetTargetTile(joueur);
                distance = current.checkporté();
                GetTarget(10);
            }
            Debug.DrawRay(transform.position, transform.forward);
            
            if (!moving && Want_to_move)
            {
                FindSelectableTiles(); //appelle les fonction si ça bouge pas 
                CheckMouse();
            }
        }
        if (moving)
            Move();
        else
        {
            if (move == 0)
            {
                Want_to_move = false;
                Resetalltiles();
            }            
        }

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
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player") && (distance))
                {
                    GameObject npc = hit.transform.gameObject;                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    Player_manager_multi ennemi = npc.GetComponent<Player_manager_multi>();               
                    if (ennemi.is_turn!=is_turn)
                    {
                        Perso_Generique_multi cara_cible = npc.GetComponent<Perso_Generique_multi>();
                        cara_cible.NewPV(atk);              //return cara_cible;
                        has_attack = true;
                        Want_to_fight = false;
                    }
                }
            }            //return null;
        }        //return null;
    }
}

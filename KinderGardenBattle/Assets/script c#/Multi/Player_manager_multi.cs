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
    public bool atkable;


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
            has_attack = false;
        }
        if (is_turn)
        {
            if (Want_to_fight)
            {
                Perso_Generique_multi classe = gameObject.GetComponent<Perso_Generique_multi>();
                GetCurrentTile();
                Tile current = GetTargetTile(gameObject);
                current.triplepute = true; // permet de differencier la case où se situt le perso d'une case current 
                atkable = current.checkporté(current, classe.Distance, false);
                GetTarget((int) classe.Atk);
                current.triplepute = false;
            }
            
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
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player")&& (atkable))
                {
                    GameObject npc = hit.transform.gameObject;                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    Player_manager_multi ennemi = npc.GetComponent<Player_manager_multi>();
                    Debug.Log(is_turn);
                    Debug.Log(ennemi.is_turn);                    
                    if (ennemi.is_turn!=is_turn)
                    {
                        Perso_Generique_multi cara_cible = npc.GetComponent<Perso_Generique_multi>();                    //cara_cible.SetClasse(cara_cible.classe);
                        Debug.Log(cara_cible);
                        Debug.Log(cara_cible.ClasseToString());                    //Debug.Log("cible acquise :" +cara_cible.ClasseToString());
                        Debug.Log(cara_cible.Hp);
                        cara_cible.NewPV(atk);
                        Debug.Log(cara_cible.Hp);                    //return cara_cible;
                        has_attack = true;
                        Want_to_fight = false;
                    }
                }
            }            //return null;
        }        //return null;
    }
}

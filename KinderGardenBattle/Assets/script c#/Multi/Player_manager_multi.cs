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
    public bool has_move;


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
        has_move = false;
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
            has_move = false;
        }
        if (is_turn && !moving)
        {
             //appelle les fonction si ça bouge pas 
            if (Want_to_move)
            {
                FindSelectableTiles(move);
                CheckMouse();
            }
            if (Want_to_fight)
            {
                FindSelectableTiles(gameObject.GetComponent<Perso_Generique_multi>().Distance);
                //Debug.Log(is_turn);
            }
        }
        
        /*if (!Want_to_move && move == 0 && !has_move)
        {
            Want_to_move = false;
            Resetalltiles();
            has_move = true;
        }*/
              
    }
    private void LateUpdate()
    {
        if (moving)
        {
            //Resetalltiles();
            Move();
        }
        if (!is_turn || moving || photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Want_to_fight)
        {
            Perso_Generique_multi classe = gameObject.GetComponent<Perso_Generique_multi>();
            Tile current = GetTargetTile(gameObject);
            GetCurrentTile();
            current.triplepute = true;
            atkable = current.checkporté(current, classe.Distance, false);
            GetTarget((int)classe.Atk);
            current.triplepute = false;
        }
    }

    public void GetTarget(int atk)
    {

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("debut get target");
            Debug.Log(atkable);
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

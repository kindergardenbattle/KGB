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
            is_turn = !PhotonNetwork.IsMasterClient;
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
        if (!Multi.Game_Manager.NomDuJoeurVersUneBool(gameObject.name))
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
            if (Multi.Game_Manager.has_change_classe)
            {
                move = 0;
                has_attack = true;
                gameObject.GetComponent<Perso_Generique_multi>().int_to_classe(Multi.Game_Manager.futur_classe);
                Multi.Game_Manager.has_change_classe = false;
            }
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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (!Multi.Game_Manager.NomDuJoeurVersUneBool(gameObject.name))
        {
            return;
        }       
        anim.SetBool("Déplacement", moving);
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
        string attaque;
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))//&& (atkable))
                {
                    GameObject npc = hit.transform.gameObject;//manager_cible= npc.GetComponent<GameManagerSolo>();
                    Player_manager_multi ennemi = npc.GetComponent<Player_manager_multi>();
                    if (ennemi.is_turn!=is_turn)
                    {
                        Perso_Generique_multi classe = gameObject.GetComponent<Perso_Generique_multi>();
                        if (classe.Distance == 1)
                        {
                            switch (classe.classe)
                            {
                                case Perso_Generique_multi.Classe.NINJA:
                                    attaque = "épée 1 main";
                                    break;
                                case Perso_Generique_multi.Classe.TANK:
                                    attaque = "brute";
                                    break;
                                case Perso_Generique_multi.Classe.PIRATE:
                                    attaque = "épée 1 main";
                                    break;
                                case Perso_Generique_multi.Classe.HEALER:
                                    attaque = "malette";
                                    break;
                                default:
                                    attaque = "epee 2 main";
                                    break;
                            }
                            anim.SetTrigger(attaque);
                        }
                        else
                        {
                            switch (classe.classe)
                            {
                                case Perso_Generique_multi.Classe.PIRATE:
                                    attaque = "flingue";
                                    break;
                                case Perso_Generique_multi.Classe.MAGE:
                                    attaque = "Baton_Magique";
                                    break;
                                default:
                                    attaque = "lance pierre";
                                    break;
                            }
                            anim.SetTrigger(attaque);
                        }
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
﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class Generale_Attaquemulti : TacticsMove
{
    //public GameManagerSolo manager_cible;    
    //public GameManagerSolo manager_joueur;
    //public List<Perso_Generique> cara_joueur = new List<Perso_Generique>();
    public bool atkable;
    public bool hasattacked;
    public bool bascule = false;
    public bool bascule2 = true;
    public Tile current;
    public GameObject joueur;
    public bool boolquichamboule;
    public Perso_Generiquemulti classe;




    void Start()
    {
        Init();
        atkable = false;
        hasattacked = false;

    }


    public void GetTarget(int atk)
    {
        string attaque = "flingue";
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("debut get target");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {

                if (hit.transform != null && hit.transform.gameObject.CompareTag("NPC") && (atkable))


                {


                    GameObject npc = hit.transform.gameObject;



                    if (classe.Distance == 1)
                    {
                        switch (classe.classe)
                        {
                            case Perso_Generiquemulti.Classe.NINJA:
                                attaque = "épée 1 main";
                                break;
                            case Perso_Generiquemulti.Classe.TANK:
                                attaque = "brute";
                                break;
                            case Perso_Generiquemulti.Classe.PIRATE:
                                attaque = "épée 1 main";
                                break;
                            case Perso_Generiquemulti.Classe.HEALER:
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
                            case Perso_Generiquemulti.Classe.PIRATE:
                                attaque = "flingue";
                                break;
                            case Perso_Generiquemulti.Classe.MAGE:
                                attaque = "Baton_Magique";
                                break;
                            default:
                                attaque = "lance pierre";
                                break;
                        }
                        anim.SetTrigger(attaque);
                    }
                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    Perso_Generiquemulti cara_cible = npc.GetComponent<Perso_Generiquemulti>();
                    //cara_cible.SetClasse(cara_cible.classe);
                    Debug.Log(cara_cible.ClasseToString());
                    Debug.Log("cible acquise :" + cara_cible.ClasseToString());
                    Debug.Log(cara_cible.Hp);

                    cara_cible.NewPV(atk);
                    hasattacked = true;
                    Debug.Log(cara_cible.Hp);
                    //return cara_cible;
                }
            }
            //return null;
        }
        //return null;




    }
    public Perso_Generiquemulti SelectionPerso(bool boolen)
    {

        if (Input.GetMouseButtonUp(0) && boolen)
        {
            int penis = 1;
            Debug.Log("bruh");
            bascule = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                Debug.Log("hit or miss");
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
                {
                    Debug.Log("bite de cheval ");
                    GameObject joueur = hit.transform.gameObject;
                    if (joueur.GetComponent<Perso_Generique>() == null)
                    {
                        return joueur.GetComponent<Perso_Generiquemulti>();
                    }
                    Perso_Generiquemulti cara_joueur = joueur.GetComponent<Perso_Generiquemulti>();
                    if (cara_joueur != null && cara_joueur.vivant)
                    {
                        penis += 1;
                        Debug.Log(penis);
                        Debug.Log("joueur hit :" + cara_joueur.ClasseToString());
                        boolen = true;
                        cara_joueur.selection = true;
                        return cara_joueur;
                    }
                }
                Debug.Log("vagin");
            }
        }

        return null;
    }

    public void Button()
    {
        boolquichamboule = !boolquichamboule;
    }
    public bool boolene = true;
    private void LateUpdate()
    {
        if (GameManagermulti.TeamTurn == GameManagermulti.Team.Red)
        {
            hasattacked = false;
            return;
        }
        //if (!boolquichamboule)
        // {
        //     return;
        //  }
        if (moving)
        {
            return;
        }
        if (!GameManagermulti.ATK_Button)
        {
            return;
        }

        if (this.tag == "Player")
        {
            if (GameManagermulti.NomDuJoeurVersUneBool(this.name) && hasattacked == false)
            {
                joueur = GameObject.FindGameObjectWithTag("NPC");
                classe = gameObject.GetComponent<Perso_Generiquemulti>();

                boolquichamboule = false;
                GetCurrentTile();
                current = GetTargetTile(joueur);
                current.triplepute = true; // permet de differencier la case où se situt le perso d'une case current 

                atkable = true;//current.checkporté(current,classe.Distance,false);

                GetTarget((int)classe.Atk);
                Debug.Log("febhfbdsjbfdjhsbfdhjsfbdjhbfdqksfbdsqjfbdsqfbsqkbfdskj");
                Debug.Log(classe.classe);
                Debug.Log(classe.Atk);

                current.triplepute = false;
            }
            else
            {
                Resetalltiles();
                GetCurrentTile();
                foreach (Tile VARIABLE in currentTile.adjacencyList)
                {
                    VARIABLE.selectable = false;
                }
            }
        }



    }

}
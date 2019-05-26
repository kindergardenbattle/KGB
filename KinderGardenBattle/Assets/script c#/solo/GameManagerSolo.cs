﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSolo : MonoBehaviour
{
    public static bool Move_Button;
    public static bool ATK_Button;
    public static bool is_in_menu = false;
    public Perso_Generique vis;
    public    GameObject Stat;

    private void Start()
    {
        Move_Button = false;
        ATK_Button = false;
        TeamTurn = Team.Blue;
        J1 = true;
        J2 = J3 = J4 = false;
    }

    public enum Team
    {
        Blue,Red
    }
    
    public   static bool J1;
    public  static bool J2;
    public  static bool J3;
    public  static bool J4;

    public   void QuelJoueur()
    {
        if (J1)
        {
            J1 = J2;
            J2 = true;
        }

        else
        {
            if (J2)
            {
                J2 = !J2;
                J3 = true;
            }
            else
            {
                if (J3)
                {
                    J3 = !J3;
                    J4 = true;
                }
                else
                {
                    J4 = false;
                    J1 = true;
                }
            }
        }
        
    }

    public  static bool   NomDuJoeurVersUneBool(string name)
    {
        switch (name)
        {
                case "1":
                    return J1;
                case "2":
                    return J2;
                case "3":
                    return J3;
                case"4":
                    return J4;
                default:
                    return false;
                
        }
    }
    public static string TeamtoString(Team team)
    {
        switch (team)
        {
                case Team.Blue:
                    return "BLEU";
                case Team.Red:
                    return "RED";
                default:
                    return "ERROR";            
        }
    }
    
    public static Team TeamTurn;
    public static bool booleen = false;

    public void Button_Moove()
    {
       
        Move_Button = !Move_Button;
        ATK_Button = false;
        Debug.Log("MOVE " +Move_Button);
        Stat.SetActive(false);
    }
    public void Button_ATK()
    {
        ATK_Button = !ATK_Button;
        Move_Button = false;
        Debug.Log("ATK"+ATK_Button);
        Stat.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menu();
        }
        if (!Move_Button && !ATK_Button)
        {
            show_stat();
        }        
    }

    public void show_stat()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                if (hit.transform != null && (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("NPC")))
                {
                    GameObject npc = hit.transform.gameObject;                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    vis = npc.GetComponent<Perso_Generique>();
                    Stat.SetActive(true);
                    Player_Stats s = Stat.GetComponent<Player_Stats>();
                    s.set_player_stats(vis);
                }
            }
        }
    }

    public void menu()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.CompareTag("Button"))
                go.SetActive(is_in_menu);//go.activeSelf
            if (go.CompareTag("Menupause"))
                go.SetActive(!is_in_menu);
            Stat.SetActive(false);
        }
        is_in_menu = !is_in_menu;
    }

    public void quit()
    {
        SceneManager.LoadScene(0);
    }



    public static void SetTeamTurn (Team Color)
    {
        TeamTurn = Color;       
    }

    public void  FinDeTour()
    {
        
       
            TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
            Move_Button = false;
            ATK_Button = false;
            Stat.SetActive(false);
            Debug.Log("Tours de l'equipe : "+ TeamtoString(TeamTurn));

        GameObject[] liste = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(liste.Length);
        foreach (GameObject VARIABLE in liste)
        {
            PlayerMove script = VARIABLE.GetComponent<PlayerMove>();
            script.hasmooved = false;
        }



    }
    


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSolo : MonoBehaviour
{
    public static bool Move_Button;
    public static bool ATK_Button;
    public static bool is_in_menu = false;
    public static bool want_to_change_classe = false;
    public Perso_Generique vis;
    public GameObject Stat;
    public static bool has_change_classe = false;
    public static int futur_classe;

    private void Start()
    {
        Move_Button = false;
        ATK_Button = false;
        TeamTurn = Team.Blue;
        J1 = true;
        J2 = J3 = J4 = false;
        npc1 = true;
        npc2 = npc3 = npc4 = false;
    }

    public enum Team
    {
        Blue,Red
    }
    
    public  static bool J1;
    public  static bool J2;
    public  static bool J3;
    public  static bool J4;
    public static bool npc1;
    public static bool npc2;
    public static bool npc3;
    public static bool npc4;

    public static void QuelNpc()
    {
        if (npc1)
        {
            npc1 = npc2;
            npc2 = true;
        }

        else
        {
            if (npc2)
            {
                npc2 = !npc2;
                npc3 = true;
            }
            else
            {
                if (npc3)
                {
                    npc3 = !J3;
                    npc4 = true;
                }
                else
                {
                    npc4 = false;
                    npc1 = true;
                }
            }
        }
    }

    public static bool NomDuNPCVersUneBool(string name)
    {
        switch (name)
        {
                case "npc1":
                    return npc1;
                case "npc2":
                    return npc2;
                case"npc3":
                    return npc3;
                case"npc4":
                    return npc4;
                default:
                    return false;
        }
    }
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
    public void  setfuturclasse(int i)
    {
        futur_classe = i;
    }

    public void confirmclasse()
    {
        has_change_classe = true;
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
                else
                {
                    if(hit.transform != null && hit.transform.gameObject.CompareTag("Box"))
                    {
                        menuselectionclasse();
                        Debug.Log("sortie de fonction");
                    }
                } 
            }
        }
    }
    public void menuselectionclasse()
    {
        Debug.Log("entree");
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.CompareTag("Button"))
                go.SetActive(want_to_change_classe);//go.activeSelf
            if (go.CompareTag("ChangeClasse"))
                go.SetActive(!want_to_change_classe);
        }
        Stat.SetActive(false);
        want_to_change_classe = !want_to_change_classe;
    }

    public void menu()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            Debug.Log("change clase" + want_to_change_classe);
            if (go.CompareTag("Button"))
                go.SetActive(is_in_menu ?!want_to_change_classe : is_in_menu);//go.activeSelf
            if (go.CompareTag("Menupause"))
                go.SetActive(!is_in_menu);
            if (go.CompareTag("ChangeClasse"))
                go.SetActive(is_in_menu ?want_to_change_classe : is_in_menu);
            
        }
        Stat.SetActive(false);
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

    public static void NPCFinDeTour()
    {
        TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
        Move_Button = false;
        ATK_Button = false;
        Debug.Log("Tours de l'equipe : "+ TeamtoString(TeamTurn));

        GameObject[] liste = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(liste.Length);
        foreach (GameObject VARIABLE in liste)
        {
            PlayerMove script = VARIABLE.GetComponent<PlayerMove>();
            Debug.Log(script.hasmooved);
            if (script.hasmooved = true)
            {
                script.hasmooved = false;
            }
            script.hasmooved = false;
            Debug.Log(script.hasmooved);
        }
    }

    public static string printNPC()
    {
        return "npc1: " + npc1 + " npc2: " + npc2 + " npc3:" + npc3 + " npc4: " + npc4;
    }
    public void  FinDeTour()
    {
        
       
            TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
            Move_Button = false;
            ATK_Button = false;
            Stat.SetActive(false);
            Debug.Log("Tours de l'equipe : "+ TeamtoString(TeamTurn));

        GameObject[] liste = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject VARIABLE in liste)
        {
            PlayerMove script = VARIABLE.GetComponent<PlayerMove>();
            
            if (script.hasmooved = true)
            {
                script.hasmooved = false;
            }
            script.hasmooved = false;
            
        }



    }
    


}
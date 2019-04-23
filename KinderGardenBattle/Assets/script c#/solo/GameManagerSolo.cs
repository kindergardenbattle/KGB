using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSolo : MonoBehaviour
{
    public static bool Move_Button;
    public static bool ATK_Button;
    public static bool is_in_menu = false;
    public Perso_Generique vis;
    public GameObject Stat;

    private void Start()
    {
        Move_Button = false;
        ATK_Button = false;
        TeamTurn = Team.Blue;
    }

    public enum Team
    {
        Blue,Red
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
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 25.0f))
                {
                    if (hit.transform != null && (hit.transform.gameObject.CompareTag("Player")|| hit.transform.gameObject.CompareTag("NPC")))
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

    public void FinDeTour()
    {
        TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
        Move_Button = false;
        ATK_Button = false;
        Stat.SetActive(false);
        Debug.Log("Tours de l'equipe : "+ TeamtoString(TeamTurn));     
    }
    


}
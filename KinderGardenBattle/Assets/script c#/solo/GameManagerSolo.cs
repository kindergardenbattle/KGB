using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSolo : MonoBehaviour
{
    public static bool Move_Button;
    public static bool ATK_Button;
    
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
        Debug.Log("MOVE " +Move_Button);
    }
    public void Button_ATK()
    {
        ATK_Button = !ATK_Button;
        Debug.Log("ATK"+ATK_Button);
    }

    
    
    public static void SetTeamTurn (Team Color)
    {
        TeamTurn = Color;       
    }

    public void FinDeTour()
    {
        TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
        Debug.Log("Tours de l'equipe : "+ TeamtoString(TeamTurn));     
    }
    


}
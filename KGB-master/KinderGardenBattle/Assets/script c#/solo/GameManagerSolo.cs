using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSolo : MonoBehaviour
{
    
    public enum Team
    {
        Blue,Red
        
    }

    public  static  int compteur_tours = 0;
    public static Team TeamTurn;
    public static bool booleen = false;

    public static void SetTeamTurn (Team Color)
    {
        TeamTurn = Color;       
    }

    public  static void FinDeTours()
    {
        if (compteur_tours <1)
        {
            TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
            compteur_tours += 1; 
        }   
        
    }


}
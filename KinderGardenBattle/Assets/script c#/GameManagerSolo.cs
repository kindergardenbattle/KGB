using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSolo : MonoBehaviour
{
    
    public enum Team
    {
        Blue,Red
        
    }

    public static Team TeamTurn;

    public static void SetTeamTurn (Team Color)
    {
        TeamTurn = Color;
       
    }

    public  static void FinDeTours()
    {
        Debug.Log((TeamTurn == Team.Blue) ?"Blue":"Red");
        
        TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
    }


}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiMoove : MonoBehaviour
{
    public static bool MvmFini = false; // le mouvement est il finie ?

    public static void TurnNpc()
    {
       
           return;
             
            
        
      
    }
    void Update () 
    {
         if (GameManagerSolo.TeamTurn == EnemieCaracteristique.TeamEnemie)
        {
            TurnNpc();
        }
    }
}
// /!\  en progrés bande de pute /!\
   // le mouvement du npc est gerer depuis player moove, mais je rangerais aprés whalla

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSolo : MonoBehaviour
{
    public enum Team
    {
        Red,
        Blue
    }

    public Team TeamTurn;

    public void SetTeamTurn(Team Color)
    {
        TeamTurn = Color;
    }

    public void FinDeTours()
    {
        TeamTurn = (TeamTurn == Team.Red) ? Team.Blue : Team.Red;
    }


}
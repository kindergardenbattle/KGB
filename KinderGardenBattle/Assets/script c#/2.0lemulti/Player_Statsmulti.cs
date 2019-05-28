using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Statsmulti : MonoBehaviour
{
    public TextMeshProUGUI Classe;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Mp;
    public TextMeshProUGUI Def;
    public TextMeshProUGUI Atk;
    public TextMeshProUGUI Dis;

    public void set_player_stats(Perso_Generiquemulti p)
    {
        Classe.text = "Classe :\n" + p.ClasseToString();
        Hp.text = "Hp :\n" + p.Hp + "/" + p.Max_hp;
        Mp.text = "Mp :\n" + p.Pm;
        Def.text = "Def :\n" + p.Def;
        Atk.text = "Atack :\n" + p.Atk;
        Dis.text = "Range :\n" + p.Distance;
    }
}

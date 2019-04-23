using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats_multi : MonoBehaviour
{
    public TextMeshProUGUI Classe;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Mp;
    public TextMeshProUGUI Def;

    public void set_player_stats(Perso_Generique_multi p)
    {
        Classe.text = "Classe :\n" + p.ClasseToString();
        Hp.text = "Hp :\n" + p.Hp+"/"+p.Max_hp;
        Mp.text = "Mp :\n" + p.Pm;
        Def.text = "Def :\n" + p.Def;


    }

}

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
        Classe.text = "Classe : " + p.ClasseToString();
        Hp.text = "Hp : " + p.Hp;
        Mp.text = "Mp : " + p.Pm;
        Def.text = "Def : " + p.Def;


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour
{
    public  static Perso_Generique Klasse = new Perso_Generique();
    private  static Perso_Generique.Classe Tpe = Perso_Generique.Classe.GUERRIER;
    private Collider collider;

    public static double life = Klasse.Hp;
    // Update is called once per frame
    void Update()
    {
       
    }

    private void PrintName(GameObject go)
    {
        print(go.tag);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_distance : Generale_Attaque
{
    
      public  Perso_Generique classe;
    public GameObject perso;

    private void Start()
    {
        perso = GameObject.FindGameObjectWithTag("Player");
        classe = perso.GetComponent<Perso_Generique>();

    }

    public bool verification(Perso_Generique persoGenerique)
    {
        return (classe.Distance != 1);
    }
    private void Update()
    {
        
    }
}

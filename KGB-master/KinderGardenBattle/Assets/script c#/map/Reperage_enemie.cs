using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reperage_enemie : MonoBehaviour
{
    public static GameObject Map;

    public void MAp ()
    {
      Map=  GameObject.FindWithTag("MAP"); 
    }
    public NPC chibre;

    public Collision collision;

    public void _collision()
    {
        if (Map.GetComponent<Collision>() != null)
        {
            collision=Map.GetComponent<Collision>();
        }
        else
        {
            Debug.Log("rien");
        }
    }
        
    // Update is called once per frame
    public  List<GameObject> liste_des_collisions = new List<GameObject>();

    public void detection_collision()
    {
        foreach (GameObject objet  in collision)
        {
            if (objet.CompareTag("NPC"))// normalment y en a qu'un 
            {
                liste_des_collisions.Add(objet);
                Debug.Log("enemie");
            }
        }

        chibre = liste_des_collisions[0].GetComponent<NPC>();
    }

    private void Awake()
    {
        _collision();
        MAp();
    }

    void Update()
    {
        detection_collision();
        
    }
}

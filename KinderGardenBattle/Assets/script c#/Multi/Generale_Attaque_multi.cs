﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generale_Attaque_multi : TacticsMove_multi
{
    public bool Want_to_fight = false;

    public Perso_Generique SelectionPerso(bool boolen)
    {

        if (Input.GetMouseButtonUp(0) && boolen == true)
        {
            int penis = 1;
            Debug.Log("bruh");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                Debug.Log("hit or miss");
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
                {
                    Debug.Log("bite de cheval ");
                    GameObject joueur = hit.transform.gameObject;
                    if (joueur.GetComponent<Perso_Generique>() == null)
                    {
                        return joueur.GetComponent<Perso_Generique>();
                    }
                    Perso_Generique cara_joueur = joueur.GetComponent<Perso_Generique>();
                    if (cara_joueur != null && cara_joueur.vivant)
                    {
                        penis += 1;
                        Debug.Log(penis);
                        Debug.Log("joueur hit :" + cara_joueur.ClasseToString());
                        boolen = true;
                        cara_joueur.selection = true;
                        return cara_joueur;
                    }
                }
                Debug.Log("vagin");
            }
        }

        return null;
    }
}

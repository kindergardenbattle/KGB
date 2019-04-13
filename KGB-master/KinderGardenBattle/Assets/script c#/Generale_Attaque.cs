using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class Generale_Attaque : Perso_Generique
{    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.CompareTag("NPC"))
                {
                    GameObject npc = hit.transform.gameObject;
                }

            }
            
        }
    }
}
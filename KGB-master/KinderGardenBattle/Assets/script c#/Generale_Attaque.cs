using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class Generale_Attaque : MonoBehaviour
{   public GameManagerSolo manager_cible;
    public Perso_Generique cara_cible;
    public GameManagerSolo manager_joueur;
    public Perso_Generique cara_joueur;
    public bool distance = false;
    public void GetTarget()
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
                    manager_cible= npc.GetComponent<GameManagerSolo>();
                    cara_cible = npc.GetComponent<Perso_Generique>();
                    Debug.Log("cible acquise :" +npc.name);
                }

            }
            
        }
    }

    public void SelectionPerso()
    {
        bool boolen = false;
       

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
                    {
                        GameObject joueur = hit.transform.gameObject;
                        manager_joueur = joueur.GetComponent<GameManagerSolo>();
                        cara_joueur = joueur.GetComponent<Perso_Generique>();
                        if (cara_joueur!=null)
                        {
                            
                            Debug.Log("joueur hit :" + cara_joueur.ClasseToString());
                            boolen = true;
                            cara_joueur.selection = true;
                        }
                        else
                        {
                            Debug.Log("penis");
                        }
                            
                        
                       
                    }
                    Debug.Log("vagin");
                }
            }
        
    }

    

    public void ATK_joueur_cqc()
    {
        if (cara_cible != null && cara_joueur != null)
        { 
            double degat =  cara_cible.Hp - cara_cible.Def * cara_joueur.Atk;
            // declencher l'animation
            cara_cible.Hp -= degat;
            cara_cible.Verification();
            Debug.Log("hp :"+ cara_cible.Hp);
            
            // if dead => animation puis destroy object
        }
        
    }

    
}
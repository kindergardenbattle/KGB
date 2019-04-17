using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class Generale_Attaque : MonoBehaviour
{
    //public GameManagerSolo manager_cible;    
    //public GameManagerSolo manager_joueur;
    //public List<Perso_Generique> cara_joueur = new List<Perso_Generique>();
    public bool distance = false;
    public bool bascule = false;
    public bool bascule2 = true;

    
   
    
    
    public void GetTarget(int atk)
    {
        
        if (Input.GetMouseButtonUp(0) )
        {
            Debug.Log("debut get target");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.CompareTag("NPC"))
                {
                    GameObject npc = hit.transform.gameObject;
                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    Perso_Generique cara_cible = npc.GetComponent<Perso_Generique>();
                    //cara_cible.SetClasse(cara_cible.classe);
                    Debug.Log(cara_cible.ClasseToString());
                    //Debug.Log("cible acquise :" +cara_cible.ClasseToString());
                    Debug.Log(cara_cible.Hp);
                    cara_cible.NewPV(atk);
                    Debug.Log(cara_cible.Hp);
                    //return cara_cible;
                }
            }
            //return null;
        }
        //return null;




    }
    public Perso_Generique SelectionPerso(bool boolen)
    {

        if (Input.GetMouseButtonUp(0) && boolen == true)
        {
            int penis = 1;
            Debug.Log("bruh");
            bascule = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                Debug.Log("hit or miss");
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
                {
                    Debug.Log("bite de cheval ");
                    GameObject joueur = hit.transform.gameObject;
                    if (joueur.GetComponent<Perso_Generique>()==null)
                    {
                        return joueur.GetComponent<Perso_Generique>();
                    }
                    Perso_Generique cara_joueur = joueur.GetComponent<Perso_Generique>();
                    if (cara_joueur!=null && cara_joueur.vivant)
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
    public bool boolene = true;
    private void Update()
    {
        GetTarget(10);
    }
}

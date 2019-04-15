using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class Generale_Attaque : MonoBehaviour
<<<<<<< HEAD
{
    public GameManagerSolo manager_cible;

=======
{   public GameManagerSolo manager_cible;
    
>>>>>>> CHIBREDESINGE
    public GameManagerSolo manager_joueur;
    public List<Perso_Generique> cara_joueur = new List<Perso_Generique>();
    public bool distance = false;
    public bool bascule = false;
    public bool bascule2 = true;
<<<<<<< HEAD




    public Perso_Generique GetTarget(int atk)
    {

        bool boolen = true;
        if (Input.GetMouseButtonUp(0))
=======
    
   
    
    
    public Perso_Generique GetTarget(int atk)
    {
        
        bool boolen = true;
        if (Input.GetMouseButtonUp(0) )
>>>>>>> CHIBREDESINGE
        {
            Debug.Log("5");
            Debug.Log("bite");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.CompareTag("NPC"))
                {
                    GameObject npc = hit.transform.gameObject;
<<<<<<< HEAD
                    manager_cible = npc.GetComponent<GameManagerSolo>();
                    Perso_Generique cara_cible = npc.GetComponent<Perso_Generique>();
                    cara_cible.SetClasse(cara_cible.classe());
                    Debug.Log(cara_cible.ClasseToString());
                    Debug.Log("cible acquise :" + cara_cible.ClasseToString());
=======
                    manager_cible= npc.GetComponent<GameManagerSolo>();
                     Perso_Generique cara_cible = npc.GetComponent<Perso_Generique>();
                    cara_cible.SetClasse(cara_cible.classe);
                    Debug.Log(cara_cible.ClasseToString());
                    Debug.Log("cible acquise :" +cara_cible.ClasseToString());
>>>>>>> CHIBREDESINGE
                    Debug.Log(cara_cible.Hp);
                    cara_cible.NewPV(atk);
                    Debug.Log(cara_cible.Hp);
                    return cara_cible;
                }

            }

            return null;


        }

        return null;




    }
<<<<<<< HEAD
    public Perso_Generique SelectionPerso(bool boolen)
    {


=======
    public Perso_Generique SelectionPerso( bool boolen)
    {
        
        
>>>>>>> CHIBREDESINGE

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
<<<<<<< HEAD
                    if (joueur.GetComponent<Perso_Generique>() == null)
=======
                    if (joueur.GetComponent<Perso_Generique>()==null)
>>>>>>> CHIBREDESINGE
                    {
                        return joueur.GetComponent<Perso_Generique>();
                    }
                    Perso_Generique cara_joueur = joueur.GetComponent<Perso_Generique>();
<<<<<<< HEAD
                    if (cara_joueur != null && cara_joueur.vivant)
=======
                    if (cara_joueur!=null && cara_joueur.vivant)
>>>>>>> CHIBREDESINGE
                    {
                        penis += 1;
                        Debug.Log(penis);
                        Debug.Log("joueur hit :" + cara_joueur.ClasseToString());
                        boolen = true;
                        cara_joueur.selection = true;
                        return cara_joueur;
                    }
<<<<<<< HEAD





=======
                    
                       
                            
                        
                       
>>>>>>> CHIBREDESINGE
                }
                Debug.Log("vagin");
            }
        }

        return null;
    }






<<<<<<< HEAD

    public bool boolene = true;
    private void Update()
    {
        //  GetTarget(10);
=======
    
    public bool boolene = true;
    private void Update()
    {
      //  GetTarget(10);
>>>>>>> CHIBREDESINGE


    }
}
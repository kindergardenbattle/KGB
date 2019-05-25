using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class Generale_Attaque : TacticsMove
{
    //public GameManagerSolo manager_cible;    
    //public GameManagerSolo manager_joueur;
    //public List<Perso_Generique> cara_joueur = new List<Perso_Generique>();
    public bool atkable;
    public bool bascule = false;
    public bool bascule2 = true;
    public Tile current;
    public GameObject joueur;
    public bool boolquichamboule;
    public Perso_Generique classe;



    void Start()
    {
        Init();
        atkable = false;

    }


    public void GetTarget(int atk)
    {

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("debut get target");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f))
            {    Debug.Log("hit");
                Debug.Log(atkable);

                if (hit.transform != null && hit.transform.gameObject.CompareTag("NPC")&& (atkable))


                {
                    

                    GameObject npc = hit.transform.gameObject;
                   

                    
                    if (classe.Distance == 1)
                        anim.SetTrigger("epee 2 main");
                    else
                        anim.SetTrigger("lance pierre");
                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    Perso_Generique cara_cible = npc.GetComponent<Perso_Generique>();
                    //cara_cible.SetClasse(cara_cible.classe);
                    Debug.Log(cara_cible.ClasseToString());
                    Debug.Log("cible acquise :" + cara_cible.ClasseToString());
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

        if (Input.GetMouseButtonUp(0) && boolen)
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

    public void Button()
    {
        boolquichamboule = !boolquichamboule;
    }
    public bool boolene = true;
    private void LateUpdate()
    {
        if (GameManagerSolo.TeamTurn== GameManagerSolo.Team.Red)
        {
            return;
        }
        //if (!boolquichamboule)
        // {
        //     return;
        //  }
        if (moving){
            return;
        }
        else if (!GameManagerSolo.ATK_Button)
        {
            return;
        }
        
        joueur = GameObject.FindGameObjectWithTag("Player");
        classe = joueur.GetComponent<Perso_Generique>();
        
        boolquichamboule = false;
        GetCurrentTile();
        current = GetTargetTile(joueur);
        current.triplepute = true; // permet de differencier la case où se situt le perso d'une case current 
        Debug.Log(atkable);
        Debug.Log("Bite");
        atkable = true;//current.checkporté(current,classe.Distance,false);
        Debug.Log(atkable);
        GetTarget(10);
        current.triplepute = false;

    }

}
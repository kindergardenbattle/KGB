using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;
    public Tile targetTile;
    public Tile current;
    public bool ChrisBool = false;
    private GameObject npc;
    private GameManagerSolo GM;
    public bool findetour;
    public bool DeBuT;
    public TacticsMove Tact;
    public bool atk;
    public GameObject CibleRotate;

     

    // Use this for initialization
    void Start()
    {
        Init();
        anim = GetComponent<Animator>();
        npc = GameObject.FindGameObjectWithTag("Player");
        Tact = this.GetComponent<TacticsMove>();
        Tact.GetCurrentTile();
        ComputeAdjacencyLists(2,currentTile);
        findetour = false;
        DeBuT = true;
        atk = true;
        CibleRotate = null;

    }

    void Rotate()
    {
        double X=this.transform.position.x;
        double Y = this.transform.position.y;
        double Z = this.transform.position.z;
        GameObject[] listperso = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject VARIABLE in listperso)
        {

            if (atk&&(this.transform.position.x -VARIABLE.transform.position.x<1.5 && transform.position.z -VARIABLE.transform.position.z<1.5 )&&(this.transform.position.x -VARIABLE.transform.position.x>-1.5 && transform.position.z -VARIABLE.transform.position.z>-1.5 ))
            {
                CibleRotate = VARIABLE;
                transform.LookAt(CibleRotate.transform);
            }
        }
        
    }
    void ATK()
    {
        bool atk = true;
        double X=this.transform.position.x;
        double Y = this.transform.position.y;
        double Z = this.transform.position.z;
        GameObject[] listperso = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject VARIABLE in listperso)
        {

            if (atk&&(this.transform.position.x -VARIABLE.transform.position.x<1.5 && transform.position.z -VARIABLE.transform.position.z<1.5 )&&(this.transform.position.x -VARIABLE.transform.position.x>-1.5 && transform.position.z -VARIABLE.transform.position.z>-1.5 ))
            {
                CibleRotate = VARIABLE;
                transform.LookAt(CibleRotate.transform);
                anim.SetTrigger("epee 2 main");
                VARIABLE.GetComponent<Perso_Generique>()
                    .NewPV((int) this.GetComponent<Perso_Generique>().Atk);
               
                atk = false;
            }
        }
    }

    // Update is called once per frame
   void Update()
   {
       
       
       if (GameManagerSolo.NomDuNPCVersUneBool(this.name) && findetour==false  && EnemieCaracteristique.TeamEnemie == GameManagerSolo.TeamTurn )
       {           
           findetour = true;
           GetCurrentTile();
           current = GetTargetTile(npc);



           

           
           

           anim.SetBool("Déplacement",moving);

           if (moving == false && DeBuT)
           {

               FindNearestTarget();
               CalculatePath();
               FindSelectableTiles(npc.GetComponent<Perso_Generique>().Pm);
               Move();
               DeBuT = false;


           }
           else
           {
               
               Move();
               
               
              
           }

           if (!moving && !DeBuT&& atk)
           {
               anim.SetBool("Déplacement",moving);
                GetCurrentTile();
               
               if (atk)
               {
                   ATK();
                   atk = false;
               }
               GameManagerSolo.QuelNpc();
               
           }
       }

       if (GameManagerSolo.TeamTurn== GameManagerSolo.Team.Blue)
       {
           Init();
           DeBuT = true;
           findetour = false;
           DeBuT = true;
           atk = true;
           Rotate();
           GameManagerSolo.npc1 = true;
           GameManagerSolo.npc2 = GameManagerSolo.npc3 = GameManagerSolo.npc4 = false;
       }

       else
       {
           findetour = false;
           
           Rotate();
           return;
       }
       

       
      
       

   }
    

    void CalculatePath()
    {
        targetTile = GetTargetTile(target);

        FindPath(targetTile);
        
    }

    
    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }
        
        target = nearest;
        
    }
}

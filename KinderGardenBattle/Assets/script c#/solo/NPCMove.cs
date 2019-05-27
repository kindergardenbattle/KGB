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


    // Use this for initialization
    void Start()
    {
        Init();
        anim = GetComponent<Animator>();
        npc = GameObject.FindGameObjectWithTag("NPC");
        findetour = false;
        DeBuT = true;

    }

    // Update is called once per frame
   void Update()
   {
       Debug.Log(GameManagerSolo.printNPC());
       
       if (GameManagerSolo.NomDuNPCVersUneBool(this.name) && findetour==false  && EnemieCaracteristique.TeamEnemie == GameManagerSolo.TeamTurn )
       {
           
           findetour = false;
           

           GetCurrentTile();
           current = GetTargetTile(npc);



           Debug.DrawRay(transform.position, transform.forward);

           
           

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

           if (!moving && !DeBuT)
           {
               anim.SetBool("Déplacement",moving);
               GameManagerSolo.QuelNpc();
           }
       }
       

       
       else
       {
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

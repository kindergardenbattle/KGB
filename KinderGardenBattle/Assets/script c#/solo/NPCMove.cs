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


    // Use this for initialization
    void Start()
    {
        Init();
        anim = GetComponent<Animator>();
        npc = GameObject.FindGameObjectWithTag("NPC");
        findetour = false;

    }

    // Update is called once per frame
   void Update()
   {

       findetour = false;
       if (findetour)
       {
           moving = false;
       }
        GetCurrentTile();
        current = GetTargetTile(npc);
       
       

        Debug.DrawRay(transform.position, transform.forward);

        if (EnemieCaracteristique.TeamEnemie != GameManagerSolo.TeamTurn)
        {
            return;
        }
        
        anim.SetBool("Déplacement",moving);

        if (moving==false)
        {
            
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles(npc.GetComponent<Perso_Generique>().Pm);
            Move();
            

        }
        else
        {
            Move();
            findetour = true;
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

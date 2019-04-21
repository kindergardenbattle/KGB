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
    

    // Use this for initialization
    void Start()
    {
        Init();
        npc= GameObject.FindGameObjectWithTag("NPC");
          
    }

    // Update is called once per frame
    void Update()
    {    
        GetCurrentTile();
        current = GetTargetTile(npc);
        ChrisBool = current.checkporté();
        
        Debug.DrawRay(transform.position, transform.forward);
        
        if (EnemieCaracteristique.TeamEnemie != GameManagerSolo.TeamTurn)
        {
            return;
        }

        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            Debug.Log("tours ia");
            

        }
        else
        {
            //Move();
            GameManagerSolo.FinDeTours();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Tiles : MonoBehaviour
{
    public bool current = false;

    public bool target = false;

    public bool selectable = false;
    public bool walkable = true;
    public bool visité = false;
    
    public int distance = 0;
    public List<Tiles> adjacente = new List<Tiles>();
    
    // djistra de ses grands mort :
    public bool visited = false;
    public Tiles parent = null;
    

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color= Color.magenta;
            
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color= Color.blue;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color= Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.name = "texture map";
        }
    }

    private void Reset()
    {
    
   current = false;

    target = false;

    selectable = false;
   
     visité = false;
     parent = null;
     distance = 0;
     adjacente.Clear();
       
    }

    public void Neighbours(Vector3 direction,float jumpHeight )
    {
        Reset();
        CheckTile(Vector3.forward,jumpHeight);
        CheckTile(-Vector3.forward,jumpHeight);
        CheckTile(Vector3.left,jumpHeight);
        CheckTile(Vector3.right,jumpHeight);
    }

    public void CheckTile(Vector3 direction, float jumpHeight)
    {Vector3 moitié_porté = new Vector3(0.25f,(1+jumpHeight)/2.0f,0.25f);
        Collider[] collision_de_la_case = Physics.OverlapBox(transform.position + direction, moitié_porté);
        foreach ( Collider VARIABLE in collision_de_la_case)
        {
            Tiles Case = VARIABLE.GetComponent<Tiles>();
            if (Case != VARIABLE && Case.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(Case.transform.position, Vector3.up, out hit, 1))
                {
                    adjacente.Add(Case);
                }
            
        }
        }
    }
}

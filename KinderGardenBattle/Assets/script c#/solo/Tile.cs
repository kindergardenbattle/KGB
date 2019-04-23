using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public bool walkable = true;
	public bool current = false;
	public bool target = false;
	public bool selectable = false;
	public bool triplepute = false;

	public List<Tile> adjacencyList = new List<Tile>(); // utile pour le parcours largeur 


	public bool visited = false;
	public Tile parent = null;
	public int distance = 0;

	public float f = 0;
	public float g = 0;
	public float h = 0;
	public bool utilise = false;
	public bool dans_la_porte = false;

	public bool checkporté(Tile tile, double porte, bool repoonse)
	{
		if (repoonse || porte < 0)
		{
			return repoonse;
		}
        if (tile.current && tile.triplepute ==false)
		{
            repoonse = true;
		}

        //Debug.Log(tile.adjacencyList.Count);
        foreach (Tile VARIABLE in tile.adjacencyList)
		{
			repoonse= repoonse || checkporté(VARIABLE, porte-1, repoonse);			
		}
		return repoonse;
	}

	




void Start ()
{
	triplepute = false;
}

    public void used()
	{
	
	}
	// Update is called once per frame
	void Update () 
	{
			
            if (current)
	        {
	            GetComponent<Renderer>().material.color = Color.magenta;
	        }
	        
            else if(GameManagerSolo.TeamTurn==GameManagerSolo.Team.Red)
            {
	            GetComponent<Renderer>().material.color = Color.white;
            }
            else if (target)
            {
	            GetComponent<Renderer>().material.color = Color.green; 
            }
	        else if (selectable )
	        {
                GetComponent<Renderer>().material.color = Color.red;
	        }
           
	        
	        else if (!GameManagerSolo.Move_Button && !GameManagerSolo.ATK_Button)
            {
	            GetComponent<Renderer>().material.color = Color.white;
            }
			else
	        {
	            GetComponent<Renderer>().material.color = Color.white;
	        }
	    
	    /*else
	    {
		    GetComponent<Renderer>().material.color = Color.white;
	    }*/
	}

    public void Reset()
    {
        adjacencyList.Clear();
	    triplepute = false;
        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target)
    {
        Reset();

        CheckTile(Vector3.forward, jumpHeight, target); // up
        CheckTile(-Vector3.forward, jumpHeight, target);// down
        CheckTile(Vector3.right, jumpHeight, target);// right
        CheckTile(-Vector3.right, jumpHeight, target);//left
    }
	

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents); // cherhce les collisions et les ranges

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}

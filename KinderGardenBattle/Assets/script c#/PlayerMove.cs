using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : TacticsMove
{
    public GameManagerSolo.Team Team;
	public GameObject TeamTurn;
	void Start () 
	{	
		
        Init();
		
	}
	
	void Update () 
	{
	    


	        Debug.DrawRay(transform.position, transform.forward);

	        if (!turn)
	        {
	            return;
	        }

	        if (!moving)
	        {
	            FindSelectableTiles(); //appelle les fonction si ça bouge pas 
	            CheckMouse();
	        }
	        else
	        {
	            Move();
	        }
	    
	}

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile") // verifie que l'on tape une case 
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}

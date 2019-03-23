using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : TacticsMove
{
    
	public bool DebutTour;
	
	void Start ()
	{
		DebutTour = true;
        Init();
		
	}
	
	
	void Update () 
	{


		if(GameManagerSolo.Turn)
		{

			Debug.DrawRay(transform.position, transform.forward);



			if (!moving)
			{
				FindSelectableTiles(); //appelle les fonction si ça bouge pas 
				CheckMouse();
			}
			else
			{
				Move();
				DebutTour = false;

			}
		}

		if (!moving && DebutTour==false)
		{
			GameManagerSolo.Turn = false;
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

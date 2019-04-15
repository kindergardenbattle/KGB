using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMove : TacticsMove
{



    void Start()
    {
        Init();
    }
    void Update()
	{


		if (GameManagerSolo.TeamTurn == PlayerCaracteristique.TeamJoueur)
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

			if (!moving && DebutTour == false)
			{
				GameManagerSolo.FinDeTours();
			}
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



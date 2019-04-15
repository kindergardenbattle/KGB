using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMove_multi : TacticsMove_multi
{
    
	
	
	void Start ()
	{
        Init();
	}
<<<<<<< HEAD:KGB-master/KinderGardenBattle/Assets/script c#/PlayerMove_multi.cs
=======
	
	
	void Update () 
	{


		if(GameManagerSolo.TeamTurn==PlayerCaracteristique.TeamJoueur)
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
			if (!moving && DebutTour==false)
			{
				GameManagerSolo.FinDeTours();
			}
		}
>>>>>>> origin/CHIBREDESINGE:KGB-master/KinderGardenBattle/Assets/script c#/PlayerMove.cs


    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (is_turn!= ancient_turn)
        {
            move = max_move;
            ancient_turn = is_turn;
        }
        if (is_turn && Want_to_move)
        {            
            Debug.DrawRay(transform.position, transform.forward);
            if (!moving)
            {
                FindSelectableTiles(); //appelle les fonction si ça bouge pas 
                CheckMouse();
            }
        }
        if(moving)
            Move();
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

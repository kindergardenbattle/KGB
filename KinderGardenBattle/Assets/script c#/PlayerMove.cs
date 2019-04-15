using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMove : TacticsMove
{
    
	
	
	void Start ()
	{
        Init();
	}


    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true || !is_turn)
        {
            return;
        }
        if (Want_to_move||moving)
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

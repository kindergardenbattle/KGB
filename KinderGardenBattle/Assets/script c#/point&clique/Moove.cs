using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.UIElements;

public class Moove : MonoBehaviour
{
  

    Vector3 newPosition = Vector3.zero;

    public int speed = 5;

    public GameObject Point;
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0)))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {   
                newPosition = hit.point;
             //à reparer //  transform.LookAt(newPosition);
                Point.transform.position= new Vector3(newPosition.x,Point.transform.position.y,newPosition.z);
                
            }

            if (newPosition != Vector3.zero) 
                transform.position = Vector3.MoveTowards(transform.position, newPosition, speed*Time.deltaTime);
        }

       
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class camera_moove : MonoBehaviour
{
  public Transform target;
  public float vitesse_cam=0.125f;
    private Vector3 offset = new Vector3(10, 10, 10);

    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.O))
       // {
      //     offset.y += 5;
       // }

       // if (Input.GetKeyDown(KeyCode.L))
        //{
           // offset.y -= 5;
       // }

        if (Input.GetKeyDown(KeyCode.M))
        {
            offset.x += 5;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            offset.x -= 5;
        }
    }

    private void LateUpdate()
    {
       
        Vector3 Position_ = target.position + offset;
        Vector3 Nposition = Vector3.Lerp(transform.position, Position_, vitesse_cam);
        transform.position = Nposition;
        transform.LookAt(target);
    }
    
    

}
    
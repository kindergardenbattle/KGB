using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class camera_moove : MonoBehaviour
{
    public Transform target;
    public float vitesse_cam = 0.125f;
    public Vector3 offset = new Vector3(20, 20, 20);
    public  int cas = 0;
    private void Update()
    {
        cas = (Input.GetKeyDown(KeyCode.Keypad6) ? ++cas : (Input.GetKeyDown(KeyCode.Keypad4) ? --cas : cas))%4;
        if (cas < 0)
            cas += 4;

        if (cas == 0)
        {
            if (offset.x < 0)
               offset.x= offset.x * (-1);
            if (offset.z < 0)
                offset.z = offset.z * (-1);

        }

        if (cas == 1)
        {
            if (offset.x < 0)
                offset.x=offset.x * (-1);
            if (offset.z>0)
            {
                offset.z = offset.z * (-1);
            }

        }

        if (cas == 2)
        {
            if (offset.x > 0)
            offset.x= offset.x * (-1);
            if (offset.z > 0)
                offset.z = offset.z * (-1);

        }

        if (cas == 3)
        {
            if (offset.x > 0)
            offset.x= offset.x * (-1);
            if (offset.z < 0)
                offset.z = offset.z * (-1);

        }
        
        if (offset.y < 45)
        {
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                offset.y += 5;
                offset.x += offset.x < 0 ? -5 : 5;
                offset.z += offset.z < 0 ? -5 : 5;
            }
        }
        if (offset.y > 15)
        {
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                offset.y -= 5;
                offset.x += offset.x < 0 ? 5 : -5;
                offset.z += offset.z < 0 ? 5 : -5;
            }
            
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

    
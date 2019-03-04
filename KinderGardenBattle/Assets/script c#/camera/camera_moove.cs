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
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            set_cas_plus();
        }
        
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            set_cas_less();
        }
        
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
        
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                zoom_plus();
            }
        
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                zomm_less();
            }
    }
    
    public void set_cas_plus()
    {
        cas = (cas + 1) % 4;
    }
        
    public void set_cas_less()
    {
        cas = cas == 0 ? 3 : --cas;
    }
    
    public void zoom_plus()
    {
        if (offset.y > 15)
        {
            offset.y -= 5;
            offset.x += offset.x < 0 ? 5 : -5;
            offset.z += offset.z < 0 ? 5 : -5;
        }
    }
    
    public void zomm_less()
    {
        if (offset.y < 45)
        {
            offset.y += 5;
            offset.x += offset.x < 0 ? -5 : 5;
            offset.z += offset.z < 0 ? -5 : 5;
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

    
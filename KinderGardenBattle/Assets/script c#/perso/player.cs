using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // public ClasseduPerso Perso 
    public float speed;
    public float life = 100;


    public Camera camera;
    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(this);
        }
        else

        {

           // float vertical = camera.get
            float horizontal = Input.GetAxis("camera_up") * Time.deltaTime;
           
           // transform.Translate(new Vector3(horizontal * speed, 0f, * speed)); // mouvement

        }
    }
}

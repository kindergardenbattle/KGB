using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mouvement : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;
        transform.Translate(new Vector3(horizontal * speed, 0f, vertical * speed)); // mouvement
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lumiere : MonoBehaviour
{
    
    public Light Lumiere;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButtonDown("space"))
        {
            Lumiere.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

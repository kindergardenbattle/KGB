using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       
    }

    private void PrintName(GameObject go)
    {
        print(go.tag);
    }
}

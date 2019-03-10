using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace multi
{
    public class Team_manager : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Keypad5))
                switch_target();
        }

        public void switch_target()
        {
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateau : MonoBehaviour
{
    private int SelectionX = -1;

    private int SelectioY = -1;
    private int[,] Case;
   


   
    
        
    
  //  private int[,] Actualisation_Tab(int[,] tab)
   // {
      
   // }

    public void  initialisation()
    {
        Case = new int[10, 8];

        for (int i = 3; i <= 7; i++)
        {
            Case[i, 0] = 1;
            Case[i, 10] = 1;
        }
    }






    // Update is called once per frame
    void Update()
    {
        
    }
}

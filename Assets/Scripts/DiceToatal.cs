using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceToatal : MonoBehaviour
{
    // Start is called before the first frame update
    StateManager theStateManager;
    
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>() ;  
    }
    

    // Update is called once per frame
    void Update()
    {
        if (theStateManager.isDoneRolling == true)
        {
            GetComponent<Text>().text = "= " + theStateManager.DiceTotal;
        }
        else
        {
            GetComponent<Text>().text = "= ?";
        }
    }
}

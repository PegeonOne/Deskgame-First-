using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    StateManager theStateManager;
    void Start()
    {
        DiceValue = new int[4];
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }
    public Sprite[] DiceImageOne;
    public Sprite[] DiceImageZero;
    public int[] DiceValue;
    

    void Update()
    {
        
    }
    
    
    
    public void RollTheDice()
    {
        if (theStateManager.isDoneRolling == true)
        {
            return;
        }
        theStateManager.DiceTotal = 0;
        for (int i = 0; i < DiceValue.Length; i++)
        {
            DiceValue[i] = Random.Range(0, 2);
            theStateManager.DiceTotal += DiceValue[i];
            if (DiceValue[i] == 0) {
                this.transform.GetChild(i).GetComponent<Image>().sprite =
                        DiceImageZero[Random.Range(0, DiceImageZero.Length)];
                    }
            else
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite =
                        DiceImageOne[Random.Range(0, DiceImageOne.Length)];
            }
            theStateManager.isDoneRolling = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public int NumberOfOlayers = 2;
    public int currentPlayer = 0;
    public int DiceTotal;
    public bool isDoneRolling  = false;
    public bool isDoneClicking = false;
    public bool isDoneAnimated = false;

    public void NewTurn()
    {
        isDoneRolling  = false;
        isDoneClicking = false;
        isDoneAnimated = false;

        currentPlayer = (currentPlayer + 1) % NumberOfOlayers;
    }
    // Update is called once per frame
    void Update()
    {
        if(isDoneAnimated && isDoneClicking && isDoneRolling)
        {
            Debug.Log("Finish the Roll!");
            NewTurn();
        }
        
    }
}

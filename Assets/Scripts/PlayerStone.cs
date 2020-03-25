using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{
    StateManager theStateManager;
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        targetPosition = this.transform.position;
    }
    
    public Tile startingTile;
    Tile currentTile;

    bool scoreMe = false;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    float SmoothTime = 0.25f;

    float smoothTimeVertical = 0.1f; 

    float SmoothDistance = 0.01f;
    float SmoothHeight = 0.5f;


    Tile[] moveQueue;
    int moveQueueIndex;

    bool isAnimated = false;

    
    void Update()
    {
        if (isAnimated == false)
        {
            return;
        }
        if (Vector3.Distance
            (new Vector3(this.transform.position.x, targetPosition.y ,this.transform.position.z), 
            targetPosition) < SmoothDistance)
        {
            if (moveQueue != null && moveQueueIndex == (moveQueue.Length) && this.transform.position.y > SmoothDistance)
            {
                this.transform.position = Vector3.SmoothDamp(this.transform.position,
                new Vector3(this.transform.position.x, 0f, this.transform.position.z),
                ref velocity, smoothTimeVertical);
            }
            else
            {
                AdvanceMoveQueue();
            }
        }
        else if (this.transform.position.y < (SmoothHeight - SmoothDistance))
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position,
            new Vector3(this.transform.position.x, SmoothHeight, this.transform.position.z),
            ref velocity, smoothTimeVertical);
        }
        else
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position,
            new Vector3(targetPosition.x, SmoothHeight, targetPosition.z),
            ref velocity, SmoothTime);
        }
        
        
    }
    void AdvanceMoveQueue()
    {
        if (moveQueue != null && moveQueueIndex < moveQueue.Length)
        {
            Tile nextTile = moveQueue[moveQueueIndex];
            if (nextTile == null)
            {
                SetNewTargetPosition(this.transform.position + Vector3.right * 10f);
            }
            else
            {
                SetNewTargetPosition(nextTile.transform.position);
                moveQueueIndex++;
            }
        }
        else
        {
            Debug.Log("Done!");
            this.isAnimated = false;
            theStateManager.isDoneAnimated = true;
            
        }
    }
    void SetNewTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        velocity = Vector3.zero;
    }
    
    private void OnMouseUp()
    {
        Tile finalTile = currentTile;
        if(theStateManager.isDoneClicking == true)
        {
            return;
        }
        int spaceToMove = theStateManager.DiceTotal;
            if(spaceToMove == 0)
        {
            return;
        }
        moveQueue = new Tile[spaceToMove];
       
        

        for (int i = 0; i < spaceToMove; i++)
        {
            if(finalTile == null && scoreMe == false)
            {
                finalTile = startingTile;
            }
            else
            {
                if (finalTile.nextTile == null || finalTile.nextTile.Length == 0)
                {
                    scoreMe = true;
                    finalTile = null;
                    Debug.Log("SCORE!");
                    Destroy(gameObject);
                    return;
                }
                else if (finalTile.nextTile.Length > 1)
                {
                    finalTile = finalTile.nextTile[0];
                }
                else
                {
                    finalTile = finalTile.nextTile[0];
                }
               
            }
            moveQueue[i] = finalTile;
        }

        //this.transform.position = finalTile.transform.position;
        //SetNewTargetPosition(finalTile.transform.position);
        theStateManager.isDoneClicking = true;
        currentTile = finalTile;
        moveQueueIndex = 0;
        this.isAnimated = true;
    }
}

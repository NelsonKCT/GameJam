using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movePos = 1f;
    private float moveTimeCount;
    private float moveTime = 1f;

    private Queue playerInput = new Queue();
    private int playerInputCount = 0;

    void Start()
    {
        playerInputCount = 0;
    }
    void Update()
    {   
        moveTimeCount -= Time.deltaTime;
        if(moveTimeCount < 0)
        {
            moveTimeCount = moveTime;
            Debug.Log("Check");
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            playerInput.Enqueue("Left");
            playerInputCount++;
            //transform.position = new Vector3(transform.position.x + movePos, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerInput.Enqueue("Up");
            playerInputCount++;
            //transform.position = new Vector3(transform.position.x, transform.position.y + movePos, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            while(playerInputCount > 0)
            {
                string input = (string)playerInput.Dequeue();
                playerInputCount--;
                
                if(input == "Left")
                {
                    transform.position = new Vector3(transform.position.x + movePos, transform.position.y, transform.position.z);
                }
                else if(input == "Up")
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + movePos, transform.position.z);
                }
            }
            //transform.position = new Vector3(transform.position.x, transform.position.y - movePos, transform.position.z);
        }
    }
}

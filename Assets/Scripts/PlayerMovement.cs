using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movePos = 1f;
    private float moveTimeCount;
    private float moveTime = 0.5f;
    private bool isMoveFinished;
    private bool isEnterPressed;
    private bool moveForwardFinished;

    private Queue playerInputQueue = new Queue();
    private Stack playerBackwardStack = new Stack();
    private int playerInputCount = 0;
    private int playerBackwardCount = 0;

    void Start()
    {
        playerInputCount = 0;
        isMoveFinished = true;
        isEnterPressed = false;
        moveForwardFinished = true;
    }
    void Update()
    {
        Debug.Log(playerInputQueue.Count);

        moveTimeCount -= Time.deltaTime;
        if (moveTimeCount < 0)
        {
            moveTimeCount = moveTime;

            if (playerInputCount > 0 && isEnterPressed)
            {
                PlayerMoveForward();
            }
            if (playerBackwardCount > 0 && moveForwardFinished)
            {
                PlayerMoveBackward();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            playerInputQueue.Enqueue("Left");
            playerInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerInputQueue.Enqueue("Up");
            playerInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Return) && isMoveFinished)
        {
            isEnterPressed = true;
            isMoveFinished = false;
            moveForwardFinished = false;
        }
    }

    private void PlayerMoveForward()
    {
        Debug.Log("Move");
        string input = (string)playerInputQueue.Dequeue();
        playerInputCount--;

        if (input == "Left")
        {
            transform.position = new Vector3(transform.position.x + movePos, transform.position.y, transform.position.z);
            playerBackwardStack.Push("Right");
            playerBackwardCount++;
        }
        else if (input == "Up")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + movePos, transform.position.z);
            playerBackwardStack.Push("Down");
            playerBackwardCount++;
        }

        if (playerInputCount == 0)
        {
            StartCoroutine(WaitForMoveFinish());
        }
    }

    IEnumerator WaitForMoveFinish()
    {
        yield return new WaitForSeconds(1f);
        moveForwardFinished = true;
        isEnterPressed = false;
    }

    private void PlayerMoveBackward()
    {
        Debug.Log("MoveBack");
        string input = (string)playerBackwardStack.Pop();
        playerBackwardCount--;

        if (input == "Right")
        {
            transform.position = new Vector3(transform.position.x - movePos, transform.position.y, transform.position.z);
        }
        else if (input == "Down")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - movePos, transform.position.z);
        }

        if (playerBackwardCount == 0)
        {
            isMoveFinished = true;

        }
    }
}

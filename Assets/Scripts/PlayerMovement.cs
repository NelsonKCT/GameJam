using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputList IList;
    private float movePos = 1f;
    private float moveTimeCount=0;
    private float moveTime = 0.5f;
    public bool isMoveFinished;
    public bool isEnterPressed;
    public bool moveForwardFinished;
    public bool movingForward;
    public bool movingBackward;
    public bool doDelete;

    public Queue playerInputQueue = new Queue();
    private Stack playerBackwardStack = new Stack();
    private int playerInputCount = 0;
    private int playerBackwardCount = 0;
    public string playerCurInput;

    private Transform groundCheck;
    private Transform upCheck;
    private Transform leftCheck;
    private Transform rightCheck;
    private Transform trigger;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    public bool isDownBlocked;
    public bool isUpBlocked;
    public bool isLeftBlocked;
    public bool isRightBlocked;
    public bool canWalkThrough;

    private List<Sprite> playerInputList = new List<Sprite>();

    public GameObject rock;
    public bool rockOnLeft;
    public bool rockOnRight;

    [SerializeField] private AudioSource reverseSoundEffect;

    void Start()
    {
        playerInputCount = 0;
        isMoveFinished = true;
        isEnterPressed = false;
        moveForwardFinished = true;
        isGrounded = true;
        isDownBlocked = false;
        isUpBlocked = false;
        isLeftBlocked = false;
        isRightBlocked = false;
        doDelete=false;

        groundCheck = transform.Find("GroundCheck");
        upCheck = transform.Find("UpCheck");
        leftCheck = transform.Find("LeftCheck");
        rightCheck = transform.Find("RightCheck");
        trigger = transform.Find("Trigger");
        trigger.gameObject.SetActive(false);

        rockOnLeft = false;
        rockOnRight = true;
    }
    void Update()
    {
        // Debug.Log(playerInputQueue.Count);

        moveTimeCount -= Time.deltaTime;
        if (moveTimeCount < 0)
        {
            moveTimeCount = moveTime;

            if (playerInputCount > 0 && isEnterPressed)
            {
                if (isGrounded)
                {
                    PlayerMoveForward();
                }
            }
            if (playerBackwardCount > 0 && moveForwardFinished)
            {
                if (isGrounded)
                {
                    PlayerMoveBackward();
                }
            }

            if (!isGrounded)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movePos, transform.position.z);
            }
        }

        if (isMoveFinished)
        {
            PlayerInput();
        }

        CheckGround();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerInputQueue.Enqueue("Right");
            playerInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerInputQueue.Enqueue("Up");
            playerInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerInputQueue.Enqueue("Trigger");
            playerInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Return) && isMoveFinished)
        {
            isEnterPressed = true;
            isMoveFinished = false;
            moveForwardFinished = false;
            moveTimeCount=0;
        }
    }

    private void PlayerMoveForward()
    {
        string input = (string)playerInputQueue.Dequeue();
        playerCurInput = input;
        playerInputCount--;

        if (input == "Trigger")
        {
            doDelete=true;
            StartCoroutine(SetTrigger());
            playerBackwardStack.Push("Trigger");
            playerBackwardCount++;
        }
        else if (input == "Right")
        {
            doDelete=true;
            if (!isRightBlocked || canWalkThrough)
            {
                transform.position = new Vector3(transform.position.x + movePos, transform.position.y, transform.position.z);
                if (rockOnRight)
                {
                    rock.SendMessage("MoveRockToRight");
                }

            }
            playerBackwardStack.Push("Left");
            playerBackwardCount++;
        }
        else if (input == "Up")
        {
            doDelete=true;
            if (!isUpBlocked || canWalkThrough)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movePos, transform.position.z);
            }
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
        reverseSoundEffect.Play();
        yield return new WaitForSeconds(1f);
        moveForwardFinished = true;
        isEnterPressed = false;
        // movingForward=false;
        // movingBackward=false;
    }

    private void PlayerMoveBackward()
    {

        string input = (string)playerBackwardStack.Pop();
        playerBackwardCount--;

        if (input == "Trigger")
        {
            doDelete=true;
            StartCoroutine(SetTrigger());
        }
        else if (input == "Left")
        {
            doDelete=true;
            if (!isLeftBlocked || canWalkThrough)
            {
                transform.position = new Vector3(transform.position.x - movePos, transform.position.y, transform.position.z);

                if (rockOnLeft)
                {
                    rock.SendMessage("MoveRockToLeft");
                    /*
                    float rockNewX = rock.GetComponent<Transform>().position.x - movePos;
                    float rockNewY = rock.GetComponent<Transform>().position.y;
                    float rockNewZ = rock.GetComponent<Transform>().position.z;
                    rock.GetComponent<Transform>().position = new Vector3(rockNewX, rockNewY, rockNewZ);
                    */
                }
            }
        }
        else if (input == "Down")
        {
            doDelete=true;
            if (!isGrounded || !isDownBlocked || canWalkThrough)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movePos, transform.position.z);
            }
        }

        if (playerBackwardCount == 0)
        {
            isMoveFinished = true;

        }
    }

    IEnumerator SetTrigger()
    {
        trigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        trigger.gameObject.SetActive(false);
    }

    private void CheckGround()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer))
        {
            Collider2D collider = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
            if (collider.CompareTag("Platform"))
            {
                isDownBlocked = false;
            }
            else
            {
                isDownBlocked = true;
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Physics2D.OverlapBox(upCheck.position, groundCheckSize, 0f, groundLayer))
        {
            Collider2D collider = Physics2D.OverlapBox(upCheck.position, groundCheckSize, 0f, groundLayer);
            if (collider.CompareTag("Platform"))
            {
                isUpBlocked = false;
            }
            else
            {
                isUpBlocked = true;
            }
        }
        else
        {
            isUpBlocked = false;
        }

        if (Physics2D.OverlapBox(leftCheck.position, groundCheckSize, 0f, groundLayer))
        {
            Collider2D collider = Physics2D.OverlapBox(leftCheck.position, groundCheckSize, 0f, groundLayer);
            
            if (collider.CompareTag("Rock"))
            {
                rockOnLeft = true;
                rock = collider.gameObject;
                if (rock.GetComponent<Rock>().isLeftBlocked)
                {
                    isLeftBlocked = true;
                }
                else
                {
                    isLeftBlocked = false;
                }
                isLeftBlocked = false;
            }
            else
            {
                
                rockOnLeft = false;
                isLeftBlocked = true;
            }
        }
        else
        {
            rockOnLeft = false;
            isLeftBlocked = false;
        }

        if (Physics2D.OverlapBox(rightCheck.position, groundCheckSize, 0f, groundLayer))
        {
            Collider2D collider = Physics2D.OverlapBox(rightCheck.position, groundCheckSize, 0f, groundLayer);
            if (collider.CompareTag("Rock"))
            {
                rockOnRight = true;
                rock = collider.gameObject;
                if (rock.GetComponent<Rock>().isRightBlocked)
                {
                    isRightBlocked = true;
                }
                else
                {
                    isRightBlocked = false;
                }
            }
            else
            {
                rockOnRight = false;
                isRightBlocked = true;
            }
        }
        else
        {
            rockOnRight = false;
            isRightBlocked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("OneWayDoor"))
        {
            StartCoroutine(OneWayDoor());
        }
    }

    IEnumerator OneWayDoor()
    {
        canWalkThrough = true;
        yield return new WaitForSeconds(0.1f);
        canWalkThrough = false;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}

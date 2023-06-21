using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    private float moveTimeCount = 0;
    private float moveTime = 0.5f;
    private float movePos = 1f;

    private Transform groundCheck;
    private Transform upCheck;
    private Transform leftCheck;
    private Transform rightCheck;
    private Transform trigger;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    
    public bool isDownBlocked;
    public bool isUpBlocked;
    public bool isLeftBlocked;
    public bool isRightBlocked;
    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        moveTimeCount = moveTime;

        isDownBlocked = false;
        isUpBlocked = false;
        isLeftBlocked = false;
        isRightBlocked = false;

        groundCheck = transform.Find("GroundCheck");
        upCheck = transform.Find("UpCheck");
        leftCheck = transform.Find("LeftCheck");
        rightCheck = transform.Find("RightCheck");
        /*
        trigger = transform.Find("Trigger");
        trigger.gameObject.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        moveTimeCount -= Time.deltaTime;
        if (moveTimeCount < 0 && !isGrounded)
        {
            moveTimeCount = moveTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - movePos, transform.position.z);

        }
        CheckGround();
    }

    private void MoveRockToLeft()
    {
        if (!isLeftBlocked)
        {
            float rockNewX = transform.position.x - movePos;
            float rockNewY = transform.position.y;
            float rockNewZ = transform.position.z;
            transform.position = new Vector3(rockNewX, rockNewY, rockNewZ);
        }
    }
    private void MoveRockToRight()
    {
        if (!isRightBlocked)
        {
            float rockNewX = transform.position.x + movePos;
            float rockNewY = transform.position.y;
            float rockNewZ = transform.position.z;
            transform.position = new Vector3(rockNewX, rockNewY, rockNewZ);
        }
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
            isLeftBlocked = true;
        }
        else
        {
            isLeftBlocked = false;
        }

        if (Physics2D.OverlapBox(rightCheck.position, groundCheckSize, 0f, groundLayer))
        {
            isRightBlocked = true;
        }
        else
        {
            isRightBlocked = false;
        }
    }
}

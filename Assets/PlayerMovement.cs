using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal;
    public float moveSpeed = 5f;
    public Transform movePoint; 

    void Start(){
        movePoint.parent = null;
    }
    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal < 0) horizontal = 0;
        else if(horizontal == 1){
            movePoint.position += new Vector3(horizontal, 0f, 0f);
        }
    }
    }

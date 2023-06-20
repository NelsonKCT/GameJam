using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<GameObject> doorList = new List<GameObject>();
    [SerializeField] private List<Sprite> leverSpriteList = new List<Sprite>();
    private bool isTriggered;

    private void Start()
    {
        isTriggered = false;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteList[0];
    }
    private void Update()
    {
        if (isTriggered)
        {
            Debug.Log("test");
            foreach (GameObject door in doorList)
            {
                door.SendMessage("OpenDoor");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trigger"))
        {
            isTriggered = true;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteList[1];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<GameObject> doorList = new List<GameObject>();
    private bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }
    private void Update()
    {
        if (isTriggered)
        {
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
        }
    }
}

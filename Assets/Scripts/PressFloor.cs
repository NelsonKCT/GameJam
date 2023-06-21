using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFloor : MonoBehaviour
{
    [SerializeField] private List<GameObject> doorList = new List<GameObject>();
    [SerializeField] private List<Sprite> PressFloorSpriteList = new List<Sprite>();
    [SerializeField] private bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = PressFloorSpriteList[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            Debug.Log("test");
            foreach (GameObject door in doorList)
            {

                door.SendMessage("OpenDoor");
            }
        }
        else
        {
            foreach (GameObject door in doorList)
            {
                door.SendMessage("CloseDoor");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger enter");
        Debug.LogWarning(other.gameObject.name);
        if (other.CompareTag("Player") || other.CompareTag("Rock"))
        {
            isTriggered = true;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = PressFloorSpriteList[1];
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("trigger exit");
        Debug.LogWarning(other.gameObject.name + " out");
        isTriggered = false;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = PressFloorSpriteList[0];
    }
}

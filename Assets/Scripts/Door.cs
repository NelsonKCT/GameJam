using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    private Transform ClosedDoor;
    private Transform OpenedDoor;
    private void Start()
    {
        ClosedDoor = transform.Find("ClosedDoor");
        OpenedDoor = transform.Find("OpenedDoor");
        ClosedDoor.gameObject.SetActive(true);
        OpenedDoor.gameObject.SetActive(false);
    }
    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        ClosedDoor.gameObject.SetActive(false);
        OpenedDoor.gameObject.SetActive(true);
    }
}

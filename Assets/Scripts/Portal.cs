using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextSeceneName;
    [SerializeField] private bool isLocked;

    private void Update()
    {
        if(isLocked)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void OpenDoor()
    {
        if (isLocked)
        {
            isLocked = false;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isLocked)
        {
            Debug.Log("test");
            SceneManager.LoadScene(nextSeceneName);
        }
    }
}

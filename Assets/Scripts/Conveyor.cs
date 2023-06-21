using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement pM;
    private Transform movePos;
    private void Start()
    {
        movePos = transform.Find("MovePos");
        player = GameObject.Find("Character");
        pM = player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MovePlayer(other.gameObject));
        }
    }

    IEnumerator MovePlayer(GameObject player)
    {
        yield return new WaitForSeconds(0.2f);

        if (pM.playerInputQueue.Count != 0)
        {
            string input = (string)pM.playerInputQueue.Peek();
            if (pM.playerCurInput == input)
            {

            }
            else
            {
                player.transform.position = movePos.position;
            }
        }
        else if(pM.playerInputQueue.Count == 0 && pM.playerBackwardStack.Count !=0)
        {
            string input = (string)pM.playerBackwardStack.Peek();
            if((pM.playerCurInput != input))
            {
                player.transform.position = movePos.position;
            }
        }
        else
        {
            player.transform.position = movePos.position;
        }
    }

}

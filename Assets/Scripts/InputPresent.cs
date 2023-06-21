using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPresent : MonoBehaviour
{
    public Sprite[] sp;
    public SpriteRenderer spR;
    private bool showed;
    // Start is called before the first frame update
    void Start()
    {
        sp = Resources.LoadAll<Sprite>("move-Sheet");
        spR = gameObject.GetComponent<SpriteRenderer>();
        showed = false;
        if(Input.GetKeyDown(KeyCode.D) && !showed){
            spR.sprite = sp[0];
            showed=true;
        }
        else if(Input.GetKeyDown(KeyCode.W) && !showed){
            spR.sprite = sp[1];
            showed=true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}

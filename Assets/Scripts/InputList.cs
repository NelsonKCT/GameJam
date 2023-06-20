using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputList : MonoBehaviour
{
    public List<GameObject> inputs;
    public List<GameObject> rsv_inputs;
    public GameObject input_obj;
    public GameObject rvs_input_obj;
    private int input_n=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)){
            Instantiate(input_obj, transform.position + new Vector3((1+input_n)*0.75f,0,0), transform.rotation);
            Instantiate(rvs_input_obj, transform.position + new Vector3((1+input_n)*0.75f,-0.75f,0), transform.rotation);

            inputs.Add(input_obj);
            rsv_inputs.Add(rvs_input_obj);
            input_n++;
        }
        
    }
}

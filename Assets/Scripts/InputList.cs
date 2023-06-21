using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputList : MonoBehaviour
{
    public List<GameObject> inputs;
    public List<GameObject> rvs_inputs;
    public GameObject input_obj;
    public GameObject rvs_input_obj;
    public PlayerMovement PM;
    private GameObject new_input;

    // private float waitTime=0;
    public int input_n=0;
    public int list_index=0;
    private bool delRvs;
    private bool doDelete;
    // Start is called before the first frame update
    void Start()
    {
        input_n=0;
        list_index=0;
        doDelete=false;
        delRvs=false;
    }

    // Update is called once per frame
    void Update()
    {
        doDelete=PM.doDelete;
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
            new_input =Instantiate(input_obj, transform.position + new Vector3((-input_n)*0.75f,0,0), transform.rotation);
            inputs.Add(new_input);

            new_input = Instantiate(rvs_input_obj, transform.position + new Vector3((-input_n)*0.75f,-0.75f,0), transform.rotation);
            rvs_inputs.Add(new_input);

            input_n++;
        }
        // else if(Input.GetKeyDown(KeyCode.Return)){
        // }
        Debug.Log(doDelete);
        if(doDelete){
            DeleteIcon();
        }
        if(list_index==2*input_n-1){
            list_index=0;
            input_n=0;
        }
    }

    public void DeleteIcon(){
            if(list_index<input_n && !delRvs){
                // if(waitTime>0){
                //     waitTime-=Time.deltaTime;
                // }
                // else{
                //     Destroy(inputs[list_index]);
                //     waitTime = 0.5f;
                //     list_index++; 
                // }
                Destroy(inputs[list_index]);
                list_index++; 
            }
            else if(list_index==input_n && !delRvs) {
                list_index--;
                delRvs=true;
            }
            else if(list_index>=0 && delRvs){
                Destroy(rvs_inputs[list_index]);
                if(list_index==0) delRvs=false;
                list_index--; 
                // if(waitTime>0){
                //     waitTime-=Time.deltaTime;
                // }
                // else{
                //     Destroy(rvs_inputs[list_index-input_n]);
                //     waitTime = 0.5f;
                //     list_index++; 
                // }
            }
        PM.doDelete=false;
    }
}
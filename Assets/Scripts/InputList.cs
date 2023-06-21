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
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
            new_input =Instantiate(input_obj, transform.position + new Vector3((input_n)*0.75f,0,0), transform.rotation);
            inputs.Add(new_input);

            new_input = Instantiate(rvs_input_obj, transform.position + new Vector3((input_n)*0.75f,-0.75f,0), transform.rotation);
            rvs_inputs.Add(new_input);

            input_n++;
        }
        if(PM.doDelete){
            DeleteIcon();
        }
        if(list_index==-1 && delRvs){
            input_n=0;
            list_index=0;
            PM.doDelete=false;
            delRvs=false;
            Debug.Log("Rst");
        }
    }

    public void DeleteIcon(){
            if(list_index<input_n && !delRvs){
                Destroy(inputs[0]);
                inputs.Remove(inputs[0]);
                Debug.Log(list_index);
                list_index++; 
            }
            if(list_index==input_n && !delRvs) {
                delRvs=true;
                Debug.Log("Fix");
                list_index--;
            }
            else if(list_index>=0 && list_index<=input_n && delRvs){
                Destroy(rvs_inputs[rvs_inputs.Count-1]);
                rvs_inputs.Remove(rvs_inputs[rvs_inputs.Count-1]);
                list_index--;
            }
            PM.doDelete=false;
    }
}
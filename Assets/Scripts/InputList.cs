using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputList : MonoBehaviour
{
    public List<GameObject> inputs;
    public List<GameObject> rvs_inputs;
    public GameObject input_obj;
    public GameObject rvs_input_obj;
    private GameObject new_input;
    public PlayerMovement PMove;

    private float waitTime=0;
    private int input_n=0;
    private int list_index=0;
    private bool startDelete=false;
    private bool forwardMove=false;
    private bool backwardMove=false;
    public GameObject selfReference;
    // Start is called before the first frame update
    void Start()
    {
        input_n=0;
        list_index=0;
        startDelete=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){

            new_input =Instantiate(input_obj, transform.position + new Vector3((-input_n)*0.75f,0,0), transform.rotation);
            inputs.Add(new_input);

            new_input = Instantiate(rvs_input_obj, transform.position + new Vector3((-input_n)*0.75f,-0.75f,0), transform.rotation);
            rvs_inputs.Add(new_input);

            input_n++;
        }
        else if(Input.GetKeyDown(KeyCode.Return)){
            startDelete=true;
        }

        if(startDelete){
            DeleteIcon();
        }
        else if(list_index==2*input_n-1){
            startDelete=false;
            list_index=0;
            input_n=0;
        }
    }

    void DeleteIcon(){
        forwardMove = PMove.movingForward;
        backwardMove = PMove.movingBackward; 
        if((forwardMove || backwardMove) && !PMove.delayDeleteIcon){
            if(list_index<input_n){
                if(waitTime>0){
                    waitTime-=Time.deltaTime;
                }
                else{
                    Destroy(inputs[list_index]);
                    waitTime = 0.5f;
                    list_index++; 
                }
            }
            else if(list_index>=input_n && list_index<2*input_n){
                if(waitTime>0){
                    waitTime-=Time.deltaTime;
                }
                else{
                    Destroy(rvs_inputs[list_index-input_n]);
                    waitTime = 0.5f;
                    list_index++; 
                }
            }
        }
    }
}
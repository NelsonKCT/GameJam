using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour{
    private static bgm BGM;
    void Awake(){
        if (BGM == null){
            BGM = this;
            DontDestroyOnLoad(BGM);
        }
    }
}

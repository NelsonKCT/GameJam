using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    void Update() {
        if (Input.anyKeyDown){
            PlayGame();
        }
    }
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
}

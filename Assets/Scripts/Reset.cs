using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    private Button reset;
    [SerializeField] private string sceneName;
    private void Start()
    {
        reset = transform.gameObject.GetComponent<Button>();
    }

    private void Update()
    {
        //reset.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject quit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 1 - Time.timeScale;
            quit.SetActive(!quit.activeSelf);
        }
    }

    public void quitapp() {
        Application.Quit();
    } 
}

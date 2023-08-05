using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public int Scene;
    public int[] wait = new int[4];
    public List<GameObject> on = new List<GameObject>();
    public List<GameObject> off = new List<GameObject>();
    
    private bool disable = true;

    public void OnClick() {
        StartCoroutine(click());
    }

    IEnumerator click() {
        if(disable)
            GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(wait[0]);
        for (int i = 0; i < on.Count; i++)
            on[i].SetActive(true);
        yield return new WaitForSeconds(wait[1]);
        for (int i = 0; i < off.Count; i++)
            off[i].SetActive(false);
        yield return new WaitForSeconds(wait[2]);
        if (Scene != -1)
            SceneManager.LoadScene(Scene);
        else
            Application.Quit();
        yield return new WaitForSeconds(wait[3]);
    }
}

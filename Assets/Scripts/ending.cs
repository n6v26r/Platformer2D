using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
    public int goodending = 0;
    public GameObject ending1;
    public GameObject ending2;


    // Start is called before the first frame update
    void Start()
    {
        if (Movement.score >= goodending)
            ending2.SetActive(true);
        else
            ending1.SetActive(true);
        StartCoroutine("end");
    }

    IEnumerator end() {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
}

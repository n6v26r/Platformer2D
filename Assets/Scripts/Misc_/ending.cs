using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ending : MonoBehaviour
{
    public int goodending = 0;
    public GameObject ending1;
    public GameObject ending2;
    public GameObject scoreUI;

    private void Start()
    {
        Destroy(FindAnyObjectByType<SoundManger>());
        if (Movement.score >= goodending)
            ending2.SetActive(true);
        else
            ending1.SetActive(true);
        if (scoreUI != null)
            scoreUI.GetComponent<TMP_Text>().text = "Score: " + Movement.score.ToString();

        StartCoroutine("end");
    }

    private void Update() {
        
    }

    IEnumerator end() {
        Movement.score = 0;
        LiveManager lm = FindAnyObjectByType<LiveManager>();
        lm.lives = 45; 
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}

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
        StartCoroutine("end");
    }

    private void Update() {
        if (scoreUI != null)
            scoreUI.GetComponent<TMP_Text>().text = "Score: " + Movement.score.ToString();
    }

    IEnumerator end() {
        Movement.score = 0;
        LiveManager lm = FindAnyObjectByType<LiveManager>();
        lm.lives = 15; 
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}

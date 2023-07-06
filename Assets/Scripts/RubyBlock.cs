using UnityEngine;

public class RubyBlock : MonoBehaviour
{
    private SoundManger SoundManager;
    public GameObject rubyItem;
    public float timeRequired;

    float timer;

    void Awake(){
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= timeRequired)
        {
            SoundManager.PlaySound(SoundManager.BlockBreak);
            rubyItem.SetActive(true);
            Destroy(gameObject);
        }
    }
}

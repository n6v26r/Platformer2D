using UnityEngine;

public class RubyBlock : MonoBehaviour
{
    public GameObject rubyItem;
    public float timeRequired;
  
    float timer;

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
            rubyItem.SetActive(true);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrool : MonoBehaviour{
    public GameObject Entity;

    private void Update(){
      if(Entity==null){
            Debug.Log("<color=yellow>[BackgroundScrool WARN]: No entity specified!</color>"); 
            return;
        }
      gameObject.transform.position = new Vector2(Entity.transform.position.x, Entity.transform.position.y);
    }
}

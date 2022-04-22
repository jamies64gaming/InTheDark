using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public int playerId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            if(collider.gameObject.GetComponent<PlayerDetails>().playerID == playerId){
                Debug.Log("player " + playerId + " is ready");
            }
        }
    }
}

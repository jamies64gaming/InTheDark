using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndManager : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;

    private bool P2Ready;
    private bool P1Ready;
    

    void start(){
        P1Ready = trigger1.GetComponent<EndLevelTrigger>().PlayerReady;
        P2Ready = trigger2.GetComponent<EndLevelTrigger>().PlayerReady;
    }
    // Update is called once per frame
    void Update()
    {
        P1Ready = trigger1.GetComponent<EndLevelTrigger>().PlayerReady;
        P2Ready = trigger2.GetComponent<EndLevelTrigger>().PlayerReady;
        if(P1Ready && P2Ready){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("change scene");
        }
    }
}

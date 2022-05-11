using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spiritTalkScript : MonoBehaviour
{
    public string msg;
    public TextMeshProUGUI spiritTextBox;
    private int count = 0;
    public GameObject textbox;

    void start(){
        spiritTextBox.text = "";
        
        
    }
    private void OnTriggerStay2D(){
        spiritTextBox.text = msg;
        textbox.SetActive(true);
        
    }
    private void OnTriggerEnter2D(){
        count++;
        textbox.SetActive(true);
        
    }

    private void OnTriggerExit2D(){
        count--;

        if(count == 0){
            spiritTextBox.text = "";
            textbox.SetActive(false);
            
        }
    }
}

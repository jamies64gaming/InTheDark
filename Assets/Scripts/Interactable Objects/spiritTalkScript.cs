using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spiritTalkScript : MonoBehaviour
{
    public string msg;
    public TextMeshProUGUI spiritTextBox;
    private int count = 0;

    void start(){
        spiritTextBox.text = "";

    }
    private void OnTriggerStay2D(){
        spiritTextBox.text = msg;
    }
    private void OnTriggerEnter2D(){
        count++;
    }

    private void OnTriggerExit2D(){
        count--;

        if(count == 0){
            spiritTextBox.text = "";
        }
    }
}

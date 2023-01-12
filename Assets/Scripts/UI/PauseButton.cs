using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private EventSystem es;

    
        void Start()
    {
        es = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventSystem>();
    }
    
    // pause game and darken button for .2s
    public void OnClick()
    {
        es.PauseGame();
    }
}

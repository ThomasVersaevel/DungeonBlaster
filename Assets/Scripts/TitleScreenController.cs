using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreenController : MonoBehaviour
{
    private string currRun = "0";
    private string scores = "";
    public TMP_Text leaderboard;
    public TMP_Text buttonText;

    /**
     * Setup data keys to save to later
     * Sort leaderboard by time
     */
    void Start()
    {
        if (PlayerPrefs.HasKey("RunNr"))
        {
            currRun = PlayerPrefs.GetInt("RunNr").ToString();
        } else
        {
            PlayerPrefs.SetInt("RunNr", 0);
        }
        if (PlayerPrefs.HasKey("Score"))
        {
            scores = PlayerPrefs.GetString("Score");
        }
        else
        {
            PlayerPrefs.SetString("Score", "");
        }
        print("scores: " + scores);
        leaderboard.text = scores;
        buttonText.text = "Start run " + currRun;
    }
}

using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayScreen : MonoBehaviour
{
    public GameObject canvas;

    public TMP_Text dayCompletedText;

    public TMP_Text coinsText;
    public int coinsCollected;

    public TMP_Text missionText;
    public List<string> missionsCompleted;

    public TMP_Text herbText;
    public List<string> herbsCompleted;

    public TMP_Text brewText;
    public List<string> brewsCompleted;

    public float endOfDayScreenDuration;

    public static EndOfDayScreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void ActivateEndOfDayScreen()
    {
       canvas.SetActive(true);
       SetStats();
    }

    public void SetStats()
    {
        dayCompletedText.text = "";
        coinsText.text = "";
        missionText.text = "";
        herbText.text = "";
        brewText.text = "";

        dayCompletedText.text = "Day " + DayNightCycle.Instance.daysCompleted + " Completed!";

        coinsText.text = coinsCollected.ToString();

        for (int i = 0; i < missionsCompleted.Count; i++)
        {
            missionText.text += missionsCompleted[i] + "\n";
        }

        for (int i = 0; i < herbsCompleted.Count; i++)
        {
            herbText.text += herbsCompleted[i] + "\n";
        }

        for (int i = 0; i < brewsCompleted.Count; i++)
        {
            brewText.text += brewsCompleted[i] + "\n";
        }
    }

    public void DisableEndOfDayScreen()
    {
        canvas.SetActive(false);
    }

    public void CoinsCollected(int coins) { this.coinsCollected += coins; }
    public void MissionCompleted(string text) { missionsCompleted.Add(text); }
    public void HerbCompleted(string text) { herbsCompleted.Add(text); }
    public void BrewCompleted(string text) { brewsCompleted.Add(text); }

}

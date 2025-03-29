using System.Collections;
using TMPro;
using UnityEngine;

public class MatrixTimer : MonoBehaviour
{
    public MatrixPanelManager matrixPanelManager;

    public TMP_Text timerText;

    public float time;
    public float timeElapsed;

    public bool timerActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartMatrixTimer()
    {
        timerActive = true;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while (timeElapsed < time)
        {
            if (timerActive)
            {
                float t = timeElapsed / time;
                timeElapsed += Time.deltaTime;
                timerText.text = System.Math.Round(time - timeElapsed, 0).ToString();
            }

            yield return null;
        }

        TimeRanOut();
    }

    public void TimeRanOut()
    {
        // Game failed
        Debug.Log("Game failed, Time Ran Out!");
        timerActive = false;

        // Call function to infrom player time ran out / Close matrix game?
    }

    public void TimerStop()
    {
        // Stop timer, player won the game
        Debug.Log("Timer Stopped!");
        timerActive = false;

    }
}

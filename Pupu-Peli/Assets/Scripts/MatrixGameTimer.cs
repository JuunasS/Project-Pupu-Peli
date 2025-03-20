using System.Collections;
using UnityEngine;

public class MatrixGameTimer : MonoBehaviour
{
    public float time;
    public float timeElapsed;

    public bool timerActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartMatrixTimer()
    {
        timerActive = true;
        StartCoroutine(MatrixTimer());
    }

    private IEnumerator MatrixTimer()
    {
        while (timeElapsed < time)
        {
            if (timerActive)
            {
                float t = timeElapsed / time;
                timeElapsed += Time.deltaTime;
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

    }

    public void TimerStop()
    {
        // Stop timer, player won the game
        Debug.Log("Timer Stopped!");
        timerActive = false;

    }
}

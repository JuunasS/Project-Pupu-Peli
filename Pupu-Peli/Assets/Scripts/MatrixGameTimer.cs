using System.Collections;
using UnityEngine;

public class MatrixGameTimer : MonoBehaviour
{
    public int time;
    public float timeElapsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartMatrixTimer()
    {

    }

    private IEnumerator MatrixTimer()
    {
        yield return new WaitForEndOfFrame();
    }

    public void TimeRanOut()
    {
        // Game failed

    }

    public void TimerStop()
    {
        // Stop timer, player won the game

    }
}

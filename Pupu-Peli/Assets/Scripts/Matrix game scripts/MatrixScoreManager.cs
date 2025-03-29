using TMPro;
using UnityEngine;

public class MatrixScoreManager : MonoBehaviour
{
    public int currentScore = 0;

    public TMP_Text currentScoreText;

    public void AddScore(int score)
    {
        currentScore += score;
        currentScoreText.text = currentScore.ToString();

        // Add animation for score increase?
    }

    public int GetScore()
    {
        return currentScore;
    }

    /* Redundant?
    public void ResetScore()
    {
    }
    */
}

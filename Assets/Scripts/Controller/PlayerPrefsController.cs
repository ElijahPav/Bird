using UnityEngine;

public class PlayerPrefsController : SingletonMono<PlayerPrefsController>
{
    private string candyKey = "candy";
    private string bestScoreKey = "bestScote";


    public int GetCandiesCount()
    {
        return PlayerPrefs.GetInt(candyKey);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(bestScoreKey);
    }

    public void UpdateCandiesCount(int candies)
    {
        PlayerPrefs.SetInt(candyKey, PlayerPrefs.GetInt(candyKey) + candies);
    }

    public void UpdateBestScore(int score)
    {
        int bestScore = PlayerPrefs.GetInt(bestScoreKey);
        if (bestScore < score)
        {
            PlayerPrefs.SetInt(bestScoreKey, score);
        }
    }
}
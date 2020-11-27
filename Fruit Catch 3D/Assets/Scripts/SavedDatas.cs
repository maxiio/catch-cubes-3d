using UnityEngine;

public class SavedDatas
{
    public void SaveLevel(int currentLevel)
    {
        PlayerPrefs.SetInt("SAVED_LEVEL",currentLevel);
    }

    public void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HIGH_SCORE",highScore);
    }
    
}

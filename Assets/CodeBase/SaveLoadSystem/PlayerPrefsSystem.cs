using UnityEngine;

namespace CodeBase.SaveLoadSystem
{
    public class PlayerPrefsSystem : ISaveLoadSystem
    {
        public void SaveApples(int number) => 
            PlayerPrefs.SetInt("Apple", number);

        public int LoadApples()
        {
            int numbersOfApples = PlayerPrefs.GetInt("Apple", 0);
            return numbersOfApples;
        }

        public void SaveScore(int score) => 
            PlayerPrefs.SetInt("Score", score);

        public int LoadScore()
        {
            int score = PlayerPrefs.GetInt("Score", 0);
            return score;
        }  
        
        public void SaveMaxCompletedStage(int stage) => 
            PlayerPrefs.SetInt("Stage", stage);

        public int LoadMaxCompletedStage()
        {
            int score = PlayerPrefs.GetInt("Stage", 0);
            return score;
        }

        public void SaveCurrentSkin(int skinNumber)
        {
            PlayerPrefs.SetInt("CurrentSkin", skinNumber);
            Debug.Log(skinNumber);
        }
        
        public int LoadCurrentSkin()
        {
            int skinNumber = PlayerPrefs.GetInt("CurrentSkin", 0);
            return skinNumber;
        }  
    }
}
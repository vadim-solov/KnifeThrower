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
        
        public void SaveStage(int stage) => 
            PlayerPrefs.SetInt("Stage", stage);

        public int LoadStage()
        {
            int score = PlayerPrefs.GetInt("Stage", 0);
            return score;
        }
    }
}
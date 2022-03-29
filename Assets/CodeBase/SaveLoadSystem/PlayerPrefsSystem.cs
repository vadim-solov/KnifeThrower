using UnityEngine;

namespace CodeBase.SaveLoadSystem
{
    public class PlayerPrefsSystem : ISaveLoadSystem
    {
        public void SaveApples(int number)
        {
            PlayerPrefs.SetInt("apple", number);
            Debug.Log("Apples save");
        }

        public int LoadApples()
        {
            int numbersOfApples = PlayerPrefs.GetInt("apple", 0);
            Debug.Log("Apples load");
            return numbersOfApples;
        }
    }
}
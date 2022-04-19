using UnityEngine;

namespace CodeBase.SaveLoadSystem
{
    public class PlayerPrefsSystem : ISaveLoadSystem
    {
        public void Save(SaveLoadType type, int value)
        {
            string saveName = "";
            
            switch (type)
            {
                case SaveLoadType.Apples:
                    saveName = "Apple";
                    break;
                case SaveLoadType.Score:
                    saveName = "Score";
                    break;
                case SaveLoadType.MaxCompletedStage:
                    saveName = "Stage";
                    break;
                case SaveLoadType.CurrentSkin:
                    saveName = "CurrentSkin";
                    break;
            }
            
            PlayerPrefs.SetInt(saveName, value);
        }

        public int Load(SaveLoadType type)
        {
            string loadName = "";

            switch (type)
            {
                case SaveLoadType.Apples:
                    loadName = "Apple";
                    break;
                case SaveLoadType.Score:
                    loadName = "Score";
                    break;
                case SaveLoadType.MaxCompletedStage:
                    loadName = "Stage";
                    break;
                case SaveLoadType.CurrentSkin:
                    loadName = "CurrentSkin";
                    break;
            }
            
            int skinNumber = PlayerPrefs.GetInt(loadName, 0);
            return skinNumber;
        }
    }
}
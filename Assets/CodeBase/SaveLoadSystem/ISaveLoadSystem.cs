namespace CodeBase.SaveLoadSystem
{
    public interface ISaveLoadSystem
    {
        void SaveApples(int number);
        int LoadApples();
        void SaveScore(int score);
        int LoadScore();   
        void SaveMaxCompletedStage(int score);
        int LoadMaxCompletedStage();
        void SaveCurrentSkin(int skinNumber);
        int LoadCurrentSkin();
    }
}
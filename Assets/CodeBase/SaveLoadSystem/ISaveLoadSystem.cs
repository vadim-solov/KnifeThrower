namespace CodeBase.SaveLoadSystem
{
    public interface ISaveLoadSystem
    {
        void SaveApples(int number);
        int LoadApples();
        void SaveScore(int score);
        int LoadScore();   
        void SaveStage(int score);
        int LoadStage();
    }
}
namespace CodeBase.SaveLoadSystem
{
    public interface ISaveLoadSystem
    {
        void Save(SaveLoadType type, int value);
        int Load(SaveLoadType type);
    }
}
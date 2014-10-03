namespace SuperbEdit.Base
{
    public interface IConfig
    {
        dynamic UserConfig { get; }
        dynamic DefaultConfig { get; }

        T RetrieveConfigValue<T>(string path);
    }
}
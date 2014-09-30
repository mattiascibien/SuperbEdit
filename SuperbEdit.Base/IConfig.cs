namespace SuperbEdit.Base
{
    public interface IConfig
    {
        dynamic UserConfig { get; }
        dynamic DefaultConfig { get; }
    }
}
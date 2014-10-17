using System;

namespace SuperbEdit.Base
{
    public interface IConfig
    {
        event EventHandler ChangeConfig; 

        dynamic UserConfig { get; }
        dynamic DefaultConfig { get; }

        /// <summary>
        /// Tries to retrieve a configuration value,
        /// if the value does not exsist, returns null or default(T).
        /// </summary>
        /// <typeparam name="T">Type of the parameter</typeparam>
        /// <param name="path">Path in the config.json file, levels must be separated by dots (.)</param>
        /// <returns>Retrieved config value, null or default(T)</returns>
        T RetrieveConfigValue<T>(string path);

        /// <summary>
        /// Tries to retrieve a configuration value,
        /// if the value does not exsist, returns defaultValue.
        /// </summary>
        /// <typeparam name="T">Type of the parameter</typeparam>
        /// <param name="path">Path in the config.json file, levels must be separated by dots (.)</param>
        /// <param name="defaultValue">Value to be returned if config value cannot be found</param>
        /// <returns>Retrieved config value or defaultValue</returns>
        T RetrieveConfigValue<T>(string path, T defaultValue);
    }
}
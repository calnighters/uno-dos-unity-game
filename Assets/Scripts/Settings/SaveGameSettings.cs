using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    public static class SaveGameSettings
    {
        private static readonly string PATH = "/settings.txt";
        private static readonly string SAVE_FILE_NOT_FOUND = "Save File Not Found!!!";

        public static void SaveSettings(GameSettings gameSettings)
        {
            BinaryFormatter _BinaryFormatter = new BinaryFormatter();
            string _Path = Application.persistentDataPath + PATH;
            FileStream _FileStream = new FileStream(_Path, FileMode.Create);

            _BinaryFormatter.Serialize(_FileStream, gameSettings);
            _FileStream.Close();
        }

        public static GameSettings LoadSettings()
        {
            string _Path = Application.persistentDataPath + PATH;
            if (File.Exists(_Path))
            {
                BinaryFormatter _BinaryFormatter = new BinaryFormatter();
                FileStream _FileStream = new FileStream(_Path, FileMode.Open);
                GameSettings _LoadedGameSettings = new GameSettings();
                try
                {
                    _LoadedGameSettings = _BinaryFormatter?.Deserialize(_FileStream) as GameSettings ?? new GameSettings();
                }
                catch
                {
                    Debug.LogError(SAVE_FILE_NOT_FOUND);
                }

                _FileStream.Close();
                return _LoadedGameSettings;
            }
            else
            {
                Debug.LogError(SAVE_FILE_NOT_FOUND);
                return null;
            }
        }
    }
}

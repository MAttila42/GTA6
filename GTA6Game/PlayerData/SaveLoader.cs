using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GTA6Game.PlayerData
{
    public static class SaveLoader
    {
        /// <summary>
        /// The player's save that is loaded
        /// </summary>
        public static PlayerSave Save { get; private set; }

        /// <summary>
        /// The save's destination. 
        /// Documents/GTA6/save.dat
        /// </summary>
        private static string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GTA6", "save.dat");

        /// <summary>
        /// Loads the save
        /// </summary>
        public static void LoadSave()
        {
            try
            {
                FileStream stream = new FileStream(savePath, FileMode.Open,FileAccess.Read);
                using (stream)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Save = (PlayerSave)formatter.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                Save = new PlayerSave();
                PersistData();
            }

            Save.PropertyChanged += OnSaveChanged;
        }
           

        private static void OnSaveChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PersistData();
        }

        /// <summary>
        /// Saves the modified player data
        /// </summary>
        private static void PersistData()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                FileStream stream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                formatter.Serialize(stream, Save);
            }
            catch (Exception e)
            {

            }
        }


    }
}

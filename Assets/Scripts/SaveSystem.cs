using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer()
    {
        //Update to have 3 save files
        string path = Application.persistentDataPath + "/Save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(GameObject.Find("Player").GetComponent<CollisionBehavior>().playerStats, GameObject.Find("Player").GetComponent<CollisionBehavior>().playerInventory);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Save";

        if (!File.Exists(path))
        {
            Debug.LogError("No Save File Found.");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();

        return data;
    }
}

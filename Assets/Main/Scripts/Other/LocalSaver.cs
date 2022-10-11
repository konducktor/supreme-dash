using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LocalSaver
{
    private static string path = Application.persistentDataPath + "/player.sd";

    public static void SaveLocal(int cube, int ball, bool[] cubes, string color)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        LocalData data = new LocalData(cube, ball, cubes, color);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void LoadLocal(int cube, int ball, bool[] cubes, string color)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Deserialize(stream);
        }
    }
}

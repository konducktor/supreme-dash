using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GlobalData
{

    [System.Serializable]
    private class LocalData
    {

        public int? currentCube, currentBall;
        public bool[] unlockedCubes, unlockedBalls;
        public string currentColor, login, password;
        public float? volume;

        public LocalData(bool[] unlockedCubes, bool[] unlockedBalls, int? currentCube, int? currentBall, string color, float? volume, string login, string password)
        {
            this.unlockedCubes = unlockedCubes;
            this.unlockedCubes = unlockedBalls;

            this.currentCube = currentCube;
            this.currentBall = currentBall;

            this.currentColor = color;
            this.volume = volume;

            this.login = login;
            this.password = password;
        }
    }


    private static bool[] unlockedCubes, unlockedBalls;
    private static int? currentCube, currentBall;
    private static string currentColor, login, password;
    private static float? volume;

    #region Properties
    public static bool[] UnlockedCubes
    {
        get
        {
            if (unlockedCubes == null) LoadLocal();
            return unlockedCubes;
        }
        set { unlockedCubes = value; }
    }
    public static bool[] UnlockedBalls
    {
        get
        {
            if (unlockedBalls == null) LoadLocal();
            return unlockedBalls;
        }
        set { unlockedBalls = value; }
    }
    public static int? CurrentCube
    {
        get
        {
            if (currentCube == null) LoadLocal();
            return currentCube;
        }
        set { currentCube = value; }
    }
    public static int? CurrentBall
    {
        get
        {
            if (currentBall == null) LoadLocal();
            return currentBall;
        }
        set { currentBall = value; }
    }
    public static string CurrentColor
    {
        get
        {
            if (currentColor == null) LoadLocal();
            return currentColor;
        }
        set { currentColor = value; }
    }
    public static string Login
    {
        get
        {
            if (login == null) LoadLocal();
            return login;
        }
        set { login = value; }
    }
    public static string Password
    {
        get
        {
            if (password == null) LoadLocal();
            return password;
        }
        set { password = value; }
    }
    public static float? Volume
    {
        get
        {
            if (volume == null) LoadLocal();
            return volume;
        }
        set { volume = value; }
    }
    #endregion

    private static string path = Path.Combine(Application.persistentDataPath, "save");
    private static string file = "/game";

    public static void SaveLocal()
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path + file, FileMode.Create);

        formatter.Serialize(stream, new LocalData(
            unlockedCubes, unlockedBalls,
            currentCube, currentBall,
            currentColor, volume,
            login, password
        ));

        stream.Close();
    }

    public static void LoadLocal()
    {
        if (File.Exists(path + file))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path + file, FileMode.Open);

            if (stream.Length != 0)
            {
                LocalData data = formatter.Deserialize(stream) as LocalData;
                stream.Close();

                unlockedCubes = data.unlockedCubes;
                unlockedBalls = data.unlockedBalls;

                currentCube = data.currentCube;
                currentBall = data.currentBall;

                currentColor = data.currentColor;
                volume = data.volume;

                login = data.login;
                password = data.password;

                return;
            }

            stream.Close();
        }

        unlockedCubes = new bool[46];
        unlockedBalls = new bool[8];

        currentCube = 0;
        currentBall = 0;

        currentColor = "FFFFFF";
        volume = 1f;
    }
}

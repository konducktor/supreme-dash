using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GlobalData
{

    [System.Serializable]
    private class LocalData
    {
        public bool[] unlockedCubes;
        public int? currentCube, currentBall, unlokedLevel, doublers, triplers;
        public string currentColor, login, password;
        public float? volume;
        public bool? advancedStats;

        public LocalData
        (
            bool[] unlockedCubes,
            int? currentCube, int? currentBall, int? unlokedLevel, int? doublers, int? triplers,
            string color, float? volume,
            string login, string password,
            bool? advancedStats
        )
        {
            this.unlockedCubes = unlockedCubes;

            this.currentCube = currentCube;
            this.doublers = doublers;
            this.triplers = triplers;

            this.unlokedLevel = unlokedLevel;
            this.unlokedLevel = unlokedLevel;

            this.currentColor = color;
            this.volume = volume;

            this.login = login;
            this.password = password;
            this.advancedStats = advancedStats;
        }
    }


    private static bool[] unlockedCubes;
    private static int? currentCube, currentBall, unlokedLevel, doublers, triplers;
    private static string currentColor, login, password;
    private static float? volume;
    private static bool? advancedStats;

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
    public static int? UnlokedLevel
    {
        get
        {
            if (unlokedLevel == null) LoadLocal();
            return unlokedLevel;
        }
        set { unlokedLevel = value; }
    }
    public static int? Doublers
    {
        get
        {
            if (doublers == null) LoadLocal();
            return doublers;
        }
        set { doublers = value; }
    }
    public static int? Triplers
    {
        get
        {
            if (triplers == null) LoadLocal();
            return triplers;
        }
        set { triplers = value; }
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
    public static float Volume
    {
        get
        {
            if (volume == null) LoadLocal();
            return (float)volume;
        }
        set { volume = value; }
    }
    public static bool AdvancedStats
    {
        get
        {
            if (advancedStats == null) LoadLocal();
            return (bool)advancedStats;
        }
        set { advancedStats = value; }
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
            unlockedCubes,
            currentCube, currentBall,
            unlokedLevel, doublers, triplers,
            currentColor, volume,
            login, password,
            advancedStats
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

                currentCube = data.currentCube;
                currentBall = data.currentBall;

                unlokedLevel = data.unlokedLevel;
                doublers = data.doublers;
                triplers = data.triplers;

                currentColor = data.currentColor;
                volume = data.volume;

                login = data.login;
                password = data.password;

                advancedStats = data.advancedStats;

                return;
            }

            stream.Close();
        }

        unlockedCubes = new bool[46];

        currentCube = 0;
        currentBall = 0;

        unlokedLevel = 0;
        doublers = 0;
        triplers = 0;

        currentColor = "FFFFFF";
        volume = 1f;
        advancedStats = false;
    }
}

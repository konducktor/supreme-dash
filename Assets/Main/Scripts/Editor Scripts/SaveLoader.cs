using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Defective.JSON;

public class SaveLoader : MonoBehaviour
{

    static string lastName;

    public static void SaveFile()
    {
        string levelString = LevelToJSON();

        File.WriteAllText(Path.Combine(Application.persistentDataPath, lastName + ".txt"), levelString);
        EditorLogic.levelData = null;
    }

    public static string LoadFile(string name = "")
    {
        if (name == string.Empty) name = lastName;

        lastName = name;

        string path = Path.Combine(Application.persistentDataPath, name + ".txt");
        string inputLevel = string.Empty;

        if (File.Exists(path))
        {
            inputLevel = File.ReadAllText(path);
        }


        return inputLevel;
    }

    public static string LevelToJSON() //7 часов работы над редактором би лайк
    {
        JSONObject lvl = new JSONObject();
        JSONObject obj;

        JSONObject objectArray = JSONObject.emptyArray;

        for (int i = 0; i < EditorLogic.objects.Count; i++)
        {
            obj = new JSONObject();

            if (EditorLogic.objects[i].id != 0)
                obj.AddField("id", EditorLogic.objects[i].id);
            if (EditorLogic.objects[i].pos != Vector3.zero)
                obj.AddField("pos", EditorLogic.RoundVector(EditorLogic.objects[i].pos, 5).FromVector3());
            if (EditorLogic.objects[i].rot != Vector3.zero)
                obj.AddField("rot", EditorLogic.objects[i].rot.FromVector3());
            if (EditorLogic.objects[i].col != Color.white)
                obj.AddField("col", EditorLogic.objects[i].col.FromColor());
            if (EditorLogic.objects[i].scale != Vector3.one)
                obj.AddField("scale", EditorLogic.objects[i].scale.FromVector3());
            if (EditorLogic.objects[i].deco)
                obj.AddField("deco", EditorLogic.objects[i].deco);
            if (EditorLogic.objects[i].layer != 0)
                obj.AddField("layer", EditorLogic.objects[i].layer - 100);

            objectArray.Add(obj);
        }

        lvl.AddField("bg", EditorLogic.bgColor.FromColor());
        lvl.AddField("song", EditorSound.songID);
        lvl.AddField("startpos", GameObject.Find("Player").transform.position.FromVector3());
        lvl.AddField("data", objectArray);

        return lvl.ToString();
    }

    public static List<GameObject> JSONToLevel(string input, Camera cam, GameObject[] gameObjs, AudioSource audio)
    {
        JSONObject lvl = new JSONObject(input);
        JSONObject data = new JSONObject();

        for (int i = 0; i < lvl.list.Count; i++)
        {
            string key = lvl.keys[i];
            JSONObject element = lvl.list[i];

            switch (key) //туть добавляем параметры уровня
            {
                case "data":
                    data = element;
                    break;
                case "bg":
                    cam.backgroundColor = element.ToColor();
                    break;
                case "song":
                    EditorSound.songID = element.intValue;
                    audio.clip = Resources.Load<AudioClip>(Path.Combine("Sounds/", EditorSound.songs[element.intValue]));
                    audio.Play();
                    break;
                case "startpos":
                    GameObject.Find("Player").transform.position = element.ToVector3();
                    break;
            }
        }

        List<GameObject> importedLevel = new List<GameObject>();
        EditorLogic.objects = new List<EditorLogic.SavedObject>();

        JSONObject obj;

        for (int a = 0; a < data.list.Count; a++)
        {
            int objID = 0;
            Vector3 pos = Vector3.zero;
            Vector3 rot = Vector3.zero;
            Color col = Color.white;
            Vector3 scale = Vector3.one;
            bool deco = false;
            int layer = 0;

            obj = data.list[a];
            for (int b = 0; b < obj.list.Count; b++)
            {
                switch (obj.keys[b]) //а туть для объекта
                {
                    case "id":
                        objID = obj.list[b].intValue;
                        break;
                    case "pos":
                        pos = obj.list[b].ToVector3();
                        break;
                    case "rot":
                        rot = obj.list[b].ToVector3();
                        break;
                    case "col":
                        col = obj.list[b].ToColor();
                        break;
                    case "scale":
                        scale = obj.list[b].ToVector3();
                        break;
                    case "deco":
                        deco = obj.list[b].boolValue;
                        break;
                    case "layer":
                        layer = obj.list[b].intValue;
                        break;
                }
            }

            importedLevel.Add(Instantiate(gameObjs[objID], pos, Quaternion.Euler(rot)));
            importedLevel[importedLevel.Count - 1].GetComponent<SpriteRenderer>().color = col;
            importedLevel[importedLevel.Count - 1].transform.localScale = scale;

            EditorLogic.objects.Add(new EditorLogic.SavedObject(objID, pos, rot, col, scale));

            EditorLogic.objects[EditorLogic.objects.Count - 1].layer = layer + 100;
            importedLevel[importedLevel.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = layer + 100;

            if (deco)
            {
                EditorLogic.objects[EditorLogic.objects.Count - 1].deco = deco;

                Destroy(importedLevel[importedLevel.Count - 1].GetComponent<Rigidbody2D>());
                Destroy(importedLevel[importedLevel.Count - 1].GetComponent<PhysicsLogic>());
                Destroy(importedLevel[importedLevel.Count - 1].GetComponent<Collider2D>());
            }
        }

        return importedLevel;
    }
}

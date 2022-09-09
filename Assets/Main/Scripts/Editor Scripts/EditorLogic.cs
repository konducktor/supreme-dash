using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Defective.JSON;

public class EditorLogic : MonoBehaviour
{
    public static string levelData;
    public static int objectID = 0;
    public static GameObject[] gameObjects;
    [SerializeField] private GameObject[] inputObjects;
    [SerializeField] private Camera cam;

    public class SavedObject
    {
        public int id;
        public Vector3 pos;
        public Vector3 rot;

        public Color col;
        public Vector3 scale;

        public bool deco;
        public int layer;

        public SavedObject(int objID, Vector3 position, Vector3 rotation, Color color, Vector3 scale, int layer = 0, bool deco = false)
        {
            this.id = objID;
            this.pos = position;
            this.rot = rotation;
            this.col = color;
            this.scale = scale;
            this.layer = layer;
            this.deco = deco;
        }
    }

    public static List<GameObject> level = new List<GameObject>();
    public static List<SavedObject> objects = new List<SavedObject>();
    [SerializeField] private Text objectCounter;

    private GameObject currentObject;


    void Start()
    {

        currentObject = new GameObject();
        gameObjects = inputObjects;

        level.Clear();
        objects.Clear();

        if (EditorLogic.levelData == null) EditorLogic.levelData = SaveLoader.LoadFile();
        level = SaveLoader.JSONToLevel(levelData, cam, gameObjects, GetComponent<AudioSource>());

        EditorLogic.bgColor = cam.backgroundColor;
    }

    public static Vector3 RoundVector(Vector3 vec, int amount)
    {
        vec.x = EditorCursor.Round(vec.x, amount);
        vec.y = EditorCursor.Round(vec.y, amount);

        return vec;
    }

    private bool conditions;
    void Update()
    {
        conditions = objects.Exists(a =>
            RoundVector(a.pos, 0) == RoundVector(GameInput.Pointer(), 0) &&
            a.rot == EditorCursor.currentRotation &&
            a.id == objectID &&
            a.layer == LayerManager.currentLayer
        );


        if (GameInput.Build() && !conditions && objectID != 11)
        {
            currentObject = Instantiate(gameObjects[objectID], GameInput.Pointer(), Quaternion.Euler(EditorCursor.objRotation));

            currentObject.GetComponent<SpriteRenderer>().color = EditorSelector.currentObjectColor;
            currentObject.GetComponent<SpriteRenderer>().sortingOrder = LayerManager.currentLayer;
            currentObject.transform.localScale = EditorSelector.currentObjectScale;

            level.Add(currentObject);
            objects.Add(new SavedObject(
                objectID,
                currentObject.transform.position,
                EditorCursor.objRotation,
                EditorSelector.currentObjectColor,
                EditorSelector.currentObjectScale,
                LayerManager.currentLayer
            ));

        }

        if (objectID == 11 && GameInput.Build())
        {
            GameObject.Find("Player").transform.position = GameInput.Pointer();
        }

        objectCounter.text = "Objects: " + objects.Count;
    }


    [SerializeField] private InputField bgInput;
    public static Color bgColor;
    public void BGColor()
    {
        if (bgInput.text != "")
        {
            bgColor = ColorSelector.ColorFromString(bgInput.text);
            cam.backgroundColor = bgColor;
            return;
        }

        bgColor = cam.backgroundColor;
    }

    public void Playtest()
    {
        GameLoader.ExitScene = "Editor";
        SceneManager.LoadScene("CustomLevel");
    }
}


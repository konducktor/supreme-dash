using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class DiscordRichPresence : MonoBehaviour
{
    [System.Serializable]
    private class SceneActivity
    {
        public string scene, details, state;
    }

    [SerializeField] private long applicationID;

    [Space]

    [SerializeField] private string largeImage;
    [SerializeField] private SceneActivity[] sceneActivities;

    public Discord.Discord discord;

    private static bool instanceExists;

    private long time;

    void Awake()
    {
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();

    }

    void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        SceneActivity activity = Array.Find<SceneActivity>(sceneActivities, a => a.scene == activeScene);

        try
        {
            var activityManager = discord.GetActivityManager();
            var discordActivity = new Discord.Activity
            {
                Details = activity.details,
                State = activity.state,
                Assets =
                {
                    LargeImage = largeImage
                },
                Timestamps =
                {
                    Start = time
                }
            };

            switch (activity.scene)
            {
                case "CustomLevel":
                    discordActivity.State += GameLoader.currentID;
                    break;
                case "Editor":
                    discordActivity.State += EditorLogic.objects.Count;
                    break;
            }

            activityManager.UpdateActivity(discordActivity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            // If updating the status fails, Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
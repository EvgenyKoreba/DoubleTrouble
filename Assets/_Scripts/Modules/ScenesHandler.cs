using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomEventSystem;

public class ScenesHandler: MonoBehaviour, IRestartLevelHandler
{

    private static ScenesHandler _instance;

    static public ScenesHandler INSTANCE
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    private void Awake()
    {
        PrepareSingleton();
        DontDestroyOnLoad(gameObject);
    }

    private void PrepareSingleton()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(INSTANCE.gameObject);
            INSTANCE = this;
        }
    }

    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }


    static public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    static public void NextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        int allScenes = SceneManager.sceneCount;

        if (nextScene <= allScenes)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void ReloadCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    public void RestartLevel()
    {
        INSTANCE.ReloadCurrentScene();
    }
}

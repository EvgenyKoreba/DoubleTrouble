using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomEventSystem;

public class ScenesHandler: MonoBehaviour, IRespawnLevelHandler
{

    private static ScenesHandler _instance;

    static public ScenesHandler Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }


    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        int allScenes = SceneManager.sceneCount;

        if (currentScene + 1 <= allScenes)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    public void ReloadCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    public void RespawnLevel()
    {
        Instance.ReloadCurrentScene();
    }
}

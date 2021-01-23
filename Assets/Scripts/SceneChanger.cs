using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
}

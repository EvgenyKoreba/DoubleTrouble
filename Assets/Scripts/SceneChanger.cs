using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{

    private static SceneChanger _S;

    static public SceneChanger S
    {
        get { return _S; }
        private set { _S = value; }
    }

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Destroy(S.gameObject);
            S = this;
        }

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

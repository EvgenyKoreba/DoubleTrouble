using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertsContainer : MonoBehaviour
{
    private static InsertsContainer _instance;

    static public InsertsContainer INSTANCE
    {
        get { return _instance; }
        private set { _instance = value; }
    }


    [Header("Set in Inspector")]
    public Canvas checkpointReach;
    public Canvas levelFinish;

    private void Awake()
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

        DontDestroyOnLoad(INSTANCE.gameObject);
    }
}

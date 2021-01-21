using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InsertsContainer : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private Canvas checkpointReach;
    [SerializeField] private Canvas levelFinish;


    private static UI_InsertsContainer _S;

    static public UI_InsertsContainer S
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

        DontDestroyOnLoad(S.gameObject);
    }
}

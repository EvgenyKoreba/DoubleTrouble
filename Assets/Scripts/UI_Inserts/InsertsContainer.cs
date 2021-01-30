using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertsContainer : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Canvas checkpointReach;
    public Canvas levelFinish;


    private static InsertsContainer _S;

    static public InsertsContainer S
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

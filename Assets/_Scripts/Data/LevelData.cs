using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Custom/Data")]
public class LevelData : ScriptableObject
{

    [SerializeField] private float _restartDelay = 1f;
    [SerializeField] private float _endDelay = 5f;

    #region Getters
    public float GetRestartDelay()
    {
        return _restartDelay;
    }
    public float GetEndDelay()
    {
        return _endDelay;
    }
    #endregion

}

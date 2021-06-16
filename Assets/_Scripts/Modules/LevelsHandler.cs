using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class LevelsHandler : MonoBehaviour, ILevelFinishedHandler
{
    #region Singleton
    private static LevelsHandler _instance;

    static public LevelsHandler INSTANCE
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    #endregion

    #region Fields
    [SerializeField] private LevelData _currentLevel;
    #endregion

    #region Events
    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }

    public void LevelFinished()
    {

    }
    #endregion

    private void Awake()
    {
        PrepareSingleton();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        EventsHandler.RaiseEvent<IStartLevelHandler>(h => h.StartLevel(_currentLevel));
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
}

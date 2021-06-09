using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum EVENT_TYPE
{
    // Сюда добавлять все возможные события
    CheckpointReached,
    LevelFinished,
    PlayerDamaged,
    FoundModifier,

}

public class EventManager : MonoBehaviour
{
    [Serializable]
    public class GameEvent: UnityEvent<object[]> {};


    private static EventManager _instance;
    private Dictionary<EVENT_TYPE, GameEvent> _eventDictionary;


    static public EventManager Instance
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
        _eventDictionary = new Dictionary<EVENT_TYPE, GameEvent>();

        DontDestroyOnLoad(Instance.gameObject);
    }

    /// <summary>
    /// Функция добавления события в список событий
    /// </summary>
    /// <param name="eventType">Тип события, ожидаемое получателем</param>
    /// <param name="listener">Объект, ожидающий событие</param>
    public static void Subscribe(EVENT_TYPE eventType, UnityAction<object[]> listener)
    {
        GameEvent thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventType, thisEvent);
        }
    }

    /// <summary>
    /// Функция удаления объекта из списка получателей события
    /// </summary>
    /// <param name="eventType">Тип события у удаляемого объекта</param>
    /// <param name="listener">Объект, удаляемый из списка получателей события</param>
    static public void Unsubscribe(EVENT_TYPE eventType, UnityAction<object[]> listener)
    {
        GameEvent thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);

        }
    }

    /// <summary>
    /// Посылает событие слешателям
    /// </summary>
    /// <param name="eventType">Событие для вызова</param>
    /// <param name="parameters">Необязательный аргумент параметров</param>
    static public void PostNotification(EVENT_TYPE eventType, params object[] parameters)
    {
        GameEvent thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.Invoke(parameters);
        }
    }
}

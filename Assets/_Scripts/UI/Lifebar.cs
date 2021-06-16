using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CustomEventSystem;

public class Lifebar : MonoBehaviour, IStartLevelHandler, IHealthChangeHandler
{
    #region Fields
    [Header("Set in Inspector")]
    [SerializeField] private Player _player;
    [SerializeField] private Transform _lifePrefab;

    [Header("Set Dynamically")]
    private List<Transform> _lifes;
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

    public void StartLevel(LevelData level)
    {
        _lifes = new List<Transform>();

        while (_lifes.Count < _player.MaxLifes)
        {
            AddLife();
        }
    }

    public void Heal(int value = 1)
    {
        int count = value;
        while (count > 0)
        {
            AddLife();
            count--;
        }
    }

    public void RecieveDamage(int damage = 1)
    {
        int count = damage;
        while (count > 0)
        {
            RemoveLife();
            count--;
        }
    }
    #endregion

    private void AddLife()
    {
        Transform life = Instantiate(_lifePrefab, transform);
        _lifes.Add(life);
    }
    private void RemoveLife()
    {
        Destroy(_lifes.Last().gameObject);
        _lifes.Remove(_lifes.Last());
    }
}

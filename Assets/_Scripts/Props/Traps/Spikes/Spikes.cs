using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : DamagingBehaviour
{
    private enum TypeOfSpikes
    {
        Static,
        Looping,
        Triggered
    }

    #region Fields
    [SerializeField] private float _loopTime;
    [SerializeField] private float _activatingTime;
    [SerializeField] private float _triggerReactionTime;
    [SerializeField] private TypeOfSpikes _type;

    [Header("Set for use")]
    [SerializeField] private Transform _spikes;
    [SerializeField] private Transform _spikesPushPosition;

    private bool _isTriggered = false;
    private Vector3 _startSpikesPos;
    #endregion

    private void Awake()
    {
        _startSpikesPos = _spikes.position;
    }

    private void Start()
    {
        if (_type == TypeOfSpikes.Static)
        {
            StartCoroutine(PushSpikes());
        }
        else if (_type == TypeOfSpikes.Looping)
        {
            StartCoroutine(LoopSpikesMove());
        }
        else if (_type == TypeOfSpikes.Triggered)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private IEnumerator PushSpikes()
    {
        while (_spikes.position != _spikesPushPosition.position)
        {
            _spikes.position = Vector2.MoveTowards(_spikes.position, _spikesPushPosition.position, 0.1f);
            yield return null;
        }

    }
    
    private IEnumerator LoopSpikesMove()
    {
        while (true)
        {
            while (_spikes.position != _spikesPushPosition.position)
            {
                _spikes.position = Vector2.MoveTowards(_spikes.position, _spikesPushPosition.position, 0.1f);
                yield return null;
            }


            yield return new WaitForSeconds(_activatingTime);


            while (_spikes.position != _startSpikesPos)
            {
                _spikes.position = Vector2.MoveTowards(_spikes.position, _startSpikesPos, 0.1f);
                yield return null;
            }

            yield return new WaitForSeconds(_loopTime);
        }
    }

    private IEnumerator TriggeredPush()
    {
        yield return new WaitForSeconds(_triggerReactionTime);

        while (_spikes.position != _spikesPushPosition.position)
        {
            _spikes.position = Vector2.MoveTowards(_spikes.position, _spikesPushPosition.position, 0.1f);
            yield return null;
        }

        while (_isTriggered)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(_activatingTime);

        while (_spikes.position != _startSpikesPos)
        {
            _spikes.position = Vector2.MoveTowards(_spikes.position, _startSpikesPos, 0.1f);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _isTriggered = true;
            StartCoroutine(TriggeredPush());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _isTriggered = false;
        }
    }
}
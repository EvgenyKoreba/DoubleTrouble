using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float loopTime;
    [SerializeField] private float activatingTime;
    [SerializeField] TypeOfSpikes type;

    [Header("Set for use")]
    [SerializeField] private Transform spikes;
    [SerializeField] private Transform spikesPushPosition;

    private Vector3 startSpikesPos;

    private void Awake()
    {
        startSpikesPos = spikes.position;
    }
    private void Start()
    {
        if (type == TypeOfSpikes.Static)
        {
            StartCoroutine(PushSpikes());
        }
        else if (type == TypeOfSpikes.Looping)
        {
            StartCoroutine(LoopSpikes());
        }
        else if (type == TypeOfSpikes.Triggered)
        {

        }
    }





    private enum TypeOfSpikes
    {
        Static,
        Looping,
        Triggered
    }


    private IEnumerator LoopSpikes()
    {
        while (true)
        {
            StartCoroutine(PushSpikes());
            yield return new WaitForSeconds(loopTime);
            StartCoroutine(HideSpikes());
            yield return new WaitForSeconds(activatingTime);

        }
    }
    private IEnumerator PushSpikes()
    {
        while (spikes.position != spikesPushPosition.position)
        {
            spikes.position = Vector2.Lerp(spikes.position, spikesPushPosition.position, 0.1f);
            yield return null;
        }
    }
    private IEnumerator HideSpikes()
    {
        while (spikes.position != startSpikesPos)
        {
            spikes.position = Vector2.Lerp(spikes.position, startSpikesPos, 0.1f);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
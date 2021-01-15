using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float loopTime;
    [SerializeField] private float activatingTime;
    [SerializeField] private float triggerReactionTime;
    [SerializeField] TypeOfSpikes type;

    [Header("Set for use")]
    [SerializeField] private Transform spikes;
    [SerializeField] private Transform spikesPushPosition;
    private bool isTriggered = false;

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
            StartCoroutine(LoopSpikesMove());
        }
        else if (type == TypeOfSpikes.Triggered)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }





    private enum TypeOfSpikes
    {
        Static,
        Looping,
        Triggered
    }



    private IEnumerator PushSpikes()
    {
        while (spikes.position != spikesPushPosition.position)
        {
            spikes.position = Vector2.MoveTowards(spikes.position, spikesPushPosition.position, 0.1f);
            yield return null;
        }

    }
    
    private IEnumerator LoopSpikesMove()
    {
        while (true)
        {
            while (spikes.position != spikesPushPosition.position)
            {
                spikes.position = Vector2.MoveTowards(spikes.position, spikesPushPosition.position, 0.1f);
                yield return null;
            }


            yield return new WaitForSeconds(activatingTime);


            while (spikes.position != startSpikesPos)
            {
                spikes.position = Vector2.MoveTowards(spikes.position, startSpikesPos, 0.1f);
                yield return null;
            }

            yield return new WaitForSeconds(loopTime);
        }
    }
    private IEnumerator TriggeredPush()
    {
        yield return new WaitForSeconds(triggerReactionTime);

        while (spikes.position != spikesPushPosition.position)
        {
            spikes.position = Vector2.MoveTowards(spikes.position, spikesPushPosition.position, 0.1f);
            yield return null;
        }

        while (isTriggered)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(activatingTime);

        while (spikes.position != startSpikesPos)
        {
            spikes.position = Vector2.MoveTowards(spikes.position, startSpikesPos, 0.1f);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isTriggered = true;
            StartCoroutine(TriggeredPush());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isTriggered = false;
        }
    }
}
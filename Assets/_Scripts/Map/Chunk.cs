using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject startOfChunk;
    [SerializeField] GameObject endOfChunk;

    public GameObject StartOfChunk { get { return startOfChunk; } }
    public GameObject EndOfChunk { get { return endOfChunk; } }

}

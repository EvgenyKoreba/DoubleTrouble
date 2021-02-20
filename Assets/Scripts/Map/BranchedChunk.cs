using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchedChunk : MonoBehaviour
{
    [SerializeField] GameObject startOfChunk;
    [SerializeField] GameObject endOfChunk1;
    [SerializeField] GameObject endOfChunk2;

    public GameObject StartOfChunk { get { return startOfChunk; } }
    public GameObject EndOfChunk1 { get { return endOfChunk1; } }
    public GameObject EndOfChunk2 { get { return endOfChunk2; } }

}

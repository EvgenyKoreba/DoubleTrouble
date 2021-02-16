using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] private int countOfChunks;
    [SerializeField] private Chunk[] chunks;

    private Chunk[] randomChunks;
    private void GenerateLevel()
    {
        randomChunks = new Chunk[countOfChunks];
        int rnd = Random.Range(0, chunks.Length);
        randomChunks[0] = Instantiate(chunks[rnd], transform.position, Quaternion.identity);
        float x = transform.position.x;
        for (int i = 1; i < countOfChunks; i++)
        {
            rnd = Random.Range(0, chunks.Length);
            randomChunks[i] = chunks[rnd];
            x += ((randomChunks[i - 1].EndOfChunk.transform.position.x - randomChunks[i - 1].transform.position.x) 
                + (randomChunks[i].transform.position.x - randomChunks[i].StartOfChunk.transform.position.x));
            randomChunks[i] = Instantiate(randomChunks[i], new Vector3(x, transform.position.y, 0), Quaternion.identity);
        }
    }
    private void Start()
    {
        GenerateLevel();
    }
}

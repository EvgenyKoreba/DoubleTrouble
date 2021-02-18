using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{


    [Header("Chunks set")]
    [SerializeField] private Chunk[] chunksEasy;
    [SerializeField] private Chunk[] chunksMedium;
    [SerializeField] private Chunk[] chunksHard;
    [SerializeField] private BranchedChunk[] branchedChunks;

    [Header("Persents set")]
    [SerializeField] private int countsOfEasy;
    [SerializeField] private int countsOfMedium;
    [SerializeField] private int countsOfHard;


    private List<Chunk> randomChunks = new List<Chunk>();
    private void GenerateLevel()
    {
        int countOfChunks = countsOfEasy + countsOfMedium + countsOfHard;
        

        for (int i = 0; i < countsOfEasy; i++)
        {
            int rndE = Random.Range(0,chunksEasy.Length);
            randomChunks.Add(chunksEasy[rndE]);
        }
        for (int i = 0; i < countsOfMedium; i++)
        {
            int rndE = Random.Range(0, chunksMedium.Length);
            randomChunks.Add(chunksMedium[rndE]);
        }
        for (int i = 0; i < countsOfHard; i++)
        {
            int rndE = Random.Range(0, chunksHard.Length);
            randomChunks.Add(chunksHard[rndE]);
        }

        randomChunks = RandomizeChunks(randomChunks);




        randomChunks[0] = Instantiate(randomChunks[0], transform.position, Quaternion.identity);
        float x = transform.position.x;
        float y = transform.position.y;
        for (int i = 1; i < countOfChunks; i++)
        {
            //соединение конца с началом
            x += ((randomChunks[i - 1].EndOfChunk.transform.position.x - randomChunks[i - 1].transform.position.x)
                + (randomChunks[i].transform.position.x - randomChunks[i].StartOfChunk.transform.position.x));
            y += ((randomChunks[i - 1].EndOfChunk.transform.position.y - randomChunks[i - 1].transform.position.y)
                + (randomChunks[i].transform.position.y - randomChunks[i].StartOfChunk.transform.position.y));
            randomChunks[i] = Instantiate(randomChunks[i], new Vector3(x, y, 0), Quaternion.identity);
        }
    }
    private void Start()
    {
        GenerateLevel();
    }


    private List<Chunk> RandomizeChunks(List<Chunk> chunk)
    {
        List<Chunk> chunkList = new List<Chunk>();

        List<Chunk> randChunk = new List<Chunk>();

        for (int i = 0; i < chunk.Count; i++)
        {
            chunkList.Add(chunk[i]);
        }

        for (int i = 0; i < chunk.Count; i++)
        {
            int rnd = Random.Range(0, chunkList.Count);
            randChunk.Add(chunkList[rnd]);
            chunkList.RemoveAt(rnd);
        }
        return randChunk;
    }
}

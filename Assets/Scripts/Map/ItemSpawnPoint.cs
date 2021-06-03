using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsPool;
    [SerializeField] private float[] persOfSpawnItem;
    [Header("pers of any spawn from 0 to 100")]
    [SerializeField] private float persOfSpawn;



    private void Start()
    {
        if (IsAnySpawn())
            Instantiate(ItemRoll(), transform.position, Quaternion.identity);
    }
    private bool IsAnySpawn()
    {
        float rnd = Random.Range(0f, 100f);
        if (rnd > persOfSpawn)
            return false;

        return true;
    }
    private GameObject ItemRoll()
    {
        float sumOfPers = 0f;
        for (int i = 0; i < persOfSpawnItem.Length; i++)
        {
            sumOfPers += persOfSpawnItem[i];
        }
        float rnd = Random.Range(0f, sumOfPers);
        for (int i = 0; i < persOfSpawnItem.Length; i++)
        {
            rnd -= persOfSpawnItem[i];
            if (rnd <= 0)
                return itemsPool[i];
        }
        print("itemRollError");
        return null;
    }
}

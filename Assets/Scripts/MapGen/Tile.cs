using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Sprite[] squareSpritesSet;
    [SerializeField] TypeOfTile typeOfTile;
    void Start()
    {
        int rnd = Random.Range(0, squareSpritesSet.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = squareSpritesSet[rnd];
    }
    public enum TypeOfTile
    {
        Square,
        AngledLeft,
        AngledRight
    }

}

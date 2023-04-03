using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnPoint : MonoBehaviour
{
    public GameObject[] tiles;
    private void Awake()
    {
        int rand = Random.Range(0, tiles.Length);
        GameObject tile = Instantiate(tiles[rand], transform.position, Quaternion.identity);
        tile.transform.parent = transform;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

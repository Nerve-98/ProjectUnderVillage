using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomForEmptySpace : MonoBehaviour
{

    public LayerMask RoomLayerMask;
    public DungeonMapGenerationTest DungenGen;
    private void Awake()
    {
        RoomLayerMask = LayerMask.GetMask("Room");
    }
    void Start()
    {

    }

    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, RoomLayerMask);
        if (roomDetection == null && DungenGen.stopGeneration == true)
        {
            int rand = Random.Range(0, DungenGen.rooms.Length);
            Instantiate(DungenGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}

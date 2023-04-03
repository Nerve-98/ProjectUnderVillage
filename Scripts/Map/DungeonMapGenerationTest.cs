using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerationTest : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    private float moveAmount = 10;
    private float timeBtwRoom;
    private float startTimeBtwRoom = 2.0f;
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration = false;

    private const int right_rand_min = 0;
    private const int right_rand_max = 1;
    private const int left_rand_min = 2;
    private const int left_rand_max = 3;
    private const int below_rand_max = 4;

    private void Awake()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(right_rand_min, below_rand_max + 1);
        minX = startingPositions[0].transform.position.x;
        maxX = startingPositions[startingPositions.Length - 1].transform.position.x;
        minY = startingPositions[0].transform.position.y - 30;
    }
    void Start()
    {

    }

    void Update()
    {
        if(stopGeneration == false && timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;

        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        Vector3 newPos;
        if (direction <= right_rand_max) // move right
        {
            if(transform.position.x < maxX) 
            {
                newPos = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(right_rand_min, below_rand_max + 1);
                RoomInstantiate();
                if (direction == left_rand_min)
                {
                    direction = right_rand_min;
                }
                else if(direction == left_rand_max)
                {
                    direction = below_rand_max; 
                }
            }
            else
            {
                direction = below_rand_max;
            }

        } else if(direction <= left_rand_max) // move left
        {
            if (transform.position.x > minX)
            {
                newPos = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(left_rand_min, below_rand_max + 1);
                RoomInstantiate();
            }
            else
            {

                direction = below_rand_max;
            }
        }
        else if (direction <= below_rand_max) // move down
        {
            if(transform.position.y > minY)
            {
                newPos = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(right_rand_min, below_rand_max + 1);
                RoomInstantiate();
            }
            else
            {
                stopGeneration = true;
            }
        }


    }

    private void RoomInstantiate()
    {
        Instantiate(rooms[0], transform.position, Quaternion.identity);
    }
}

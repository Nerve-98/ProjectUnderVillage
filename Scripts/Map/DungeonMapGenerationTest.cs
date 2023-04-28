using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerationTest : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // from 0 LR, LRB, LRT, LRBT

    private int direction;
    private float x_moveAmount = 20;
    private float y_moveAmount = 10;
    private float timeBtwRoom;
    //private float startTimeBtwRoom = 1.0f;
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration = false;
    public LayerMask RoomLayerMask;

    private int BottomCounter = 0;

    private const int right_rand_min = 0;
    private const int right_rand_max = 4;
    private const int left_rand_min = 5;
    private const int left_rand_max = 9;
    private const int bottom_rand_min = 10;
    private const int bottom_rand_max = 11;

    private const int LR_room_type = 0;
    private const int LRB_room_type = 1;
    private const int LRT_room_type = 2;
    private const int LRBT_room_type = 3;
    private int[] Bottom_room_type_arr;
    private int[] Top_room_type_arr;

    private void Awake()
    {
        InitialRoomSetting();

    }
    void Start()
    {
        Vector3 newPos = new Vector3(transform.position.x - 4, transform.position.y - 1, Managers.Instance.player.transform.position.z);
        //Managers.Instance.player.transform.position = newPos;
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.position = Managers.Instance.player.transform.position;
    }

    void Update()
    {
        if (stopGeneration == false)// && timeBtwRoom <= 0)
        {
            Move();
        }
            /*
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }*/
    }

    private void Move()
    {
        Vector3 newPos;
        int roomRand;

        if (direction <= right_rand_max) // move right
        {
            if(transform.position.x < maxX) 
            {

                BottomCounter = 0;
                newPos = new Vector3(transform.position.x + x_moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(right_rand_min, bottom_rand_max + 1);

                roomRand = Random.Range(0, rooms.Length);
                RoomInstantiate(roomRand);


                if (direction >= left_rand_min && direction <= left_rand_max)
                {

                    float total = (right_rand_max - right_rand_min + 1) + (bottom_rand_max - bottom_rand_min + 1);
                    float RvsB_ratio = (right_rand_max - right_rand_min + 1) / total;
                    if (Random.Range(0.0f, 1.0f) < RvsB_ratio)
                        direction = right_rand_min;
                    else
                        direction = bottom_rand_min;



                }
            }
            else
            {
                direction = bottom_rand_min;
            }

        } else if(direction <= left_rand_max) // move left
        {
            if (transform.position.x > minX)
            {
                BottomCounter = 0;
                newPos = new Vector3(transform.position.x - x_moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(left_rand_min, bottom_rand_max + 1);
                roomRand = Random.Range(0, rooms.Length);
                RoomInstantiate(roomRand);
            }
            else
            {
                direction = bottom_rand_max;
            }
        }
        else if (direction <= bottom_rand_max) // move down
        {
            BottomCounter++;
            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, RoomLayerMask);
                if (roomDetection == null)
                    Debug.Log("no detection need to debug");
                else
                {
                    if (roomDetection.GetComponentInParent<RoomType>().type != LRB_room_type && roomDetection.GetComponentInParent<RoomType>().type != LRBT_room_type)
                    {
                        if (BottomCounter >= 2)
                        {
                            roomDetection.GetComponentInParent<RoomType>().RoomDestruction();
                            RoomInstantiate(LRBT_room_type);
                        }
                        else
                        {
                            roomDetection.GetComponentInParent<RoomType>().RoomDestruction();
                            roomRand = Bottom_room_type_arr[Random.Range(0, Bottom_room_type_arr.Length)];
                            RoomInstantiate(roomRand);
                        }
                    }
                }
                newPos = new Vector3(transform.position.x, transform.position.y - y_moveAmount, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(right_rand_min, bottom_rand_max + 1);

                // room need top path
                roomRand = Top_room_type_arr[Random.Range(0, Top_room_type_arr.Length)];
                RoomInstantiate(roomRand);
            }
            else
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, RoomLayerMask);
                Transform exitportal = roomDetection.transform.parent.Find("Portal");
                exitportal.gameObject.SetActive(true);

                stopGeneration = true;
            }
        }


    }

    private void RoomInstantiate(int roomIdx)
    {
        Instantiate(rooms[roomIdx], transform.position, Quaternion.identity);
    }

    private void InitialRoomSetting()
    {
        startingPositions = GameObject.FindGameObjectWithTag("RoomStartingPositions").GetComponentsInChildren<Transform>();
        minX = startingPositions[1].transform.position.x;
        maxX = startingPositions[startingPositions.Length - 1].transform.position.x;
        minY = startingPositions[1].transform.position.y - 30;
        
        Bottom_room_type_arr = new int[] { LRB_room_type, LRBT_room_type };
        Top_room_type_arr = new int[] { LRT_room_type, LRBT_room_type };
        RoomLayerMask = LayerMask.GetMask("Room");

        int randStartingPos = Random.Range(1, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;

        direction = Random.Range(right_rand_min, bottom_rand_max + 1);
        if(direction >= bottom_rand_min)
        {
            Instantiate(rooms[Bottom_room_type_arr[Random.Range(0, Bottom_room_type_arr.Length)]], transform.position, Quaternion.identity);
            BottomCounter++;
        }
        else
        {
            Instantiate(rooms[0], transform.position, Quaternion.identity);

        }


    }
}

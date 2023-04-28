using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float CameraSpeed = 2f;
    public float yoffset = 1f;
    GameObject player;
    void Start()
    {
        player = Managers.Instance.player;

    }

    void Update()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + yoffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, CameraSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    const float knockbackforce_x = 100f;
    const float knockbackforce_y = 2f;
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MonsterBody"))
        {

        }
    }
}

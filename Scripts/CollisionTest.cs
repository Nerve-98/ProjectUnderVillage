using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name);
    }
}

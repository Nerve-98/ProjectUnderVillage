using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    BoxCollider2D hitbox;
    private void Awake()
    {
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

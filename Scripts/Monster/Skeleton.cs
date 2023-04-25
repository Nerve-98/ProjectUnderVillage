using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    void Start()
    {



    }

    void Update()
    {



    }
    private void FixedUpdate()
    {
        
    }
#pragma warning disable CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    protected void OnTriggerEnter2D(Collider2D collision)
#pragma warning restore CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.CompareTag("PlayerAttack"))
        {
            collision.enabled = false;
            Debug.Log("Skeleton enter " + collision.name);
        }
    }


}

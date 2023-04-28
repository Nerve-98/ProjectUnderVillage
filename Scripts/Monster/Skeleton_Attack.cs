using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : MonoBehaviour
{
    Collider2D[] Attacks;
    private void Awake()
    {
        Attacks = GetComponentsInChildren<Collider2D>();
    }
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

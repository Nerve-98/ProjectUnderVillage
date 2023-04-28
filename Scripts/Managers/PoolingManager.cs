using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager s_instance;
    public static PoolingManager Instance { get { return s_instance; } }

    public GameObject ore_drop_pooling;
    void Awake()
    {
        s_instance = this;
        ore_drop_pooling = GameObject.Find("OreDropPooling");
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

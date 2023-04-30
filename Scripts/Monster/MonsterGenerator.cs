using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject[] MonsterPrefab;
    int MonsterPrefab_cnt;
    int monster_rand_max = 3; // now 1 Skeleton / 2 = 50%
    private void Awake()
    {
        MonsterPrefab_cnt = MonsterPrefab.Length;
        //Debug.Log("cnt :" + MonsterPrefab_cnt);
        int rand = Random.Range(0, monster_rand_max);
        //Debug.Log("rand :" + rand);
        if (rand < MonsterPrefab_cnt)
        {
            GameObject monster = Instantiate(MonsterPrefab[rand], transform.position, Quaternion.identity);
            monster.transform.parent = transform;
        }

    }
    void Start()
    {

    }

    void Update()
    {

    }
}

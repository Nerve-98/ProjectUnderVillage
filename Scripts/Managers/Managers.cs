using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField]
    static int monstercnt;

    public GameObject player;
    public GameObject MonsterPooling;


    private static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }






    private void Awake()
    {
        Init();
        player = GameObject.Find("Player");
        MonsterPooling = GameObject.Find("MonsterPooling");
    }

    void Start()
    {
        monstercnt = GameObject.FindGameObjectsWithTag("Monster").Length;
    }

    void Update()
    {
    }

    public void DecreaseMonsterCnt()
    {
        monstercnt--;
        return;
    }
    public int GetMonsterCnt()
    {
        return monstercnt;
    }

    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();

            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }

    }
}

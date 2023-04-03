using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour

{
    private static EffectManager s_instance;
    public static EffectManager Instance { get { return s_instance; } }

    public bool CanUsePlayerEffect = true;
    public Vector3 PlayerEskill_x_offset = new Vector3(-0.5f,0,0);
    public float PlayerEskillRange = 8.0f;
    void Awake()
    {
        s_instance = this;
        CanUsePlayerEffect = true;
    }
    void Start()
    {

    }
    void Update()
    {
    }

}

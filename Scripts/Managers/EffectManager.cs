using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour

{
    private static EffectManager s_instance;
    public static EffectManager Instance { get { return s_instance; } }

    public bool CanUsePlayerEffect = true;

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

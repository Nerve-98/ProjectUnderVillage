using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillInfo : MonoBehaviour
{
    public bool is_Ecool = false;
    public float Ecooltime_setting = 5f;
    public float Ecooltime = -1f;
    void Start()
    {

    }

    void Update()
    {
        if(is_Ecool)
        {
            Ecooltime -= Time.deltaTime;
            if(Ecooltime < 0)
            {
                is_Ecool = false;
            }
        }
    }
    public void ResetEskill()
    {
        is_Ecool = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSkillEffect : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
    }
    void DeactivateSkillEffectFunc() // Animation Event
    {
        gameObject.SetActive(false);
    }
}

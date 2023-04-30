using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{

    [SerializeField]
    private Image skillcooldown;
    [SerializeField]
    private TMPro.TextMeshProUGUI textCooldown;



    void Start()
    {
        textCooldown = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textCooldown.gameObject.SetActive(false);
        skillcooldown.fillAmount = 0.0f;

    }

    void Update()
    {
        SkillCooldownUpdate();
    }

    void SkillCooldownUpdate()
    {
        PlayerSkillInfo info = Managers.Instance.playerskillinfo;
        if(info.is_Ecool)
        {
            skillcooldown.fillAmount = info.Ecooltime / info.Ecooltime_setting;
            textCooldown.gameObject.SetActive(true);
            textCooldown.text = Mathf.Ceil(info.Ecooltime).ToString();
        }
        else
        {
            textCooldown.gameObject.SetActive(false);
            skillcooldown.fillAmount = 0.0f;
        }
    }
}

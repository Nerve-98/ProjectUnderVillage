using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerESkill : MonoBehaviour
{
    Transform[] EffectsTransformArr;
    [SerializeField]
    GameObject EffectPrefab;
    private void Awake()
    {
        EffectsTransformArr = GetComponentsInChildren<Transform>(true);
    }
    void Start()
    {

    }
    void Update()
    {
        ActivateESkill();
    }

    void ActivateESkill()
    {
        // todo: cooltime
        if(Input.GetKeyDown(KeyCode.E) && EffectManager.Instance.CanUsePlayerEffect)
        {
            GameObject Effect;
            for (int i = 0; i < EffectsTransformArr.Length; i++)
            {
                Effect = EffectsTransformArr[i].gameObject;
                if (!Effect.activeSelf)
                {
                    Effect.transform.position = Managers.Instance.player.transform.position;
                    Effect.SetActive(true);
                    return;
                }
            }

            // Effect pooling because all effects are using by great player
            Effect = Instantiate(EffectPrefab);
            Effect.transform.SetParent(gameObject.transform);
            Effect.transform.position = Managers.Instance.player.transform.position;
            Effect.SetActive(true);
            EffectsTransformArr = GetComponentsInChildren<Transform>(true);
        }
    }

}

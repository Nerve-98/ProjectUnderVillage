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
                    Effect.transform.position = Managers.Instance.player.transform.position + EffectManager.Instance.PlayerEskill_x_offset;
                    Effect.SetActive(true);
                    EskillReposition(Effect);
                    return;
                }
            }

            // Effect pooling because all effects are using by great player
            Effect = Instantiate(EffectPrefab);
            Effect.transform.SetParent(gameObject.transform);
            Effect.transform.position = Managers.Instance.player.transform.position + EffectManager.Instance.PlayerEskill_x_offset;
            Effect.SetActive(true);
            EffectsTransformArr = GetComponentsInChildren<Transform>(true);
            EskillReposition(Effect);
            return;
        }
    }
    void EskillReposition(GameObject Effect)
    {
        Vector3 originalScale = Effect.transform.localScale;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 dir = mouseWorldPosition - Managers.Instance.player.transform.position;
        dir.z = 0;

        // player warp 
        Managers.Instance.player.transform.position += dir.normalized * EffectManager.Instance.PlayerEskillRange;
        // Todo : need collision check for using skill for wall


        // effect reposition
            
        Effect.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        Effect.transform.rotation *= Quaternion.Euler(0, 0, 90);

    }
}

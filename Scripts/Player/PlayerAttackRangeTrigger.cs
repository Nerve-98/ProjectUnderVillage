using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRangeTrigger : MonoBehaviour
{
    BoxCollider2D attackrange;
    Vector3 initialposition;
    float airattackduration = 0.3f;
    const float airattackduration_const = 0.3f;
    private void Awake()
    {
        attackrange = GetComponent<BoxCollider2D>();
        initialposition = gameObject.transform.position;
        
    }
    void Start()
    {

    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if(attackrange.enabled == true)
        {
            airattackduration -= Time.deltaTime;
            if (airattackduration < 0)
            {
                attackrange.enabled = false;
                airattackduration = airattackduration_const;
            }
            Vector3 temp = attackrange.transform.position;
            temp.x = temp.x + 0.001f;
            attackrange.transform.position = temp;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        airattackduration = airattackduration_const;
        gameObject.transform.position = Managers.Instance.player.transform.position;
        attackrange.enabled = false;
        //Debug.Log("Attack Range hit with : " + collision.name);
    }
}

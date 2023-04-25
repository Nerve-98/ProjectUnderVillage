using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRangeTrigger : MonoBehaviour
{
    BoxCollider2D attackrange;
    Vector3 initialposition;
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
            Vector3 temp = attackrange.transform.position;
            temp.x = temp.x + 0.001f;
            attackrange.transform.position = temp;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.transform.position = Managers.Instance.player.transform.position;
        attackrange.enabled = false;
        Debug.Log("Attack Range hit with : " + collision.name);
    }
}

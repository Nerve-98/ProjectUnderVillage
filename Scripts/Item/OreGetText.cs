using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreGetText : MonoBehaviour
{
    private Vector3 moveSpeed = Vector3.up;

    RectTransform textTransform;

    private const float lifetime = 1f;
    private float duration = 0f;
    private void Awake()
    {

    }
    void Start()
    {

    }

    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime;

        duration += Time.deltaTime;
        if (duration > lifetime)
            Destroy(gameObject);

    }
}

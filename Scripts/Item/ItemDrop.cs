using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRb = null;
    public float dropForce_y = 4;
    public float dropForce_x = 2;

    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    public void itemdropforce()
    {
        if(itemRb == null)
            itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(new Vector2(Random.Range(-1, 1) * dropForce_x, Random.Range(0.7f, 1) * dropForce_y) , ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Item" || collision.gameObject.tag == "Monster")
        {
            Physics2D.IgnoreCollision(collision.otherCollider, collision.collider);
        }
        else if(collision.gameObject.tag == "Player")
        {
            SoundManager.Instance.Play(Define.Sound.Effect, "Sound/OreGetSound");
            //Debug.Log(collision.gameObject.name);
            UIManager.Instance.OreGetText(Random.Range(7, 13), gameObject.transform);
            gameObject.SetActive(false);
        }
    }
}

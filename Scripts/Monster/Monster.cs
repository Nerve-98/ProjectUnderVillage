using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected BoxCollider2D hitbox;
    protected Rigidbody2D rb;

    protected float speed = 0.6f;
    protected float distance;
    protected Vector3 position_play_monster_diff;
    protected Animator Anim;
    protected SpriteRenderer Monster_SR;
    protected Material Monster_OriginMaterial;
    protected Material Monster_HitMaterial;
    private void Awake()
    {
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Monster_SR = GetComponent<SpriteRenderer>();
        Monster_OriginMaterial = Monster_SR.material;
        //Debug.Log(Monster_OriginMaterial.name);
        Monster_HitMaterial = Resources.Load("Material/MonsterHit", typeof(Material)) as Material;

        //Debug.Log(Monster_HitMaterial.name); 
        Anim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    void Update()
    {
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Collision
    protected BoxCollider2D hitbox;
    protected Rigidbody2D rb;

    // Visual
    protected Animator Anim;
    protected SpriteRenderer Monster_SR;
    protected Material Monster_OriginMaterial;
    protected Material Monster_HitMaterial;

    // distance
    protected float distance;
    protected Vector3 position_play_monster_diff;

    // Hp
    protected float HPshowduration = 3;
    protected GameObject HPcanvas;
    protected UnityEngine.UI.Slider[] HPslider;
    protected int redhpindex = 0;
    protected int orangehpindex = 1;

    protected float MaxHp = 30;
    protected float hpdisapperspeed = 10;
    protected float hp;
    protected float hp_orange;



    // Info
    protected float speed = 0.6f;


    private void Awake()
    {
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Monster_SR = GetComponent<SpriteRenderer>();
        Monster_OriginMaterial = Monster_SR.material;
        //Debug.Log(Monster_OriginMaterial.name);
        Monster_HitMaterial = Resources.Load("Material/MonsterHit", typeof(Material)) as Material;

        HPcanvas = transform.GetChild(5).gameObject;
        HPslider = HPcanvas.GetComponentsInChildren<UnityEngine.UI.Slider>();
        //Debug.Log(HPslider[0].gameObject.name);
        //Debug.Log(HPslider.name);
        //Debug.Log(Monster_HitMaterial.name); 
        Anim = GetComponent<Animator>();

        hp = MaxHp;
        hp_orange = MaxHp;
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

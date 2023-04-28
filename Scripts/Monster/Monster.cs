using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Collision
    protected BoxCollider2D hitbox;
    protected Rigidbody2D rb;

    // Collision flip
    protected Vector2 hitbox_offset;

    // Attack Range
    protected BoxCollider2D AttackTrigger1;
    protected BoxCollider2D AttackTrigger2;

    protected Vector2 AttackTrigger1_offset;
    protected Vector2 AttackTrigger2_offset;


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
    public float hp;
    protected float hp_orange;


    // hit 
    protected bool is_hit = false;
    protected float is_hit_duration;
    protected const float is_hit_duration_const = 0.15f;

    // Info
    protected float speed = 0.6f;


    private void Awake()
    {

    }
    void Start()
    {

        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Monster_SR = GetComponent<SpriteRenderer>();
        Monster_OriginMaterial = Monster_SR.material;
        Monster_HitMaterial = Resources.Load("Material/MonsterHit", typeof(Material)) as Material;

        HPcanvas = transform.GetChild(5).gameObject;
        HPslider = HPcanvas.GetComponentsInChildren<UnityEngine.UI.Slider>();
        Anim = GetComponent<Animator>();


        BoxCollider2D[] attacktriggers = transform.GetChild(6).GetComponentsInChildren<BoxCollider2D>();
        if (attacktriggers.Length >= 2)
        {
            AttackTrigger1 = attacktriggers[0];
            AttackTrigger2 = attacktriggers[1];
        }
        AttackTrigger1_offset = AttackTrigger1.offset;
        AttackTrigger2_offset = AttackTrigger2.offset;

        hitbox_offset = hitbox.offset;


        hp = MaxHp;
        hp_orange = MaxHp;
    }

    void Update()
    {

    }
    protected void Hitupdate() // white hit effect
    {
        if (is_hit)
        {
            is_hit_duration -= Time.deltaTime;
            if (is_hit_duration < 0)
            {
                is_hit = false;
                Monster_SR.material = Monster_OriginMaterial;
            }
        }
    }

    protected void HPupdate() // hp slider
    {
        if (HPcanvas.activeSelf)
        {
            if (hp_orange > hp)
            {
                hp_orange -= Time.deltaTime * hpdisapperspeed;
            }
            else
            {
                hp_orange = hp;
            }
            HPslider[redhpindex].value = Mathf.Max(0, hp / MaxHp);
            HPslider[orangehpindex].value = Mathf.Max(0, hp_orange / MaxHp);
        }
    }

    public void MonsterHit(Collider2D collision, string skillsound)
    {
        SoundManager.Instance.Play(Define.Sound.Effect, $"Sound/{skillsound}");
        HPcanvas.SetActive(true);
        //Debug.Log(HPcanvas.name);
        HitWhite();
    }
    public void HitWhite()
    {
        Monster_SR.material = Monster_HitMaterial;
        is_hit_duration = is_hit_duration_const;
        is_hit = true;
    }
}

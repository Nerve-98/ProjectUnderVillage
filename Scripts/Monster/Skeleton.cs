using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    const float skeleton_recognize_distance = 6.0f;
    const float skeleton_attack_range = 1.5f;


    public enum SkeletonState
    {
        Idle,
        Walk,
        Attack1,
        Death,
        Death2
    }


    SkeletonState _state = SkeletonState.Idle;
    bool is_hit = false;
    float is_hit_duration;
    const float is_hit_duration_const = 0.15f;


    void Update()
    {
        if (HPcanvas.activeSelf)
        {
            if(hp_orange > hp)
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
        if(hp <= 0 && _state != SkeletonState.Death2)   
        {
            _state = SkeletonState.Death;
        }


        switch(_state)
        {
            case SkeletonState.Idle:
                UpdateIdle();
                break;
            case SkeletonState.Walk:
                UpdateWalk();
                break;
            case SkeletonState.Death:
                UpdateDeath();
                break;
            case SkeletonState.Death2: // end update by object enabled false
                UpdateDeath2();
                break;
            default:
                break;
        }

        if(is_hit)
        {
            is_hit_duration -= Time.deltaTime;
            if(is_hit_duration < 0)
            {
                is_hit = false;
                Monster_SR.material = Monster_OriginMaterial;
            }
        }
    }
    private void AE_UpdateDeath2()
    {
        Anim.SetBool("SkeletonDeath2", true);
    }
    private void UpdateDeath2()
    {
        if (hp_orange < 0.01)
        {
            gameObject.SetActive(false);
            gameObject.transform.parent = Managers.Instance.MonsterPooling.transform;
            _state = SkeletonState.Idle;
            is_hit = false;
            is_hit_duration = is_hit_duration_const;
            Monster_SR.material = Monster_OriginMaterial;
            Anim.SetBool("IdleToWalk", false);
            Anim.SetBool("WalkToIdle", false);
            Anim.SetBool("SkeletonDeath2", false);
        }
    }
    private void UpdateDeath()
    {
        Anim.SetTrigger("SkeletonDeath");
        _state = SkeletonState.Death2;
    }

    void UpdateWalk()
    {
        Anim.SetBool("IdleToWalk", false);
        position_play_monster_diff = Managers.Instance.player.transform.position - gameObject.transform.position;
        distance = Vector3.Magnitude(Managers.Instance.player.transform.position - gameObject.transform.position);
        float velocity_dir;
        if(distance < skeleton_recognize_distance && skeleton_attack_range < Mathf.Abs(position_play_monster_diff.x))
        {
            if (position_play_monster_diff.x < 0)
            {
                velocity_dir = -1;
                Monster_SR.flipX = true;
            }
            else
            {
                velocity_dir = 1;
                Monster_SR.flipX = false;
            }
            rb.velocity = new Vector2(speed * velocity_dir, 0);
        }
        else
        {
            Anim.SetBool("WalkToIdle", true);
            _state = SkeletonState.Idle;
        }
    }
    void UpdateIdle()
    {
        Anim.SetBool("WalkToIdle", false);
        rb.velocity = Vector2.zero;
        position_play_monster_diff = Managers.Instance.player.transform.position - gameObject.transform.position;
        distance = Vector3.Magnitude(Managers.Instance.player.transform.position - gameObject.transform.position);
       // Debug.Log("distance : " + distance);
        //Debug.Log(gameObject.name);
        if (distance < skeleton_recognize_distance && Mathf.Abs(position_play_monster_diff.x) > skeleton_attack_range)
        {
            Anim.SetBool("IdleToWalk",true);
            _state = SkeletonState.Walk;
        }
    }
    private void FixedUpdate()
    {
        
    }
#pragma warning disable CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    protected void OnTriggerEnter2D(Collider2D collision)
#pragma warning restore CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.CompareTag("PlayerAttack"))
        {
            AttackTrigger(collision, "PlayerAttackHitSound");
            hp -= DataManager.Instance.StatDict["SwordCharacter"].attackdmg;
            /*
            collision.enabled = false;
            SoundManager.Instance.Play(Define.Sound.Effect, "Sound/PlayerAttackHitSound");
            HPcanvas.enabled = true;
            HitWhite(); */
            //Debug.Log("Skeleton enter " + collision.name);
        }
        else if (collision.transform.CompareTag("PlayerSkillAttack"))
        {
            AttackTrigger(collision, "EskillHitSound");
            hp -= DataManager.Instance.StatDict["SwordCharacter"].eskillattackdmg;
            /*
            //Debug.Log("Skeleton skill " + collision.name);
            SoundManager.Instance.Play(Define.Sound.Effect, "Sound/EskillHitSound");
            HPcanvas.enabled = true;
            HitWhite();*/

        }
    }
    void HitWhite()
    {
        Monster_SR.material = Monster_HitMaterial;
        is_hit_duration = is_hit_duration_const;
        is_hit = true;
    }
    void AttackTrigger(Collider2D collision, string skillsound)
    {
        SoundManager.Instance.Play(Define.Sound.Effect, $"Sound/{skillsound}");
        HPcanvas.SetActive(true);
        HitWhite();
    }
}

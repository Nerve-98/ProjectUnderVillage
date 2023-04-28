using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public GameObject ore_prefab;

    const float skeleton_recognize_distance = 6.0f;
    const float skeleton_attack_range = 1.5f;

    const float skeleton_attack_cooltime_const = 3f;
    float skeleton_attack_cooltime = -1f;


    const float skeleton_attack_period_const = 0.5f;
    float skeleton_attack_period = 0.5f;

    const int ore_drop_cnt = 5;

    public enum SkeletonState
    {
        Idle,
        Walk,
        Attack1,
        Attack2,
        Death,
        Death2
    }


    SkeletonState _state = SkeletonState.Idle;


    void Update()
    {
        HPupdate();
        Hitupdate();

        if (hp <= 0.01 && _state != SkeletonState.Death2)   
        {
            hitbox.enabled = false;
            _state = SkeletonState.Death;
            ItemDrop();
        }

        if (skeleton_attack_cooltime >= 0)
            skeleton_attack_cooltime -= Time.deltaTime;

        switch (_state)
        {
            case SkeletonState.Idle:
                UpdateIdle();
                break;
            case SkeletonState.Walk:
                UpdateWalk();
                break;
            case SkeletonState.Attack1:
                break;
            case SkeletonState.Death:
                UpdateDeath();
                break;
            case SkeletonState.Death2: // end update by object enabled false
                break;
            default:
                break;
        }
    }
    private void AE_Attack1_On()
    {
        AttackTrigger1.enabled = true;
    }
    private void AE_Attack1_Off()
    {
        AttackTrigger1.enabled = false;
    }
    private void AE_Attack2_On()
    {
        AttackTrigger2.enabled = true;
    }
    private void AE_UpdateAttack2()
    {
        Anim.SetBool("IdleToAttack", false);
        AttackTrigger2.enabled = false;
        _state = SkeletonState.Idle;
    }
    private void AE_UpdateDeath()
    {
        Anim.SetBool("SkeletonDeath2", true);
    }
    private void AE_UpdateDeath2() // really death -> intialize all parameter for pooling
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
                hitbox.offset = new Vector2(-hitbox_offset.x, hitbox_offset.y);
                AttackTrigger1.offset = new Vector2(-AttackTrigger1_offset.x, AttackTrigger1_offset.y);
                //Debug.Log(AttackTrigger1.name);
                //Debug.Log(AttackTrigger1.offset);
                AttackTrigger2.offset = new Vector2(-AttackTrigger2_offset.x, AttackTrigger2_offset.y);
            }
            else
            {
                velocity_dir = 1;
                Monster_SR.flipX = false;
                hitbox.offset = new Vector2(hitbox_offset.x, hitbox_offset.y);
                AttackTrigger1.offset = new Vector2(AttackTrigger1_offset.x, AttackTrigger1_offset.y);
                AttackTrigger2.offset = new Vector2(AttackTrigger2_offset.x, AttackTrigger2_offset.y);
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
        Anim.SetBool("IdleToAttack", false);
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
        else if(distance < skeleton_recognize_distance && Mathf.Abs(position_play_monster_diff.x) < skeleton_attack_range)
        {
            skeleton_attack_period -= Time.deltaTime;
            if(skeleton_attack_period < 0 && skeleton_attack_cooltime < 0)
            {
                skeleton_attack_period = skeleton_attack_period_const;
                if (UnityEngine.Random.Range(0,2) == 0)
                {
                    skeleton_attack_cooltime = skeleton_attack_cooltime_const;
                    Anim.SetBool("IdleToAttack", true);
                    _state = SkeletonState.Attack1;
                }
            }

        }
    }
    private void ItemDrop()
    {
        Transform[] ores = ore_drop_pooling.GetComponentsInChildren<Transform>(true);
        //Debug.Log(ores.Length);
        if(ores.Length - 1 < ore_drop_cnt)
        {

        }

        for(int i = 1; i <= ore_drop_cnt; i++) {
            ores[i].gameObject.SetActive(true);
            ores[i].transform.position = gameObject.transform.position;
            ores[i].GetComponent<ItemDrop>().itemdropforce();
        }
    }
}

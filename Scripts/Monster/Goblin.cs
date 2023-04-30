using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster
{
    public GameObject ore_prefab;

    const float recognize_distance = 6.0f;
    const float attack_range = 2.5f;

    const float attack_cooltime_const = 3f;
    float attack_cooltime = -1f;

    const float attack_period_const = 0.3f;
    float attack_period = 0f;

    const int ore_drop_cnt = 5;

    float goblin_speed = 3;
    float goblin_MaxHp = 30;

    protected override void Start()
    {
        base.Start();
        MaxHp = goblin_MaxHp;
        hp = MaxHp;
        speed = goblin_speed;
    }

    void Update()
    {
        HPupdate();
        Hitupdate();

        if (hp <= 0.01 && _state != MonsterState.Death2)
        {
            hitbox.enabled = false;
            _state = MonsterState.Death;
            Managers.Instance.playerskillinfo.ResetEskill();
            ItemDrop();
        }

        if (attack_cooltime >= 0)
            attack_cooltime -= Time.deltaTime;

        switch (_state)
        {
            case MonsterState.Idle:
                UpdateIdle();
                break;
            case MonsterState.Walk:
                UpdateWalk();
                break;
            case MonsterState.Attack1:
                break;
            case MonsterState.Death:
                UpdateDeath();
                break;
            case MonsterState.Death2: // end update by object enabled false
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
        _state = MonsterState.Idle;
    }
    private void AE_UpdateDeath()
    {
        Anim.SetBool("Death2", true);
    }
    private void AE_UpdateDeath2() // really death -> intialize all parameter for pooling
    {
        if (hp_orange < 0.01)
        {
            gameObject.SetActive(false);
            gameObject.transform.parent = Managers.Instance.MonsterPooling.transform;
            _state = MonsterState.Idle;
            is_hit = false;
            is_hit_duration = is_hit_duration_const;
            Monster_SR.material = Monster_OriginMaterial;
            Anim.SetBool("IdleToWalk", false);
            Anim.SetBool("WalkToIdle", false);
            Anim.SetBool("Death2", false);
        }
    }

    private void UpdateDeath()
    {
        Anim.SetTrigger("Death");
        _state = MonsterState.Death2;
    }

    void UpdateWalk()
    {
        Anim.SetBool("IdleToWalk", false);
        position_play_monster_diff = Managers.Instance.player.transform.position - gameObject.transform.position;
        distance = Vector3.Magnitude(Managers.Instance.player.transform.position - gameObject.transform.position);
        float velocity_dir;
        if (distance < recognize_distance && attack_range < Mathf.Abs(position_play_monster_diff.x))
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
            _state = MonsterState.Idle;
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
        if (distance < recognize_distance && Mathf.Abs(position_play_monster_diff.x) > attack_range)
        {
            Anim.SetBool("IdleToWalk", true);
            _state = MonsterState.Walk;
        }
        else if (distance < recognize_distance && Mathf.Abs(position_play_monster_diff.x) < attack_range)
        {
            attack_period -= Time.deltaTime;
            if (attack_period < 0 && attack_cooltime < 0)
            {
                attack_period = attack_period_const;
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    attack_cooltime = attack_cooltime_const;
                    Anim.SetBool("IdleToAttack", true);
                    _state = MonsterState.Attack1;
                }
            }

        }
    }
    private void ItemDrop()
    {
        Transform[] ores = ore_drop_pooling.GetComponentsInChildren<Transform>(true);
        //Debug.Log(ores.Length);
        if (ores.Length - 1 < ore_drop_cnt)
        {

        }

        for (int i = 1; i <= ore_drop_cnt; i++)
        {
            ores[i].gameObject.SetActive(true);
            ores[i].transform.position = gameObject.transform.position;
            //Debug.Log(ores[i].name);
            ores[i].GetComponent<ItemDrop>().itemdropforce();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_HitTrigger : Skeleton
{
    Skeleton skeleton;

    protected override void Start()
    {
        skeleton = transform.parent.GetComponent<Skeleton>();
    }

    void Update()
    {

    }
#pragma warning disable CS0108 // ����� ��ӵ� ����� ����ϴ�. new Ű���尡 �����ϴ�.
    void OnTriggerEnter2D(Collider2D collision)
#pragma warning restore CS0108 // ����� ��ӵ� ����� ����ϴ�. new Ű���尡 �����ϴ�.
    {

        if (collision.CompareTag("PlayerAttack"))
        {
            CharacterStat character = DataManager.Instance.StatDict["SwordCharacter"];
            UIManager.Instance.DamageText(character.attackdmg, transform);
            skeleton.MonsterHit(collision, "PlayerAttackHitSound");
            skeleton.hp -= character.attackdmg;
            /*
            collision.enabled = false;
            SoundManager.Instance.Play(Define.Sound.Effect, "Sound/PlayerAttackHitSound");
            HPcanvas.enabled = true;
            HitWhite(); */
            //Debug.Log("Skeleton enter " + collision.name);
        }
        else if (collision.CompareTag("PlayerSkillAttack"))
        {
            CharacterStat character = DataManager.Instance.StatDict["SwordCharacter"];
            UIManager.Instance.DamageText(character.eskillattackdmg, transform);
            skeleton.MonsterHit(collision, "EskillHitSound");
            skeleton.hp -= character.eskillattackdmg;
            /*
            //Debug.Log("Skeleton skill " + collision.name);
            SoundManager.Instance.Play(Define.Sound.Effect, "Sound/EskillHitSound");
            HPcanvas.enabled = true;
            HitWhite();*/
        }
    }
}

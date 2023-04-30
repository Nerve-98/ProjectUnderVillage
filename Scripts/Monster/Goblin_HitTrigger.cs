using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_HitTrigger : Skeleton
{
    Goblin goblin;

    protected override void Start()
    {
        goblin = transform.parent.GetComponent<Goblin>();
    }

    void Update()
    {

    }
#pragma warning disable CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    void OnTriggerEnter2D(Collider2D collision)
#pragma warning restore CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    {

        if (collision.CompareTag("PlayerAttack"))
        {
            CharacterStat character = DataManager.Instance.StatDict["SwordCharacter"];
            UIManager.Instance.DamageText(character.attackdmg, transform);
            goblin.MonsterHit(collision, "PlayerAttackHitSound");
            goblin.hp -= character.attackdmg;
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
            goblin.MonsterHit(collision, "EskillHitSound");
            goblin.hp -= character.eskillattackdmg;
            /*
            //Debug.Log("Skeleton skill " + collision.name);
            SoundManager.Instance.Play(Define.Sound.Effect, "Sound/EskillHitSound");
            HPcanvas.enabled = true;
            HitWhite();*/
        }
    }
}

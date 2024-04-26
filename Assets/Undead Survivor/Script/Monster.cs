using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Monster : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    Rigidbody2D targetRigid;

    bool isLive;
    public RuntimeAnimatorController[] aniController;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator monsterAni;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAni = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        targetRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    private void FixedUpdate()
    {
        //����� �������� ����
        if(!isLive)
            return;

        //target�� ������ ��� target�� ���Ͽ� �̵��ϱ�
        if(targetRigid != null)
        {
            Vector2 dirVec = targetRigid.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }
    private void LateUpdate()
    {
        //�̵��� �ٶ󺸴� ����
        if(isLive)
            spriteRenderer.flipX = targetRigid.position.x > rigid.position.x ? false : true;
    }

    public void Init(LevelSpawnData data)
    {
        monsterAni.runtimeAnimatorController = aniController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {

        }else
        {
            Dead();
        }

    }

    void Dead()
    {
        isLive = false;
        monsterAni.SetBool("Dead", true);

        gameObject.SetActive(false);
    }
}

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
    Collider2D coll;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAni = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        targetRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 2;
        monsterAni.SetBool("Dead", false);
        health = maxHealth;
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        if (!isLive || monsterAni.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        if (targetRigid != null)
        {
            Vector2 dirVec = targetRigid.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.linearVelocity = Vector2.zero;
        }
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        if (isLive)
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
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            StartCoroutine(MonsterHit());
        } else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriteRenderer.sortingOrder = 1;
            monsterAni.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator MonsterHit()
    {
        monsterAni.SetTrigger("Hit");
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dir = transform.position - playerPos;
        rigid.AddForce(dir.normalized * 3, ForceMode2D.Impulse);
        yield return null;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}

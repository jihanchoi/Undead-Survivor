using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed;
    Rigidbody2D targetRigid;

    bool isLive = true;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator monsterAni;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAni = GetComponent<Animator>();
        targetRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //사망시 움직임을 방지
        if(!isLive)
            return;

        //target이 존재할 경우 target을 향하여 이동하기
        Vector2 dirVec = targetRigid.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        //이동시 바라보는 방향
        spriteRenderer.flipX = targetRigid.position.x > rigid.position.x ? false : true;
    }
}

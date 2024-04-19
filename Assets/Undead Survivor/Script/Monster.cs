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
        //����� �������� ����
        if(!isLive)
            return;

        //target�� ������ ��� target�� ���Ͽ� �̵��ϱ�
        Vector2 dirVec = targetRigid.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        //�̵��� �ٶ󺸴� ����
        spriteRenderer.flipX = targetRigid.position.x > rigid.position.x ? false : true;
    }
}

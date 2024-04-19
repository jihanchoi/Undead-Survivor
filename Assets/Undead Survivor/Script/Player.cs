using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 inputVelue;
    Rigidbody2D rigd;
    SpriteRenderer sprite;
    Animator animator;

    private void Awake()
    {
        rigd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //OnMove()�� �޾ƿ� ������ Player �����̱�
        Vector2 moveVec = inputVelue * moveSpeed * Time.fixedDeltaTime;
        rigd.MovePosition(rigd.position + moveVec);
    }
    private void LateUpdate()
    {
        //Player�� �ִϸ��̼ǰ� �ٶ󺸴� ���� �����ϱ�
        animator.SetFloat("Speed", inputVelue.magnitude);
        if(inputVelue.x != 0)
        {
            sprite.flipX = inputVelue.x < 0 ? true : false;
        }
    }

    //PlayerInput�� �̿��Ͽ� �����̴� ���Ⱚ ��������
    void OnMove(InputValue value)
    {
        inputVelue = value.Get<Vector2>();
    }
}

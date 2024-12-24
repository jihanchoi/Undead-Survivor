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
    public Scanner scanner;
    public Hand[] hands;

    private void Awake()
    {
        rigd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        //OnMove()로 받아온 값으로 Player 움직이기
        Vector2 moveVec = inputVelue * moveSpeed * Time.fixedDeltaTime;
        rigd.MovePosition(rigd.position + moveVec);
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        //Player의 애니메이션과 바라보는 방향 설정하기
        animator.SetFloat("Speed", inputVelue.magnitude);
        if(inputVelue.x != 0)
        {
            sprite.flipX = inputVelue.x < 0 ? true : false;
        }
    }

    //PlayerInput을 이용하여 움직이는 방향값 가져오기
    void OnMove(InputValue value)
    {
        inputVelue = value.Get<Vector2>();
    }

}

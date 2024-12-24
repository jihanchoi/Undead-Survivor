using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos, rightPosReverse;
    Quaternion leftRot, leftRotReverse;

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void Start()
    {
        //Player의 뱡향에 따른 오른손(원거리) 무기의 위치
        rightPos = transform.localPosition;
        rightPosReverse = new Vector3(0.05f, rightPos.y);

        //Player의 방향의 따른 왼손(근거리) 무기의 회전
        leftRot = transform.localRotation;
        leftRotReverse = Quaternion.Euler(leftRot.x, leftRot.y, -135);
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;
        
        if (isLeft) //좌우를 구분하는 bool
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.sortingOrder = isReverse ? 4 : 6;
            spriter.flipY = isReverse;
        }else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.sortingOrder = isReverse ? 6 : 4;
            spriter.flipX = isReverse;
        }

    }
}

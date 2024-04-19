using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))
            return;

        Vector2 playerPosition = GameManager.instance.player.transform.position;
        Vector2 myPosition = transform.position;
        Vector2 playerDir = GameManager.instance.player.inputVelue;

        //Player와 오브젝트 간의 거리구하기
        float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

        //Player가 이동하는 방향을 구하여 어느 방향으로 나갔는지 구하기
        float dirx = playerDir.x > 0 ? 1 : -1;
        float diry = playerDir.y > 0 ? 1 : -1;

        float vel = GameManager.instance.areaLengh * 2;
        switch (transform.tag)
        {
            case "Ground":
                /*타일맵을 Player가 나간 방향으로 이동시켜 타일맵이 이어지도록 하기
                타일맵과 Area는 정사각형으로 하는 것이 비율 계산에서 더 쉬움*/
                if (diffX > diffY)
                {
                    transform.Translate(Vector2.right * dirx * vel);
                }
                if (diffX < diffY)
                {
                    transform.Translate(Vector2.up * diry * vel);
                }
                break;

            case "Monster":
                //Monster가 Player와 거리가 멀 경우 Player가 이동할 방향에서 다시 등장하도록 배치
                Collider2D monsterCollider = GetComponent<Collider2D>();
                if(monsterCollider.enabled)
                {
                    transform.Translate(GameManager.instance.spawner.SpawnPosition());
                }
                break;
        }

        
    }
}

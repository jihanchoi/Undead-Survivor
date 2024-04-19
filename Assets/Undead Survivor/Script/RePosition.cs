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

        //Player�� ������Ʈ ���� �Ÿ����ϱ�
        float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

        //Player�� �̵��ϴ� ������ ���Ͽ� ��� �������� �������� ���ϱ�
        float dirx = playerDir.x > 0 ? 1 : -1;
        float diry = playerDir.y > 0 ? 1 : -1;

        float vel = GameManager.instance.areaLengh * 2;
        switch (transform.tag)
        {
            case "Ground":
                /*Ÿ�ϸ��� Player�� ���� �������� �̵����� Ÿ�ϸ��� �̾������� �ϱ�
                Ÿ�ϸʰ� Area�� ���簢������ �ϴ� ���� ���� ��꿡�� �� ����*/
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
                //Monster�� Player�� �Ÿ��� �� ��� Player�� �̵��� ���⿡�� �ٽ� �����ϵ��� ��ġ
                Collider2D monsterCollider = GetComponent<Collider2D>();
                if(monsterCollider.enabled)
                {
                    transform.Translate(GameManager.instance.spawner.SpawnPosition());
                }
                break;
        }

        
    }
}

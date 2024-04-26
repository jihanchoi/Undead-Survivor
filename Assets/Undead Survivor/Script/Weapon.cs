using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int count;
    public float damage;
    public float speed;

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                break;
        }
    }

    void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            default:
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            Transform bullet = GameManager.instance.poolManager.GetPrefab(prefabId).transform;
            bullet.parent = transform;
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 = ¹«ÇÑ
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

        if(id == 0)
        {
            Batch();
        }
    }
}

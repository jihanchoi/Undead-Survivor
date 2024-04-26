using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public PoolManager poolManager;
    public Spawner spawner;
    public float areaLengh;

    public float gameTime;
    public float maxGameTime = 1 * 60f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }
}

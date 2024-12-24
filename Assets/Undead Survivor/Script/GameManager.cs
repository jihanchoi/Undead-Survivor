using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 1 * 60f;
    public bool isLive;
    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;
    public Spawner spawner;
    public LevelUp uilevelUp;
    public float areaLengh;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp;
    public int health;
    public int maxHealth;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        uilevelUp.Select(0);  //юс╫ц
    }
    private void Update()
    {
        if(!isLive)
        {
            return;
        }
        gameTime += Time.deltaTime;
    }
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            if (nextExp.Length - 1 > level)
            {
                level++;
                uilevelUp.Show();
            }
            exp = 0;
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

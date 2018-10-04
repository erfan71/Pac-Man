using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game_Manager : MonoBehaviour
{
    #region Singleton Pattern
    static Game_Manager _instance;
    public static Game_Manager Instance
    {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Game_Manager>();
            return _instance;
        }
    }
    #endregion

    public HudManager hud;
    public int pickupScore;
    public int pointScore;
    public int enemyScore;
    private int cuurentScore;
    public int enemiesCount;
    public int pickupCount;

    private Enemy[] enemies;

    private int currentPickCount;
    private int currentPointCount;

    void Start()
    {
        MapInitializer.Instance.LoadMap(enemiesCount, pickupCount);
        enemies = GameObject.FindObjectsOfType<Enemy>();
        currentPointCount = MapInitializer.Instance.GetFreeCellCount();
        currentPickCount = pickupCount;
        Debug.Log("Press the SPACE button to start moving");
        Debug.Log("Use Arrow keys to move the Pig");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Actor[] actores=  GameObject.FindObjectsOfType<Actor>();
            foreach(Actor actor in actores)
            {
                actor.Move();
            }
        }
    }
    void GameOver()
    {
        Debug.Log("You Loos... Game Will Restarting");
        SceneManager.LoadScene("Main");
    }
    void CheckGameWin()
    {
        if (currentPointCount==0 && currentPickCount == 0)
        {
            Debug.Log("You Win");
            GameOver();
        }
    }
    public void HitPoint()
    {
        cuurentScore += pointScore;
        hud.SetScore(cuurentScore);
        currentPointCount -= 1;
        CheckGameWin();
    }
    public void HitPickup()
    {
        cuurentScore += pickupScore;
        hud.SetScore(cuurentScore);
        foreach( Enemy enemy in enemies)
        {
            if (enemy != null)
            enemy.GetTame();
        }
        currentPickCount -= 1;
        CheckGameWin();
    }
    public void HitEnemy(Enemy enemy)
    {
        if (enemy.state == Enemy.State.WILD)
        {
            GameOver();
        }
        else
        {
            cuurentScore += enemyScore;
            hud.SetScore(cuurentScore);
            GameObject.Destroy(enemy.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private CollisionAbsorber absorber;

    protected override Cell GetNextMoveCell()
    {
        Cell cell = base.GetNextMoveCell();
        if (cell == null || cell.state == Cell.CellState.WALL)
        {
            return currentCell;
        }
        return cell;
    }
    private void Start()
    {
        absorber = GetComponentInChildren<CollisionAbsorber>();
        absorber.onEnemyHit += OnEnemyHit;
        absorber.onPickupHit += OnPickupHit;
        absorber.onPointHit += OnPointHit;

    }
    private void OnDestroy()
    {
        absorber.onEnemyHit -= OnEnemyHit;
        absorber.onPickupHit -= OnPickupHit;
        absorber.onPointHit -= OnPointHit;
    }
    void OnEnemyHit(GameObject obj)
    {
        Game_Manager.Instance.HitEnemy(obj.GetComponentInParent<Enemy>());
    }
    void OnPickupHit(GameObject obj)
    {
        GameObject.Destroy(obj);
        Game_Manager.Instance.HitPickup();
    }
    void OnPointHit(GameObject obj)
    {
        GameObject.Destroy(obj);
        Game_Manager.Instance.HitPoint();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetTurn(Turn.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetTurn(Turn.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetTurn(Turn.UP);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetTurn(Turn.DOWN);
        }
    }  
}

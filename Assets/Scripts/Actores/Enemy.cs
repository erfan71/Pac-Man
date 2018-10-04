using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public enum State
    {
        WILD,
        TAME,
    }
    public State state = State.WILD;
    public float tameTime = 5;
    private SpriteRenderer renderer;

    protected override Cell GetNextMoveCell()
    {
        Cell cell = base.GetNextMoveCell();
        if (cell == null || cell.state == Cell.CellState.WALL)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
                TurnLeft();
            else
                TurnRight();
            return GetNextMoveCell();
        }
        return cell;
    }
    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void GetTame()
    {
        state = State.TAME;
        CancelInvoke("GetWild");
        Invoke("GetWild", tameTime);
        renderer.color = Color.green;
    }
    void GetWild()
    {
        state = State.WILD;
        renderer.color = Color.white;
    }

}

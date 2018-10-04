using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum Turn
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }
    public enum Rotate
    {
        LEFT,
        RIGHT,
    }
    [SerializeField]
    protected float moveDuration = 0.5f;
    protected Cell currentCell;



    protected Turn currentTurn = Turn.DOWN;

    protected Coroutine routine;
    public void SetCurrentCell(Cell cell)
    {
        currentCell = cell;
    }
    public void TurnLeft()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        UpdateTurn(Rotate.LEFT);
    }
    public void TurnRight()
    {
        transform.Rotate(new Vector3(0, 0, -90));
        UpdateTurn(Rotate.RIGHT);
    }
    private void UpdateTurn(Rotate rotate)
    {
        switch (currentTurn)
        {
            case Turn.LEFT:
                if (rotate == Rotate.LEFT)
                {
                    currentTurn = Turn.DOWN;
                }
                else
                {
                    currentTurn = Turn.UP;
                }
                break;
            case Turn.RIGHT:
                if (rotate == Rotate.LEFT)
                {
                    currentTurn = Turn.UP;
                }
                else
                {
                    currentTurn = Turn.DOWN;
                }
                break;
            case Turn.UP:
                if (rotate == Rotate.LEFT)
                {
                    currentTurn = Turn.LEFT;
                }
                else
                {
                    currentTurn = Turn.RIGHT;
                }
                break;
            case Turn.DOWN:
                if (rotate == Rotate.LEFT)
                {
                    currentTurn = Turn.RIGHT;
                }
                else
                {
                    currentTurn = Turn.LEFT;
                }
                break;
            default:
                break;
        }
    }
    protected virtual Cell GetNextMoveCell()
    {
        Vector2Int index = currentCell.id;
        switch (currentTurn)
        {
            case Turn.LEFT:
                index = new Vector2Int(index.x, index.y - 1);
                break;
            case Turn.RIGHT:
                index = new Vector2Int(index.x, index.y + 1);
                break;
            case Turn.UP:
                index = new Vector2Int(index.x + 1, index.y);
                break;
            case Turn.DOWN:
                index = new Vector2Int(index.x - 1, index.y);
                break;
            default:
                break;
        }
        Cell newCell = MapInitializer.Instance.Get_A_Cell(index.x, index.y);
        return newCell;
    }
    public void SetTurn(Turn turn)
    {
        currentTurn = turn;
        switch (currentTurn)
        {
            case Turn.LEFT:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Turn.RIGHT:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Turn.UP:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Turn.DOWN:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                break;
        }
    }
    public void Move()
    {
        Cell nextCell = GetNextMoveCell();
        routine = StartCoroutine(MoveRoutine(nextCell));
    }
    protected IEnumerator MoveRoutine(Cell nextCell)
    {
        float i = 0.0f;
        float rate = 1 / moveDuration;
        Vector3 newPos = nextCell.transform.position;
        Vector3 startPos = transform.position;
        while (i < 1)
        {
            i += rate * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, newPos, i);
            yield return null;
        }
        transform.position = newPos;
        currentCell = nextCell;
        Move();
    }
}

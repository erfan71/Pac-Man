using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellState state;
    public enum CellState
    {
        WALL = 1,
        FREE = 0,
        PICKUP = 2,
        ENEMY = 3,
        PLAYER = 4,
    }
    public Vector2Int id;
    public Sprite wallSprite;

    private SpriteRenderer renderer;

    // Use this for initialization
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
    }
    public void Setup(Vector2Int id, CellState state)
    {
        this.state = state;
        this.id = id;
        SetState(state);
    }

    protected void Convert_to_Wall()
    {
        ChangeSprite(wallSprite);
        state = CellState.WALL;
       
    }
    void ChangeSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
        renderer.color = Color.white;
    }
    public void SetState(CellState state)
    {
        this.state = state;

        if (state == CellState.WALL)
        {
            Convert_to_Wall();
        }
        if (state != CellState.FREE)
        {
            Transform pickup = transform.Find("Point");
            if (pickup != null)
            {
                GameObject.Destroy(pickup.gameObject);
            }
        }
    }

}

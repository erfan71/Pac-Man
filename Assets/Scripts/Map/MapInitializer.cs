using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializer : MonoBehaviour
{
    #region Singleton Pattern
    static MapInitializer _instance;
    public static MapInitializer Instance
    {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MapInitializer>();
            return _instance;
        }
    }
    #endregion

    // Use this for initialization
    public int mapId;
    private List<List<Cell>> cells;

    public Transform playerPrefab;
    public Transform enemyPrefab;
    private int enemiesCount;

    public Transform pickupPrefab;
    private int pickupCount;

    void Start()
    {

    }
    public void LoadMap(int enemiesCount, int pickupCount)
    {
        this.enemiesCount = enemiesCount;
        this.pickupCount = pickupCount;
        TextAsset map = Resources.Load<TextAsset>("Maps/Map_" + mapId);
        cells = MatrixGenerator.Instance.LoadMapFromString(map.ToString());
        InstantiatePlayer();
        InstantiateEnemies();
        InstantiatePickUps();
    }
    void InstantiatePlayer()
    {
        Cell cell = Get_A_Free_Cell();
        Transform trans = Instantiate(playerPrefab);
        trans.position = cell.transform.position;
        cell.SetState(Cell.CellState.PLAYER);
        trans.GetComponent<Actor>().SetCurrentCell(cell);
    }
    void InstantiatePickUps()
    {
        for (int i = 0; i < pickupCount; i++)
        {
            Cell cell = Get_A_Free_Cell();
            Transform trans = Instantiate(pickupPrefab);
            trans.position = cell.transform.position;
            cell.SetState(Cell.CellState.PICKUP);
        }
    }
    void InstantiateEnemies()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            Cell cell = Get_A_Free_Cell();
            Transform trans = Instantiate(enemyPrefab);
            trans.position = cell.transform.position;
            cell.SetState(Cell.CellState.ENEMY);
            Actor actor = trans.GetComponent<Actor>();
            actor.SetCurrentCell(cell);
            int turn=Random.Range(0, 4);
            actor.SetTurn((Actor.Turn)turn);
        }
    }
    Cell Get_A_Free_Cell()
    {
        while (true)
        {
            int randomRow = Random.Range(0, cells.Count);
            int randomColumn = Random.Range(0, cells[0].Count);
            if (cells[randomRow][randomColumn].state == Cell.CellState.FREE)
            {
                return cells[randomRow][randomColumn];
            }
        }
    }

    public Cell Get_A_Cell(int row, int column)
    {
        if (row < cells.Count && row>=0 && column < cells[0].Count && column >=0)
        {
            return cells[row][column];
        }
        else
        {
            //Debug.Log("Invalid Index");
            return null;
        }
    }
    public int GetFreeCellCount()
    {
        int counter = 0;
        foreach(List<Cell> cellRow in cells)
        {
            foreach(Cell cell in  cellRow)
            {
                if (cell.state == Cell.CellState.FREE)
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}

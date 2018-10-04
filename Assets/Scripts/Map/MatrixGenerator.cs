using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour
{
    #region Singleton Pattern
    static MatrixGenerator _instance;
    public static MatrixGenerator Instance
    {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MatrixGenerator>();
            return _instance;
        }
    }
    #endregion
    // Use this for initialization
    public float cellSize;
    public string cellObjectKey;
    public Transform cellsParent;
    public List<List<Cell>> LoadMapForEditor()
    {
        List<List<Cell>> cells = new List<List<Cell>>();
        Camera cam = Camera.main;
        float worldSpace_ScreenHeight = 2f * cam.orthographicSize;
        float worldSpace_ScreenWidth = worldSpace_ScreenHeight * cam.aspect;
        Vector2 LowerLeftPos = new Vector2(-(worldSpace_ScreenWidth / 2.0f), -(worldSpace_ScreenHeight / 2.0f));
        Vector2Int cellCount = new Vector2Int(Mathf.CeilToInt(worldSpace_ScreenWidth / cellSize), Mathf.FloorToInt(worldSpace_ScreenHeight / cellSize)-1);

        for (int i = 0; i < cellCount.y; i++)
        {
            List<Cell> aRowCells = new List<Cell>();
            for (int j = 0; j < cellCount.x; j++)
            {
                Transform cellTrns = ObjectPoolManager.Instance.GetAnObject(cellObjectKey);
                cellTrns.parent = cellsParent;
                Vector2 relativePos = new Vector3(j * cellSize + cellSize / 2, i * cellSize + cellSize / 2);
                cellTrns.position = LowerLeftPos + relativePos;
                Cell cell = cellTrns.GetComponent<Cell>();
                cell.Setup(new Vector2Int(i, j), Cell.CellState.FREE);
                aRowCells.Add(cell);
            }
            cells.Add(aRowCells);
        }
        return cells;
    }
    public List<List<Cell>> LoadMapFromString(string map)
    {
        string[] csvLines = map.Split('\n');
        List<List<Cell>> cells = new List<List<Cell>>();
        Camera cam = Camera.main;
        float worldSpace_ScreenHeight = 2f * cam.orthographicSize;
        float worldSpace_ScreenWidth = worldSpace_ScreenHeight * cam.aspect;
        Vector2 LowerLeftPos = new Vector2(-(worldSpace_ScreenWidth / 2.0f), -(worldSpace_ScreenHeight / 2.0f));
        Vector2Int cellCount = new Vector2Int(Mathf.CeilToInt(worldSpace_ScreenWidth / cellSize), Mathf.FloorToInt(worldSpace_ScreenHeight / cellSize)-1);

        for (int i = 0; i < cellCount.y; i++)
        {
            List<Cell> aRowCells = new List<Cell>();
            string[] aline = csvLines[i + 1].Split(',');
            for (int j = 0; j < cellCount.x; j++)
            {
                Transform cellTrns = ObjectPoolManager.Instance.GetAnObject(cellObjectKey);
                cellTrns.parent = cellsParent;
                Vector2 relativePos = new Vector3(j * cellSize + cellSize / 2, i * cellSize + cellSize / 2);
                cellTrns.position = LowerLeftPos + relativePos;
                Cell cell = cellTrns.GetComponent<Cell>();                
                Cell.CellState state= (Cell.CellState)int.Parse(aline[j]);
                cell.Setup(new Vector2Int(i, j), state);
                aRowCells.Add(cell);
            }
            cells.Add(aRowCells);
        }
        return cells;
    }
}

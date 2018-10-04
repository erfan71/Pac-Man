using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;

public class GridEditor : MonoBehaviour
{
    [Header("Grid Info")]
   
    private List<List<Cell>> cells;
    
    public int mapId;
    void Start()
    {
        InitializeCellMatrix();

        Debug.Log("Drag or click with left mouse button to draw a wall");
        Debug.Log("Press the SPACE button to save the map into a file");

    }

    void Update()
    {
        HandleInput();

    }
    void InitializeCellMatrix()
    {
        cells = MatrixGenerator.Instance.LoadMapForEditor();
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveCellsToFile();
        }
    }
     void SaveCellsToFile()
    {
        StringBuilder exportedCells = new StringBuilder();
        for (int i = 0; i < cells.Count; i++)
        {
            exportedCells.Append("\n");
            for (int j = 0; j < cells[i].Count; j++)
            {
                exportedCells.Append((int)cells[i][j].state);
                if (j< cells[i].Count - 1)
                {
                    exportedCells.Append(",");
                }
            }
        }
        string path = Application.dataPath + "/Resources/" + "Maps/Map_" + mapId;
        string fileName = path + ".csv";
        string csv = cells.Count.ToString() + "," + cells[0].Count;
        csv += exportedCells.ToString();
       
        System.IO.File.WriteAllText(fileName, csv);

        AssetDatabase.Refresh();

    }
}

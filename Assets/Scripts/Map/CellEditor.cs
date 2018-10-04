using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEditor : Cell {

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Convert_to_Wall();
        }
    }
}

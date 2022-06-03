using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridsHandler : MonoBehaviour
{
    public GameObject gridTemplate;
    public Transform parentTrans;
    public int rows;
    
    public Vector2 GridSize => GetComponent<GridLayoutGroup>().cellSize;

    public void Start()
    {
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                GameObject.Instantiate(gridTemplate, parentTrans);
            }
        }
    }
}

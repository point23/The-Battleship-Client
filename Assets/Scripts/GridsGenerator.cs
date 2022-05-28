using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridsGenerator : MonoBehaviour
{
    public GameObject gridTemplate;

    public Transform parentTrans;

    public int rows;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

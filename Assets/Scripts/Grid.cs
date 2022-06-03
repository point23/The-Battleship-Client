using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public GridsHandler handler => FindObjectOfType<GridsHandler>();
}

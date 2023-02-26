using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public Game gamefile;
    public GameObject path;
    private Vector2 _pathV;
    private int _widthCount;

    private void Start()
    {
        gamefile = gameObject.GetComponent<Game>();
        _pathV = new Vector2(0, 0);
        foreach (var node in gamefile.grid.AllCells)
        {
            if (node.walkable)
            {
                Instantiate(path, _pathV, Quaternion.identity);
            }

            if (_widthCount < gamefile.grid.width-1)
            {
                _pathV.x++;
                _widthCount++;
            }
            else
            {
                _pathV.y++;
                _pathV.x = 0;
                _widthCount = 0;
            }

        }
    }
    
}

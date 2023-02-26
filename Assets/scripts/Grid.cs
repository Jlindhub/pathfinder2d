using System;
using UnityEngine;

[CreateAssetMenu]
public class Grid: ScriptableObject
{
    public Cell[] AllCells; // 100
    public int width;  // 10
    public int Height => AllCells.Length / width;

    public Cell GetCell(int x, int y) => AllCells[x + y * width];
    public Cell GetCell(Vector2Int pos) => GetCell(pos.x, pos.y);
    
    public bool GetWalkable(int x, int y) => GetCell(x, y).walkable;
    public bool GetWalkable(Vector2Int pos) => GetWalkable(pos.x, pos.y);

    private void OnEnable() { foreach (var cell in this.AllCells) { cell.visited = false; } }
}
[Serializable]
public class Cell
{
    public bool walkable;
    public int cost;
    [HideInInspector] public bool visited;
}

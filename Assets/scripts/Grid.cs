using UnityEngine;

[CreateAssetMenu]
public class Grid: ScriptableObject
{
    public bool[] walkableCells; // 100
    public int width;  // 10
    public int Height => walkableCells.Length / width;

    public bool GetCell(int x, int y) => walkableCells[x + y * width];
    public bool GetCell(Vector2Int pos) => GetCell(pos.x, pos.y);



    public Cell[] cells;
}

public class Cell
{
    public bool Walkable;
    public Cell Previous;
    public int Cost;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct State
{
    public Vector2Int playerPosition;
    [SerializeField] private Grid _mGrid;
    public void SetGrid(Grid grid) => _mGrid = grid;
    bool PositionExists(Vector2Int newPosition) {
        return newPosition.x > -1 && newPosition.x < _mGrid.width &&
               newPosition.y > -1 && newPosition.y < _mGrid.Height;
    }
    bool PositionIsWalkable(Vector2Int newPosition)
    { return PositionExists(newPosition) && _mGrid.GetWalkable(newPosition); }
    public IEnumerable<State> GetSuccessors()
    {
        Vector2Int newPosition = playerPosition + Vector2Int.left;
        if (PositionIsWalkable(newPosition)) { yield return new State { playerPosition = newPosition, _mGrid = _mGrid }; }
        newPosition = playerPosition + Vector2Int.right;
        if (PositionIsWalkable(newPosition)) { yield return new State { playerPosition = newPosition, _mGrid = _mGrid }; }
        newPosition = playerPosition + Vector2Int.down;
        if (PositionIsWalkable(newPosition)) { yield return new State { playerPosition = newPosition, _mGrid = _mGrid }; }
        newPosition = playerPosition + Vector2Int.up;
        if (PositionIsWalkable(newPosition)) { yield return new State { playerPosition = newPosition, _mGrid = _mGrid };
        }
       
    }
}

public struct Statetwo
{
    public Vector2Int playerPosition;
    [SerializeField] private Grid _mGrid;
    public void Setgrid(Grid grid) => _mGrid = grid;
    bool PositionExists(Vector2Int newPosition) {
        return newPosition.x > -1 && newPosition.x < _mGrid.width &&
               newPosition.y > -1 && newPosition.y < _mGrid.Height;
    }
    bool PositionIsWalkable(Vector2Int newPosition)
    { return PositionExists(newPosition) && _mGrid.GetWalkable(newPosition); }
    public IEnumerable<Connector> GetAdjacent()
    {
        var cell = _mGrid.GetCell(playerPosition + Vector2Int.left);
        if (cell.walkable) { yield return new Connector { Next = new Statetwo(), Costs = cell.cost }; }
        cell = _mGrid.GetCell(playerPosition + Vector2Int.right);
        if (cell.walkable) { yield return new Connector { Next = new Statetwo(), Costs = cell.cost }; }
        cell = _mGrid.GetCell(playerPosition + Vector2Int.down);
        if (cell.walkable) { yield return new Connector { Next = new Statetwo(), Costs = cell.cost }; }
        cell = _mGrid.GetCell(playerPosition + Vector2Int.up);
        if (cell.walkable) { yield return new Connector { Next = new Statetwo(), Costs = cell.cost }; }
    }
}
public struct Connector
{
    public Statetwo Next;
    public int Costs;
}

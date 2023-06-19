using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Grid grid;
    public State state;
    public State goal;
    public Statetwo aStarState;
    public Statetwo aStarGoal;

    private void Start()
    {
        aStarState.playerPosition = state.playerPosition;
        aStarGoal.playerPosition = goal.playerPosition;
        aStarState.Setgrid(grid);
    }

    [ContextMenu("Depth-Path first")]
    public void DepthFirst() { 
        foreach (var state in Pathfinder.DepthFirstPath(state, goal)) { Debug.Log(state.playerPosition); } }

    [ContextMenu("Breadth-Path first")]
    public void BreadthFirst() {
        foreach (var state in Pathfinder.BreadthFirstPath(state, goal)) { Debug.Log(state.playerPosition); } }
    [ContextMenu("Breadth-Dictionary first")]
    public void BreadthFirstDict() {
        foreach (var state in Pathfinder.BreadthFirstDict(state, goal)) { Debug.Log(state.playerPosition); } }

    [ContextMenu("Astar")]
    public void Astar() {
        foreach (var state in Pathfinder.Astar(aStarState, aStarGoal)) { Debug.Log(state.playerPosition); } }
    
}
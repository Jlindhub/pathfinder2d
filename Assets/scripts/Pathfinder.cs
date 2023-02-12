using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Pathfinder
{
    public static IEnumerable<State> DepthFirstPath(State start, State end)
    {
        Stack<State> path = new Stack<State>();
        Stack<State> visited = new Stack<State>();
        path.Push(start);

        while (path.Count > 0)
        {
            bool foundNextNode = false;
            foreach (var neighbour in path.Peek().GetSuccessors())
            {
                if (path.Contains(neighbour)) continue;
                if (visited.Contains(neighbour)) continue;
                path.Push(neighbour);
                visited.Push(neighbour);
                if (neighbour.Equals(end)) { return path.Reverse(); }
                foundNextNode = true;
                break;
            }
            if (!foundNextNode) path.Pop();
        }
        return null;
    }
    
    public static IEnumerable<State> BreadthFirstPath(State start, State end)
    {
        Queue<State> visited = new Queue<State>();
        Queue<State[]> todoPaths = new Queue<State[]>();
        visited.Enqueue(start);
        todoPaths.Enqueue(new[]{start});
        
        while (todoPaths.Count>0)
        {
            var path = todoPaths.Dequeue();
            var currentNode = path[^1];
            foreach (var neighbor in currentNode.GetSuccessors())
            {
                if (neighbor.Equals(end)) { return path.Concat(new[] { neighbor }.ToArray()); }
                if (visited.Contains(neighbor)) continue;
                visited.Enqueue(neighbor);
                State[] newpath = path.Concat(new[] { neighbor }).ToArray();
                todoPaths.Enqueue(newpath);
            }
        }
        return null;
    }
    
    public static IEnumerable<State> BreadthFirstDict(State start, State end)
    {
        Dictionary<State, State> predecessors = new Dictionary<State, State>();
        Queue<State> todo = new Queue<State>();
        todo.Enqueue(start);
        
        while (todo.Count > 0) {
            var current = todo.Dequeue();
            foreach (var neighbor in current.GetSuccessors())
            {
                if (neighbor.Equals(end)) { return BuildPath(predecessors, neighbor); }
                if(predecessors.ContainsKey(neighbor)){continue;}
                predecessors[neighbor] = current; //saves that the neighbor's predecessor is this node.
                todo.Enqueue(neighbor);
            }
        }

        static List<State> BuildPath(Dictionary<State, State> predecessors, State neighbor ) 
        {
            List<State> path = new List<State>();
            
            while (!neighbor.Equals(null)) 
            {
                path.Add(neighbor);
                neighbor = predecessors[neighbor]; //key not found??
            }
            path.Reverse();
            return path;
        }
        return null;
    }
}
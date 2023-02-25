using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

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
                if (neighbor.Equals(end))
                {
                    predecessors[neighbor] = current;
                    return BuildPath(predecessors, neighbor, start);
                }
                if(predecessors.ContainsKey(neighbor)){continue;}
                predecessors[neighbor] = current; //saves that the neighbor's predecessor is this node.
                todo.Enqueue(neighbor);
            }
        }

        static List<State> BuildPath(Dictionary<State, State> predecessors, State neighbor, State start ) 
        {
            List<State> path = new List<State>();
            
            while (!neighbor.Equals(start))  //infinite loop... neighbor not nullable
            {
                path.Add(neighbor);
                neighbor = predecessors[neighbor];
            }
            path.Reverse();
            return path;
        }
        return null;
    }

    public static IEnumerable<State> Djikstra(State start, State end)
    {
        
    }




    public static IEnumerable<State> Astar(State start, State end)
    {
        var todo_nodes = new PriorityQueue<State, int>();
        todo_nodes.Enqueue(start,0);
        var predecessors = new Dictionary<State, State>();
        var costs = new Dictionary<State, int>();
        costs[start] = 0;
        while (todo_nodes.Count > 0)
        {
            var current_node = todo_nodes.Dequeue();
            if (current_node.Equals(end)) { return BuildPath; }
            foreach (var connection in current_node.GetSuccessors())
            {
                var neighbor = connection.next; //???? watch 'refactoring' loom
                var new_costs = costs[current_node] + connection.costs; //???? see above
                if( costs.ContainsKey(neighbor) && costs[neighbor] <= new_costs){ continue; }
                else
                {
                    if (todo_nodes.contains(neighbor)) // see above
                    {
                        todo_nodes.Dequeue(neighbor);
                    }
                }

            }
        }
    }




}
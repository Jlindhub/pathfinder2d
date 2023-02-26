using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
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
    public static IEnumerable<Statetwo> Astar(Statetwo start, Statetwo end)
    {
        var todo_nodes = new PriorityQueue<Statetwo, int>();
        todo_nodes.Enqueue(start,0);
        var predecessors = new Dictionary<Statetwo, Statetwo>();
        var costs = new Dictionary<Statetwo, int>();
        costs[start] = 0;
        while (todo_nodes.Count > 0)
        {
            var current = todo_nodes.Dequeue();
            if (current.Equals(end)) { return BuildPath(predecessors, current,start); }
            foreach (var connection in current.GetAdjacent())
            {
                var neighbor = connection.Next; //???? watch 'refactoring' loom - watched, still makes no sense
                var newCosts = costs[current] + connection.Costs; //???? see above
                if( costs.ContainsKey(neighbor) && costs[neighbor] <= newCosts){ continue; }

                if (newCosts > costs[current]) // see above
                {
                    continue;
                }

                predecessors[neighbor] = current;
                costs[neighbor] = newCosts;
                var heuristic = HeurEstimate(neighbor, end);
                todo_nodes.Enqueue(neighbor,newCosts+heuristic);
            }
            predecessors.Add(current);//????
        }

        static int HeurEstimate(Statetwo node, Statetwo goal)
        {
            int dx = Mathf.Abs(node.playerPosition.x - goal.playerPosition.x);
            int dy = Mathf.Abs(node.playerPosition.y - goal.playerPosition.y);
            return dx + dy;
        }
        
        static List<Statetwo> BuildPath(Dictionary<Statetwo, Statetwo> predecessors, Statetwo neighbor, Statetwo start ) 
        {
            List<Statetwo> path = new List<Statetwo>();
            while (!neighbor.Equals(start)) { path.Add(neighbor); neighbor = predecessors[neighbor]; }
            path.Reverse();
            return path;
        }
        return null;
    }
    
    /*public static List<State> Djikstra(State start, State end)
    {
        var predecessors = new Dictionary<State, State>();
        var costs = new Dictionary<State, int>();
        var todoNodes = new PriorityQueue<State, int>();

        todoNodes.Enqueue(start, 0);
        costs[start] = 0;

        while (todoNodes.Count > 0)
        {
            var dequeuedNode = todoNodes.Dequeue();
            var queueCosts = dequeuedNode.Priority;
            var currentNode = dequeuedNode.Value;
            
            if (currentNode == end) { return BuildPath(predecessors, currentNode, start); }
            if (queueCosts > costs[currentNode]) { continue; }
            foreach (var neighbor in currentNode.GetSuccessors())
            {
                int newCosts = costs[currentNode] + neighbor.Cost;
                if (costs.ContainsKey(neighbor) && costs[neighbor] <= newCosts) { continue; }

                if (todoNodes.Contains(neighbor)) // remove neighbor from the priority queue
                {
                    todoNodes.Remove( neighbor);
                }
                predecessors[neighbor] = currentNode;
                costs[neighbor] = newCosts;
                todoNodes.Enqueue(neighbor, newCosts);
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
    }*/





}
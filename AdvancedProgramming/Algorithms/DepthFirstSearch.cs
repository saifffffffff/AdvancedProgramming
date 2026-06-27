using AdvancedProgramming.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Algorithms;

class DepthFirstSearch<T>
{


    public static void DFS( IGraph<T> graph )
    {

        if (graph.Vertices.Count == 0) return;

        var visited = new HashSet<Vertex<T>>();
        var root = graph.Vertices.First();

        Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
        stack.Push(root);
        visited.Add(root);

        while ( stack.Count > 0)
        {
            var current = stack.Pop();

            if (visited.Contains(current)) continue;

            visited.Add(current);

            foreach ( var child in  graph.GetChildren(current) ) 
            {
                if ( !visited.Contains(child) ) 
                    stack.Push(child);
            }
        }

    }


    HashSet<Vertex<T>>? visited = null ;

    public void RecursiveDFS (  IGraph<T> graph )
    {

        visited = new ();
        
        foreach ( var vertex in  graph.Vertices)
        {
            
            if ( !visited.Contains(vertex) )
                DFS_Visit(graph, vertex);
        }

    }

    public void DFS_Visit( IGraph<T> graph  , Vertex<T> vertex )
    {
        visited.Add(vertex);
        
        foreach ( var child in graph.GetChildren(vertex))
        {
            if (!visited.Contains(child))
                DFS_Visit(graph, child);
        }


    }

}

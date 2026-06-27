using AdvancedProgramming.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Algorithms;

internal class BreadthFirstSearch<T>
{

    public static void BFS(GraphAdjList<T> graph )
    {
        Dictionary<Vertex<T> , bool  > isVisited = new(); 
        
        foreach ( var vertex in graph.Vertices) // V steps
            isVisited.Add(vertex, false); // V 


        var root = graph.Vertices[0]; // 1 
        isVisited[root] = true; // 1 

        var queue = new Queue<Vertex<T>>(); // 1 
        queue.Enqueue(root); // 1 

        int i = 0; 
        while ( queue.Count > 0) // V 
        {

            var current = queue.Dequeue(); // 1 * V  
            Console.Write( current + " ");
            foreach ( var child in graph.GetChildren(current)) // children 
            {
                i++;
                if ( !isVisited[child]) 
                {
                    queue.Enqueue(child);
                    isVisited[child] = true;
                }
            }

        }
        Console.WriteLine();
        Console.WriteLine(i);


    }
}

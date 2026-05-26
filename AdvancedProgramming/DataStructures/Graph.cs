using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DataStructures;

enum GraphDirectionType { Directed , unDirected }
internal class Vertex<T>
{
    public T data { get; set; }
    
    public Vertex(T data ) { this.data = data; }

    public override string ToString() => data?.ToString() ?? string.Empty;
    
}

internal class Graph<T>
{

    private int[,] _adjacencyMatrix;
    private GraphDirectionType _directionType;
    private List<Vertex<T>> _vertices { get; }

    public Graph(GraphDirectionType directionType , params Vertex<T>[] vertices ) : this(vertices.ToList() , directionType) { }

    public Graph(List<Vertex<T>> vertices , GraphDirectionType directionType)
    {
        _vertices = vertices;
        _adjacencyMatrix = new int[vertices.Count, vertices.Count];
        _directionType = directionType;
    }

    public void AddEdge(Vertex<T> vertex1, Vertex<T> vertex2, int weight = 1)
    {

        if (_vertices.Count <= 1)
            return;

        if (!IsVertexIncludedInGraph(vertex1, out int index1) || !IsVertexIncludedInGraph(vertex2, out int index2))
            return;

        _adjacencyMatrix[index1, index2] = weight;

        if ( _directionType == GraphDirectionType.unDirected)
            _adjacencyMatrix[index2, index1] = weight;

    }

    public bool IsVertexIncludedInGraph(Vertex<T> vertex) => IsVertexIncludedInGraph(vertex, out int temp);
    
    private bool IsVertexIncludedInGraph(Vertex<T> vertex, out int index) => (index = _vertices.IndexOf(vertex)) != -1 ? true : false;


    public bool EdgeExists(Vertex<T> vertex1, Vertex<T> vertex2)
    {
        if (!IsVertexIncludedInGraph(vertex1, out int index1) || !IsVertexIncludedInGraph(vertex2 , out int index2))
            return false;
        
        return _adjacencyMatrix[index1, index2] > 0;

        
    }
    public int GetOutDegreeOfVertex(Vertex<T> vertex)
    {
        if (!IsVertexIncludedInGraph(vertex, out int index))
            return index;

        int outDegree = 0;

        for (int i = 0; i < _vertices.Count; i++)
            outDegree += _adjacencyMatrix[index, i];

        return outDegree; 

    }

    public int GetInDegreeOfVertex(Vertex<T> vertex)
    {
        
        if (!IsVertexIncludedInGraph(vertex, out int index))
            return index;

        int inDegree = 0;

        for (int i = 0; i < _vertices.Count; i++)
            inDegree += _adjacencyMatrix[i, index];

        return inDegree;
    }

    public void DisplayMatrix()
    {
        for (int row = 0; row < _vertices.Count; row++)
        {
            for (int col = 0; col < _vertices.Count; col++)
            {
                Console.Write(_adjacencyMatrix[row, col] + " ");
            }
            Console.WriteLine();
        }
    }
}


internal class GraphAdjList<T>
{

    
    Dictionary<Vertex<T>, List<KeyValuePair< Vertex<T> , int > >> _adjacencyList = new ();


    public GraphAdjList( params Vertex<T> [] vertices) :this(vertices.ToList()) { }
    
    public GraphAdjList( List<Vertex<T>> vertices )
    {
        foreach (var vertex in vertices )
            _adjacencyList.Add(vertex, new ());
    }


    public bool IsVertexIncludedInGraph(Vertex<T> vertex) => _adjacencyList.ContainsKey(vertex);
    
    public int GetOutDegreeOfVertex(Vertex<T> vertex)
    {
        if (!IsVertexIncludedInGraph(vertex))
            return -1 ;

        return _adjacencyList[vertex].Count;

    }

    public int GetInDegreeOfVertex(Vertex<T> vertex)
    {
        if (!IsVertexIncludedInGraph(vertex)) return -1;

        int InDegree = 0;
        
        foreach ( var chainNodes in _adjacencyList.Values)
            foreach ( var node in chainNodes)
                if ( node.Key == vertex ) InDegree++;

        return InDegree;
    }

    public bool EdgeExists(Vertex<T> vertex1 , Vertex<T> vertex2  )
    {
        if ( IsVertexIncludedInGraph(vertex1 ) && IsVertexIncludedInGraph(vertex2))
        {
            var chainNodes = _adjacencyList[vertex1];

            foreach ( var node in chainNodes )
                if ( node.Key == vertex2 ) return true;

        }
        return false;


    }
    public void AddEdge ( Vertex<T> vertex1 , Vertex<T> vertex2 , int weight =1 ) 
    {
        if (!IsVertexIncludedInGraph(vertex1) || !IsVertexIncludedInGraph(vertex2)) return;

        _adjacencyList[vertex1].Add(new(vertex2 , weight));

    }

    public void Display()
    {
        foreach ( var pair in _adjacencyList )
            Console.WriteLine(pair.Key + " : " + string.Join( "->", pair.Value));
    }




}

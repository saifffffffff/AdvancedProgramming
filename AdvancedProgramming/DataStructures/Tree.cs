using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvancedProgramming.DataStructures;


class Node <T>
{
    public T? Data { get; }
    private List<Node<T>> _children = new List<Node<T>>();

    public Node(T data ) { this.Data = data; }

    public IReadOnlyList<Node<T>> Children => _children.AsReadOnly();

    public void AddChild(T data) => _children.Add(new Node<T>(data));
    public void AddChildren(params T[] values)
    {
        foreach ( var value in values)
            AddChild(value);
    }
    public bool RemoveChild(T data) => _children.Remove(new Node<T>(data));
    public void RemoveChildren(params T[] data) => _children.RemoveAll( (node) => node?.Data?.Equals(data) ?? false);
    
    public void RemoveAllDescendants ()
    {
        _children.Clear();
    }

    public override bool Equals(object? obj)
    {
        
        Node<T>?other = obj as Node<T>;
        
        if (other == null) return false;
        
        return Data!.Equals(other.Data);

    }
    public override string ToString() => Data?.ToString() ?? string.Empty;
    
}

internal class Tree<T>
{

    public Node <T> Root;

    public Tree (Node<T> Root)
    {
        this.Root = Root;
    }


    private void Print(Node <T> Root , string identation= "")
    {

        Console.WriteLine(identation + Root.Data);

        foreach ( var node in Root.Children)
        {
            Print(node , identation + "  ");
        }


    }
    public void Print() => Print(this.Root);

    public bool Find(T value ) => Find(new Node<T>(value) , Root);
    
    private bool Find(Node<T> NodeToFind, Node<T> Root)
    {


        if (Root.Equals(NodeToFind))
            return true;

        foreach ( var node in Root.Children)
            if ( Find(NodeToFind , node) ) return true;

        return false;
        
    }

    public Node<T>? GetNode(T Value) => GetNode(new Node<T>(Value), Root);
    private Node<T>? GetNode(Node<T> NodeToGet, Node<T> Root)
    {


        if (Root.Equals(NodeToGet))
            return Root;

         
        foreach (var node in Root.Children)
        {
            var result = GetNode(NodeToGet, node);
            
            if (result is not null)
                return result;
        }

        return null;

    }

    public void AddChildToRoot(T value) => Root.AddChild(value);
    public bool AddChildToNode(T value , T nodeValue)
    {
        var node = GetNode(nodeValue);
        
        if (node is null)
            return false;

        node.AddChild(value);
        return true;

    }
    public bool  RemoveSubTree ( T rootValue)
    {
        var node = GetNode(rootValue);
        if (node is null)
            return false;

        node.RemoveAllDescendants();
        return true;
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace AdvancedProgramming.DataStructures;



class BinaryTree<T>
{

    private class Node<T>
    {
        public T Data { get; set; }
        public Node(T data) { Data = data; }

        public Node<T> Left;
        public Node<T> Right;

    }

    private Node<T>? Root;
    
    public enum TraversalMode
    {
        InOrder = 0,
        PreOrder = 1,
        PostOrder = 2,
    }

    public BinaryTree() { }

    public BinaryTree(T Data) 
    {
        Root = new Node<T>(Data);
    }




    
    
    // level order insertion
    public void Insert(T Data)
    {
        var node = new Node<T>(Data);
        if (Root is null)
        {
            this.Root = node;
            return;
        }

        Queue<Node<T>> queue = new();
        queue.Enqueue(Root);

        while ( queue.Count > 0 )
        {
            var current = queue.Dequeue();

            if (current.Left is null)
            {
                current.Left = node;
                break;
            }
            
            if ( current.Right is null)
            {
                current.Right = node;
                break;
            }
            
            queue.Enqueue(current.Left);
            queue.Enqueue(current.Right);
        }

    }

    // preorder application - cloning tree
    public BinaryTree <T> Copy() 
    {
        var tree = new BinaryTree<T>();
        Copy(tree, this.Root);
        return tree;
    }
    private void Copy(BinaryTree<T> copy, Node<T> Root)
    {
        if (Root is { })
        {
            copy.Insert(Root.Data);
            Copy(copy , Root.Left);
            Copy(copy , Root.Right);
        }
    }

    // post order application - clean
    public void Clean()
    {
        Clean(ref this.Root);
    }
    private void Clean( ref Node<T>  Root )
    {
        if ( Root is { })
        {
            Clean(ref Root.Left);
            Clean(ref Root.Right);
            Root = null;
        }
    }


    public void PrintTree_PreOrder_Recursive() => PrintTree_PreOrder_Recursive(this.Root);
    
    private void PrintTree_PreOrder_Recursive(Node<T> Root , int level = 0 )
    {
        if (  Root is { })
        {
            Console.WriteLine(new string ( ' ' , level * 3 ) + Root.Data);
            PrintTree_PreOrder_Recursive(Root.Left, level + 1 );
            PrintTree_PreOrder_Recursive(Root.Right, level + 1 );
        }
    }

    public void PrintTree_PreOrder_Stack() => PrintTree_PreOrder_Stack(this.Root);
    
    private void  PrintTree_PreOrder_Stack(Node<T> Root , int level = 0)
    {

        if ( Root is null) return;

        Stack<Node<T>> stack = new ();
        stack.Push(Root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            
            if (current is null) continue;
            
            Console.WriteLine($"{new string (' ' , level * 3 )}{current.Data}");
            stack.Push(current.Right);
            stack.Push(current.Left);


        }




    }


}

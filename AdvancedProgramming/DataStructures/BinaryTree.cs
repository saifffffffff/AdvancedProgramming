using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace AdvancedProgramming.DataStructures;



class BinaryTree<T>
{
    class Node<T>
    {
        public T Data{ get; set; }
        public Node(T data) { Data = data; }
    
        public Node<T> Left;
        public Node<T> Right;

    }

    Node<T> Root;
    public BinaryTree (T Data)
    {
        Root = new Node<T>(Data);
    }



    public void Insert(T Data )
    {
        
    }

    private bool Insert(T Data, Node<T> Root)
    {
        if ( Root.Data == null )
        {
            Root.Data = Data;
            return true;
        }

        if (Root.Right == null)
        {
            Root.Right = new Node<T>(Data);
            return true;
        }


        else if (Root.Left == null)
        {
            Root.Left = new Node<T>(Data);
            return true;
        }

        if ( Insert(Data , Root.Right))
        {
            return true;
        }

        if ( Insert(Data , Root.Left) )
        {
            return true;
        }

        return false;
    }

    public void PrintTree ()
    {
        PrintTree(Root);
    }

    private  void PrintTree (Node <T> Root )
    {

        Console.WriteLine(Root.Data);
        PrintTree(Root.Right);
        PrintTree(Root.Left);



    }



}

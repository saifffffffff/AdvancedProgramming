using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdvancedProgramming.DataStructures;

public enum HeapType
{
    MinHeap,
    MaxHeap
}

class Heap<T> where T : IComparable<T>
{

    T[] _arr;
    int _size;
    HeapType _type;
    Func<T, T, bool> _comparer;
    


    public Heap(int size , HeapType type)
    {    
        _arr = new T[size];
        
        _type = type;
        
        _comparer = (in1, in2 ) =>  type == HeapType.MinHeap ? in1.CompareTo(in2) <= 0 : in1.CompareTo(in2) >= 0;
            
        
    }

    public Heap(T[] array ,  HeapType type) : this( array.Length , type)
    {
        BuildHeap(array);
    }

    private void BuildHeap(T[] array )
    {
        foreach (var element in array )
        {
            Insert(element);
        }
    }
    
    public void Extract()
    {
        Swap(0, _size - 1);
        _size--;
        HeapifyDown(0);

    }

    public void Insert(T element )
    {

        
        if ( _size == _arr.Length)
        {
            throw new Exception("Heap is full");
        }

        _arr[_size] = element;
        _size++;

        
        HeapifyUp(_size - 1);
        
    }

    void Swap ( int index1 , int index2)
    {
        
        (_arr[index1], _arr[index2]) = (_arr[index2], _arr[index1]); // short hand swap
        
        
    }

    

    void HeapifyDown ( int index)
    {
        if (index >= _size )
            return;
        
        int leftChild = 2 * index + 1;
        int rightChild = 2 * index + 2;
        int selected  = index;

        
        
            
        if ( leftChild < _size && !_comparer(_arr[index] , _arr[leftChild]))
        {
            selected = leftChild;
        }
        
        if ( rightChild <_size && !_comparer(_arr[selected], _arr[rightChild]))
        {
            selected = rightChild;
        }

        if ( selected != index)
        {
            Swap(index, selected);
            HeapifyDown(selected);
        }

        

        

    }



    


    void HeapifyUp(int index)
    {

        int parent = (index - 1) / 2;

        // base case
        if (index == 0 || index > _size )
            return;

        
        if (_comparer(_arr[index], _arr[parent]))
        {
            Swap(parent, index);
            HeapifyUp(parent);
        }

        
    }


    public void Print()
    {
        for (int i = 0; i < _size; i++)
            Console.Write(_arr[i]);
        Console.WriteLine();
        
    }

    public T Peek()
    {
        if (_size == 0)
            throw new Exception("Empty heap");
        return _arr[0];
    }









}

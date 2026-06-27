using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DataStructures;

class MyPriorityQueue<TValue ,TPriority> where TPriority : IComparable<TPriority>
{


    Heap<TPriority> Heap = new Heap<TPriority> (100,HeapType.MinHeap);

    Dictionary<TPriority, TValue> dict = new();



    public void Add(TValue value , TPriority priority )
    {
        dict.Add(priority, value);
        Heap.Insert(priority);
    }

    public TValue Serve()
    {
        TValue value=  dict[Heap.Peek()];
        Heap.Extract();
        return value;
    }



    
}

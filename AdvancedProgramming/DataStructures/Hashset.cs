using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

namespace AdvancedProgramming.DataStructures;

class Hashset<T>
{
    LinkedList<T> [] _table;
    
    public int Count { get; private set; }

    private int Hash(T item)
    {
        if (item is null) 
            return 0;

        int hash = 0;
        
        string str = item.ToString();
        
        foreach (char c in str)
        {
            hash += hash * 31 + c;
        }

        return hash % _table.Length;

    }

    public Hashset(int size = 16)
    {
        _table  = new LinkedList<T> [size];
    }

    public Hashset ( IEnumerable<T> collection, int size = 16) : this()
    {
         
        foreach (var item in collection)
            this.Add(item);

    }
    public bool Contains(T item)
    {
        int index;
        if ( item is { } &&  _table[index = Hash(item) ] is { } )
            return _table[index].Contains(item);
            
        return false;
    }

    public void Add(T item)
    {
        if (this.Contains(item))
            return;

        int index = Hash(item);
        
        if (_table[index] is null )
            _table[index] = new LinkedList<T>();

        _table[index].AddLast(item);
        Count++;
    
    }

    public bool Remove(T item)
    {
        int index = Hash(item);
        
        if ( _table[index] is { })
        {
            if (_table[index].Remove(item))
            {
                Count--;
                return true;
            }
        }

        return false;
    }

    public void Display ()
    {
        foreach ( var bucket in _table)
        {
            if (bucket is null) continue;

            foreach ( var item in bucket )
                Console.WriteLine(item);
        
        }

    }

    public void UnionWith(Hashset<T> other)
    {
        
        foreach ( var bucket in other._table)
        {
            if (bucket is null) continue;
            
            foreach (var item in bucket)
                this.Add(item);
        }

    }


    public void IntersectWith(Hashset<T> other)
    {
        
        foreach ( var bucket in other._table)
        {
            if (bucket is null) continue;

            foreach ( var item in bucket)
            {
                if (!this.Contains(item))
                    this.Remove(item);
            }

        }
    }

    public void IntersectWith_(Hashset<T> other)
    {
        throw new NotImplementedException();
    }



}

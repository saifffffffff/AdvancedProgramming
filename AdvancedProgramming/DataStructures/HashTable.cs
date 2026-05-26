using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
namespace AdvancedProgramming.DataStructures;

class HashTable<TKey, TValue>
{
    int _size;
    
    List<KeyValuePair<TKey, TValue>> [] _table;
    
   

    public HashTable (int size = 5)
    {
        _size = size;
        _table = new List<KeyValuePair<TKey, TValue>>[size];
    }

    private int Hash( TKey key )
    {
        
        string keyString = key.ToString();
        int hash = 0;

        foreach( char c in keyString) 
            hash = hash * 31 + c;

        return Math.Abs(hash) % _size;
    }
    

    public void Add(TKey key , TValue value) 
    {
        
             
        int index = Hash(key);
        

        if (_table[index] is null )
            _table[index] = new ();

        for ( int i =0 ;i < _table[index].Count; i++)
        {
            if (_table[index][i].Key.Equals(key))
            {
                _table[index][i] = new KeyValuePair<TKey, TValue>(key, value);
                return;
            }
        }

        _table[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        
    }


    public TValue Get(TKey key) { 
        
        var chain = _table[Hash(key)];

        if (chain is null)
            return default(TValue);

        return chain.Find(pair => pair.Key.Equals(key) ).Value;

    }

    public void Remove(TKey key)
    {

        var chain = _table[Hash(key)];
        
        if(chain is null)
            return;

        for ( int i = 0; i < chain.Count; i++)
        {
            if (chain[i].Key.Equals(key))
            {
                chain.RemoveAt(i);
                break;
            }
        }

    }

    public void Display()
    {
        int i = 0;
        foreach ( var chain in _table)
        {
            if ( chain is null )
            {
                i++;
                continue;
            }
            Console.WriteLine($"{i}: {string.Join("->" , chain)}");
            i++;
        }
        
    }

   






}

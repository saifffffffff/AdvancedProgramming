using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DataStructures;

internal class Bitarray
{
    int[] numbers;
    

    public Bitarray(int bits_number)
    {
        numbers = new int[ (int) Math.Ceiling( bits_number / 32.0 ) ]; // divide by 32 because each int has 32 bits ( if 80 bits are needed, we need 3 integers (80/ 32))
    }

    public bool ValidateIndex ( int index ) => index >= 0 && index / 32 < numbers.Length;
    
    public bool Get(int index)
    {
        if (!ValidateIndex(index))
            throw new IndexOutOfRangeException("Index is out of range of the bitarray");
        

        int numberIndex = index / 32;
        int mask = 1 << ( index % 32 ); // mod 32 to make sure that if the integer is not in the first index (0) , we will shift the mask to the right position ( if index is 33, we need to shift the mask to the right by 1 position to get the second bit of the integer)
        return (numbers[numberIndex] & mask) != 0;
    }

    public void Set(int index , bool value )
    {
        if ( !ValidateIndex(index))
            throw new IndexOutOfRangeException("Index is out of range of the bitarray");

        int numberIndex = index / 32;
        int number = numbers[numberIndex];
        
        int mask = 1 << index;

        if (value)
            numbers[numberIndex] = number | mask;

        else
            numbers[numberIndex] = number & ~mask;

                
    }
    public override string ToString()
    {
        StringBuilder strBuilder = new StringBuilder();

        for (int i = numbers.Length - 1; i >= 0; i--)
            strBuilder.Append(Convert.ToString(numbers[i], 2).PadLeft(32, '0'));

        
        
        return strBuilder.ToString();
    }
   
}

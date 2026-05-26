using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Lessons;

public static class Algorithms
{
    public static void InsertionSort(int[] numbers)
    {

        for (int right = 1; right < numbers.Length; right++)
        {
            int current = right;
            int left = current - 1;

            while (left >= 0 && numbers[current] < numbers[left])
            {

                int temp = numbers[left];

                numbers[left] = numbers[current];

                numbers[current] = temp;

                current -= 1;

                left -= 1;

            }



        }
    }

    public static void InsertionSort_Uni(int[] numbers)
    {

        for (int right = 1; right < numbers.Length; right++)
        {
            int current = numbers[right];
            int left = current - 1;

            while (left >= 0 && current < numbers[left])
            {
                numbers[left + 1] = numbers[left];
                left -= 1;
            }

            numbers[left + 1] = current;



        }
    }
}

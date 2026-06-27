using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.Algorithms;

class QuickSortAlgo
{

    public static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    static int Partition(int[] A, int p, int r)
        {
            int x = A[r];      // pivot
            int i = p - 1;

            for (int j = p; j < r; j++)
            {
                if (A[j] <= x)
                {
                    i++;
                    Swap(A, i, j);
                }
            }

            Swap(A, i + 1, r);
            return i + 1;
        }

    // Quick Sort
    static void QuickSort(int[] A, int p, int r)
    {
        if (p < r)
        {
            int q = Partition(A, p, r);

            QuickSort(A, p, q - 1);   // left side
            QuickSort(A, q + 1, r);   // right side
        }
    }


}
// Partition (Lomuto scheme)


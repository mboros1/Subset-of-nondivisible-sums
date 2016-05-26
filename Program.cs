using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    // Program that takes a set of integers, and counts the largest set where the sum of any 2 integers is not divisible by a given integer k.
    // My strategy to solve this problem uses modular arithmetic to reduce the problem from a set of all integers, Z, to a modular space of
    // Z_k, the ring integer ring with k elements {0,1,2,...,k-1}. First, I take the array of integers a[], and reduce each element modulo k.
    // I created a new array of length k, so that each index k[i] is equivalent to an element of the ring Z_k. The program then counts the number
    // of elements congruent to a mod k. Once this loop is complete, logical tests are done on the set a Mod k to maximize the number of elements
    // that can be in the set while still satisfying the condition a_i + a_j Mod k != 0.

    static void Main(String[] args)
    {
        // Takes the key board input
        string[] tokens_n = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int k = Convert.ToInt32(tokens_n[1]);
        int[] a = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);

        // Counter for the number of points a_i in the final set.
        int count = 0;

        // Creating our now set, a Mod k
        int[] aModk = new int[k];

        // this loops through every element of a, and converts it to a Mod k. We then count how many elements there are for each element 0 <= i < k.
        for (int i = 0; i < n; i++)
        {
            int temp_a = a[i] % k;
            aModk[temp_a]++;
        }

        //For the logic tests, there are 2 cases. When k is even and when k is odd.
        if (k % 2 == 0)
        {
            //If a_i Mod k = 0, that means that if we add a_i to any other integer a_j Mod k = 0; the result will be divisible by k.
            // Therefore, there can be at most 1 element that satisifies the condition a_i Mod k = 0.
            if (aModk[0] > 0)
            {
                count++;
            }
            // Similarly, if a_i Mod k = k / 2, then adding this to another element congruent to k / 2 will give us k, meaning it is
            // divisible by k. So again, at most 1 element of this type.
            if (aModk[k / 2] > 0)
            {
                count++;
            }
            // Straightforward logic here; i + (k - i ) = k, k Mod k = 0. So I just select the largest element of the two.

            for (int i = 1; i < k / 2; i++)
            {
                count += Math.Max(aModk[i], aModk[k - i]);

                /*    I commented out the longer logic based way in favor of a simple C# function, but both work the same way.
                 *    
                if (aModk[i] < aModk[k - i])
                {
                    count += aModk[k - i];
                }
                else
                {
                    count += aModk[i];
                }
                */
            }
        }
        // Same logic in the odd case as the even, just taking out the case where a_i Mod k = k / 2, since that case doesn't exist for odd numbers,
        // and the for loop is slightly retooled, its maximum being (k+1)/2 instead of k/2 to remove any potential rounding errors.
        // I left in the logic cases in the for loop instead of using the Math.Max function to show both work indentically.
        else
        {
            if (aModk[0] > 0)
            {
                count++;
            }
            for (int i = 1; i < (k + 1) / 2; i++)
            {
                if (aModk[i] < aModk[k - i])
                {
                    count += aModk[k - i];
                }
                else
                {
                    count += aModk[i];
                }

            }
        }

        Console.WriteLine(count);
    }
}

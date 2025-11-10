using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonSDEInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            // ----------------------
            // 1. Two Sum
            // ----------------------
            Console.WriteLine("1. Two Sum (Amazon SDE style):");
            int[] nums1 = { 2, 7, 10, 11, 15 };
            int target1 = 12;
            var twoSumResult = TwoSum(nums1, target1);
            Console.WriteLine($"Indices: [{twoSumResult[0]}, {twoSumResult[1]}]");

            // ----------------------
            // 2. Longest Substring Without Repeating Characters
            // ----------------------
            Console.WriteLine("\n2. Longest Substring Without Repeating Characters:");
            string s = "abcabcbb";
            Console.WriteLine($"Length: {LengthOfLongestSubstring(s)}");



            // ----------------------
            // 4. Check Anagrams
            // ----------------------
            Console.WriteLine("\n4. Check Anagrams:");
            string s1 = "listen", s2 = "silent";
            Console.WriteLine($"Are Anagrams: {AreAnagrams(s1, s2)}");

            // ----------------------
            // 5. Triplet Sum Equals Target
            // ----------------------
            Console.WriteLine("\n5. Triplet Sum Equals Target:");
            int[] arrTriplet = { 1, 4, 45, 6, 10, 8 };
            int targetTriplet = 22;
            Console.WriteLine($"Has Triplet Sum: {HasTripletSum(arrTriplet, targetTriplet)}");

            // ----------------------
            // 6. Find Duplicates
            // ----------------------
            Console.WriteLine("\n6. Find Duplicates:");
            int[] arrDuplicates = { 1, 6, 5, 2, 3, 3, 2 };
            List<int> duplicates = FindDuplicates(arrDuplicates);
            Console.WriteLine(string.Join(" ", duplicates));

            // ----------------------
            // 7. Reverse a String
            // ----------------------
            Console.WriteLine("\n7. Reverse a String:");
            string original = "HelloWorld";
            Console.WriteLine($"Reversed: {ReverseString(original)}");
        }

        // ----------------------
        // 1. Two Sum
        // ----------------------
        public static int[] TwoSum(int[] nums, int target)
        {
            // Dictionary will store each number and its index
            // Key = number, Value = index
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                //int[] nums1 = { 2, 7, 11, 15 };
                // Calculate the number we need to reach the target
                int complement = target - nums[i];

                // If complement already seen, we found the solution
                if (map.ContainsKey(complement))
                    return new int[] { map[complement], i };

                // Store the current number and its index if not already stored
                if (!map.ContainsKey(nums[i]))
                    map[nums[i]] = i;
            }
            return Array.Empty<int>(); // no solution
        }

        // 2. Longest Substring Without Repeating Characters
        // ----------------------
        public static int LengthOfLongestSubstring(string s)
        {
            // HashSet to track unique characters in the current window
            HashSet<char> set = new HashSet<char>();
            int left = 0, maxLen = 0;

            // Expand the window by moving 'right'
            for (int right = 0; right < s.Length; right++)
            {
                // If duplicate is found, shrink from the left until it's removed
                while (set.Contains(s[right]))
                {
                    set.Remove(s[left]);
                    left++;
                }
                // Add the new unique character
                set.Add(s[right]);

                // Update maximum window length
                maxLen = Math.Max(maxLen, right - left + 1);
            }
            return maxLen;
        }


        public static int LengthOfLongestSubstring(string s)
        {
            HashSet<char> chars;

        }






        // ----------------------
        //3. Check Anagrams
        // ----------------------
        public static bool AreAnagrams(string s1, string s2)
        {
            // If lengths are not equal, they can't be anagrams
            if (s1.Length != s2.Length) return false;

            // Use an array as frequency counter for all ASCII chars
            int[] freq = new int[256];

            // Increase for s1, decrease for s2
            for (int i = 0; i < s1.Length; i++)
            {
                freq[s1[i]]++;
                freq[s2[i]]--;
            }

            // If all frequencies are 0, it's an anagram
            foreach (int count in freq)
                if (count != 0) return false;

            return true;
        }

        // ----------------------
        // 4. Triplet Sum Equals Target
        // ----------------------
        public static bool HasTripletSum(int[] arr, int target)
        {
            // Step 1: Sort array
            Array.Sort(arr);

            // Step 2: Fix one element, then apply 2-pointer search
            for (int i = 0; i < arr.Length - 2; i++)
            {
                int left = i + 1, right = arr.Length - 1;
                while (left < right)
                {
                    int sum = arr[i] + arr[left] + arr[right];
                    if (sum == target) return true;   // found triplet
                    if (sum < target) left++;         // need bigger sum
                    else right--;                     // need smaller sum
                }
            }
            return false;
        }

        // ----------------------
        // 5. Find Duplicates
        // ----------------------
        public static List<int> FindDuplicates(int[] arr)
        {
            HashSet<int> seen = new HashSet<int>();       // track numbers we have seen
            HashSet<int> duplicates = new HashSet<int>(); // store duplicates

            foreach (int num in arr)
            {
                // If Add returns false, number is already seen
                if (!seen.Add(num))
                    duplicates.Add(num);
            }

            // Return duplicates or -1 if none exist
            return duplicates.Count > 0 ? new List<int>(duplicates) : new List<int> { -1 };
        }
        // ----------------------
        // 6. Reverse a String
        // ----------------------
        public static string ReverseString(string s)
        {
            // Convert to char array
            char[] arr = s.ToCharArray();
            // Reverse in place
            Array.Reverse(arr);
            // Convert back to string
            return new string(arr);
        }
    }
}
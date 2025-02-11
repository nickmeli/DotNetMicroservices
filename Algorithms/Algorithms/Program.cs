// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

//int[] NextPermutation(int[] nums)
//{
//	var index = nums.Length - 1;

//	while (index >= 1)
//	{
//		var indexNext = index - 1;
//		while (indexNext >=0)
//		{
//			if (nums[index] > nums[indexNext])
//			{
//				var temp = nums[index];
//				nums[index] = nums[indexNext];
//				nums[indexNext] = temp;
//				return nums;
//			}
//			indexNext--;
//		}
//		index--;
//	}
//	Array.Sort(nums);

//	return nums;
//}

//int[] NextPermutation(int[] nums)
//{
//	int n = nums.Length;
//	int i = n - 2;

//	// Step 1: Find the first decreasing element from the right
//	while (i >= 0 && nums[i] >= nums[i + 1])
//	{
//		i--;
//	}

//	// Step 2: If found, swap it with the next greater element on the right
//	if (i >= 0)
//	{
//		int j = n - 1;
//		while (nums[j] <= nums[i])
//		{
//			j--;
//		}
//		Swap(nums, i, j);
//	}

//	// Step 3: Reverse the remaining part after the swapped index
//	Reverse(nums, i + 1, n - 1);
//	return nums;
//}

//void Swap(int[] nums, int i, int j)
//{
//	int temp = nums[i];
//	nums[i] = nums[j];
//	nums[j] = temp;
//}

//void Reverse(int[] nums, int start, int end)
//{
//	while (start < end)
//	{
//		Swap(nums, start, end);
//		start++;
//		end--;
//	}
//}

int[][] Merge(int[][] intervals)
{
	var result = new List<int[]>();
	var sortedList = intervals.OrderBy(x => x[0]).ToList();
	Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
	for (var i = 0; i < sortedList.Count - 1; i++)
	{
		if (sortedList[i][1] >= sortedList[i+1][0])
		{
			sortedList[i + 1][0] = sortedList[i][0];
			if (sortedList[i][1] >= sortedList[i + 1][1])
			{
				sortedList[i + 1][1] = sortedList[i][1];
			}
		}
		else
		{
			result.Add(sortedList[i]);
		}
	}
	result.Add(sortedList[sortedList.Count - 1]);
	string.IsNullOrWhiteSpace('c'.ToString());
	return result.ToArray();
}

int[] NextPermutation(int[] nums)
{
	var index = nums.Length - 2;
	Array.Find(nums, x => x != 2);
	while (index >= 0 && nums[index] >= nums[index + 1]) { index--; }

	var indexNext = nums.Length - 1;
	while (indexNext > index && index >= 0)
	{
		if (nums[index] < nums[indexNext])
		{
			Swap(nums, index, indexNext);
			break;
		}
		indexNext--;
	}

	Reverse(nums, index + 1, nums.Length - 1);

	return nums;
}

void Swap(int[] nums, int i, int j)
{
	int temp = nums[i];
	nums[i] = nums[j];
	nums[j] = temp;
}

void Reverse(int[] nums, int start, int end)
{
	while (start < end)
	{
		Swap(nums, start, end);
		start++;
		end--;
	}
}

/////////////////////////////////////////////////
int CharacterReplacement(string s, int k)
{
	var chars = s.Select(x => x).Distinct().ToArray();
	int n = s.Length;
	int first = 0;
	int longest = n > 0 ? 1 : 0;
	var pArray = s.ToCharArray();
	Array.Sort(pArray, (a, b) => a.CompareTo(b));
	foreach(var c in chars)
	{
		for (var i = 0; i < n; i++)
		{
			var j = k;
			var index = i;
			while (index < n && (j > 0 || c == s[index]))
			{
				if (c != s[index++])
				{
					j--;
				}
			}
			if ((index - i) > longest)
			{
				longest = index - i;
			}
		}
	}

	return longest;
}
int CharacterReplacement1(string s, int k)
{
	int left = 0, maxCount = 0, maxLength = 0;
	Dictionary<char, int> charCount = new Dictionary<char, int>();

	for (int right = 0; right < s.Length; right++)
	{
		if (!charCount.ContainsKey(s[right]))
		{
			charCount[s[right]] = 0;
		}
		charCount[s[right]]++;
		maxCount = Math.Max(maxCount, charCount[s[right]]);

		if ((right - left + 1) - maxCount > k)
		{
			charCount[s[left]]--;
			left++;
		}

		maxLength = Math.Max(maxLength, right - left + 1);
	}

	return maxLength;
}

//////////////////////////////////////////////////
IList<int> FindAnagrams(string s, string p)
{
	List<int> result = new List<int>();
	if (s.Length < p.Length) return result;

	int[] pCount = new int[26];
	int[] sCount = new int[26];
	
	// Count frequency of each character in p
	foreach (char c in p)
	{
		pCount[c - 'a']++;
	}

	int windowSize = p.Length;

	// Initialize the first window
	for (int i = 0; i < windowSize; i++)
	{
		sCount[s[i] - 'a']++;
	}

	// Check if the first window is an anagram
	if (AreEqual(pCount, sCount))
	{
		result.Add(0);
	}

	// Slide the window across s
	for (int i = windowSize; i < s.Length; i++)
	{
		sCount[s[i] - 'a']++; // Add new character to the window
		sCount[s[i - windowSize] - 'a']--; // Remove the leftmost character

		if (AreEqual(pCount, sCount))
		{
			result.Add(i - windowSize + 1);
		}
	}

	return result;
}
bool AreEqual(int[] arr1, int[] arr2)
{
	for (int i = 0; i < 26; i++)
	{
		if (arr1[i] != arr2[i]) return false;
	}
	return true;
}

/////////////////////////////////////////////////////////////
string ShiftingLetters(string s, int[] shifts)
{
	var range = 'z' - 'a' + 1;
	var charArray = new char[s.Length];
	var newShifts = new long[s.Length];
	var last = s.Length - 1;

	newShifts[last] = shifts[last];
	long newChar = s[last] - 'a' + newShifts[last];
	charArray[last] = (char)('a' + (newChar % range));

	for (var i = shifts.Length - 2; i >= 0; i--)
	{
		newShifts[i] = newShifts[i + 1] + shifts[i];
		newChar = s[i] - 'a' + newShifts[i];
		charArray[i] = (char)('a' + (newChar % range));
	}

	return new string(charArray);
}
/////////////////////////////////////////////////////////////
int NumberOfSubarrays(int[] nums, int k)
{
	int left = 0;
	int right = 0;
	int count = k;
	int total = 0;
	var odds = new List<int>();

	for (right = 0; right < nums.Length; right++)
	{
		if (nums[right] % 2 == 1)
		{
			odds.Add(right);
			if (count == 0)
			{
				left = odds.First() + 1;
				odds.RemoveAt(0);
			}
			else
			{
				count--;
			}
		}
		if (count == 0)
		{
			total += odds.First() - left + 1;
		}
	}
	return total;
}
//////////////////////////////////////////////////////
int MaxVowels(string s, int k)
{
	int left = 0;
	int right = k;
	int maxVowels = 0;
	int currentVowels = 0;
	var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

	// Reference
	for (var i = 0; i < k; i++)
	{
		if (vowels.Contains(s[i]))
		{
			currentVowels++;
		}
	}
	maxVowels += currentVowels;
	for (;right < s.Length; right++)
	{
		if (vowels.Contains(s[right]))
		{
			currentVowels++;
		}
		if (vowels.Contains(s[left++]))
		{
			currentVowels--;
		}
		if (currentVowels > maxVowels)
		{
			maxVowels = currentVowels;
		}
		if (maxVowels == k)
		{
			break;
		}
	}
	return maxVowels;
}
//////////////////////////////////////////////////////
int MinimumDeletions2(string s) {
	var aList = new List<int>();
	var bList = new List<int>();
	var occurancies = new List<int[]>();
	var min = int.MaxValue;
	
	for (var i = 0; i < s.Length; i++)
	{
		if (s[i] == 'a')
		{
			aList.Add(i);
		}
		else
		{
			bList.Add(i);
		}
	}

	if (aList.Count == 0 || bList.Count == 0)
	{
		return 0;
	}
	for (var i = 0; i < s.Length; i++)
	{
		var a = aList.Where(x => x > i).Count();
		var b = bList.Where(x => x < i).Count();
		min = Math.Min(min, a + b);
	}
	return min;
    }

int MinimumDeletions(string s)
{
	var aList = new int[s.Length];
	var bList = new int[s.Length];
	var min = int.MaxValue;
	var totalA = 0;
	var totalB = 0;
	int index = 0;
	int jndex = s.Length - 1;

	while (index < s.Length)
	{
		bList[index] = totalB;
		if (s[index] == 'b')
		{
			totalB++;
		}
		aList[jndex] = totalA;
		if (s[jndex] == 'a')
		{
			totalA++;
		}
		index++;
		jndex--;
	}
	if (totalA == 0 || totalB == 0)
	{
		return 0;
	}
	for (var i = 0; i < s.Length; i++)
	{
		min = Math.Min(min, aList[i] + bList[i]);
	}
	return min;
}
/////////////////////////////////////////////////////////
int MaxOperations(int[] nums, int k)
{
	var totalOperetions = 0;
	var list = new List<int>();

	for (var i = 0; i < nums.Length; i++)
	{
		var index = list.IndexOf(nums[i]);
		
		if (index >= 0)
		{
			totalOperetions++;
			list.RemoveAt(index);
		}
		else
		{
			list.Add(k - nums[i]);
		}
	}

	return totalOperetions;
}
///////////////////////////////////////////////////////////////
double AverageWaitingTime(int[][] customers)
{
	if (customers.Length == 0) return 0;

	double totalTime = customers[0][1];
	double finishTime = customers[0][0] + customers[0][1];

	for (var i = 1;i < customers.Length;i++)
	{
		double oldOrderWait = finishTime - customers[i][0];
		double waitTime = (oldOrderWait > 0 ? oldOrderWait : 0) + customers[i][1];
		totalTime += waitTime;
		finishTime = customers[i][0] + waitTime;
	}

	return totalTime / (double)customers.Length;
}
/////////////////////////////////////////////////////////////


ListNode AddTwoNumbers(ListNode l1, ListNode l2)
{
	ListNode output = null;
	var overflow = 0;
	var node1 = l1;
	var node2 = l2;
	ListNode currentNode = null;

	while(node1 != null || node2 != null)
	{
		var sum = (node1 != null ? node1.val : 0) + (node2 != null ? node2.val : 0) + overflow;
		overflow = sum / 10;
		if (output == null)
		{
			output = new ListNode(sum % 10, null);
			currentNode = output;
		}
		else
		{
			currentNode.next = new ListNode(sum % 10, null);
			currentNode = currentNode.next;
		}
		node1 = node1?.next;
		node2 = node2?.next;
	}
	if (overflow > 0 && currentNode != null)
	{
		currentNode.next = new ListNode(overflow, null);
	}

	return output;
}


//Console.WriteLine(string.Join(",", AddTwoNumbers([2, 4, 3], [5, 6, 4])));
//Console.WriteLine(string.Join(",", AddTwoNumbers([0], [0])));
//Console.WriteLine(string.Join(",", AddTwoNumbers([9, 9, 9, 9, 9, 9, 9], [9, 9, 9, 9])));

var l1 = new ListNode();
var l2 = new ListNode();
l1.val = 2;
l1.next = new ListNode();
(l1.next).val = 4;
(l1.next).next = new ListNode();
((l1.next).next).val = 3;

l2.val = 5;
l2.next = new ListNode();
(l2.next).val = 6;
(l2.next).next = new ListNode();
((l2.next).next).val = 4;

//AddTwoNumbers(l1, l2);

///////////////////////////////////////////////////////////////////////////
bool SearchMatrix(int[][] matrix, int target)
{
	if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;

	int m = matrix.Length, n = matrix[0].Length;
	int left = 0, right = m * n - 1;

	while (left <= right)
	{
		int mid = left + (right - left) / 2;
		int midValue = matrix[mid / n][mid % n];

		if (midValue == target) return true;
		else if (midValue < target) left = mid + 1;
		else right = mid - 1;
	}

	return false;
}
////////////////////////////////////////////////////////////////////
int Search(int[] nums, int target)
{
	int n = nums.Length;
	var pivot = 0;
	for (var i = 0; i < n - 1; i++)
	{
		if (nums[i] > nums[i+1])
		{
			pivot = i + 1;
			break;
		}
	}

	int left = 0, right = n - 1;
	while (left <= right)
	{
		int mid = left + (right - left) / 2;
		if (nums[(mid + pivot) % n] == target)
		{
			return (mid + pivot) % n;
		}
		else if (nums[(mid + pivot) % n] < target)
		{
			left = mid + 1;
		}
		else
		{
			right = mid - 1;
		}
	}

	return -1;
}
///////////////////////////////////////////////////////////////////
IList<IList<int>> FourSum(int[] nums, int target)
{
	List<IList<int>> result = new List<IList<int>>();
	int n = nums.Length;
	if (n < 4) return result; // Edge case: Less than 4 elements

	Array.Sort(nums); // Sort the array for easier duplicate handling

	for (int i = 0; i < n - 3; i++)
	{
		if (i > 0 && nums[i] == nums[i - 1]) continue; // Skip duplicates

		for (int j = i + 1; j < n - 2; j++)
		{
			if (j > i + 1 && nums[j] == nums[j - 1]) continue; // Skip duplicates

			int left = j + 1, right = n - 1;

			while (left < right)
			{
				long sum = (long)nums[i] + nums[j] + nums[left] + nums[right];

				if (sum == target)
				{
					result.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });

					// Skip duplicate values
					while (left < right && nums[left] == nums[left + 1]) left++;
					while (left < right && nums[right] == nums[right - 1]) right--;

					left++;
					right--;
				}
				else if (sum < target)
				{
					left++;
				}
				else
				{
					right--;
				}
			}
		}
	}

	return result;
}
////////////////////////////////////////////////////////////////
IList<IList<string>> GroupAnagrams(string[] strs)
{
	List<string[]> list = new List<string[]>();
	list.Insert(0, )
	for (int i = 0; i < strs.Length; i++)
	{ 
		var tab = strs[i].ToCharArray();
		Array.Sort(tab);
		list.Add(new string[]
		{
			new string(tab),
			i.ToString(),
		});
	}
	var listTab = list.ToArray();
	Array.Sort(listTab, (a, b) => a[0].CompareTo(b[0]));

	var all = new List<IList<string>>();
	var result = new List<string>();

	for (int i = 0; i < listTab.Length; i++)
	{
		result.Add(strs[int.Parse(listTab[i][1])]);
		if (i < listTab.Length - 1)
		{
			if (listTab[i][0] != listTab[i + 1][0])
			{
				all.Add(result);
				result = new List<string>();
			}
		}
	}
	if (result.Count > 0)
	{
		all.Add(result);
	}

	return all;
}
////////////////////////////////////////////////////////////////
int[] Intersection(int[] nums1, int[] nums2)
{
	var toTest1 = nums1.Length > nums2.Length ? nums2 : nums1;
	var toTest2 = nums1.Length > nums2.Length ? nums1 : nums2;
	var list = new List<int>();

	foreach(var n in toTest1)
	{
		if (toTest2.Contains(n) && !list.Contains(n))
		{
			list.Add(n);
		}
	}

	return list.ToArray();
}
///////////////////////////////////////////////////////////
int Compress(char[] chars)
{
	var list = new List<int[]>();

	foreach (var c in chars)
	{
		if (list.Count == 0)
		{
			list.Add(new int[] { c, 0 });
		}
		
		var last = list.Last();
		if (last[0] != c)
		{
			list.Add(new int[] { c, 0 });
			last = list.Last();
		}
		last[1]++;
	}

	var str = "";
	var index = 0;
	foreach (var l in list)
	{
		var c = (char)l[0];
		str += c;
		chars[index++] = c;
		if (l[1] > 1)
		{
			var num = l[1].ToString();
			str += num;
			foreach(var nc in num)
			{
				chars[index++] = nc;
			}
		}
	}

	return str.Length;
}
/////////////////////////////////////////////////////////////
int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
{
	var intervals = new List<int[]>();
	var firstIndex = 0;
	var secondIndex = 0;

	if (firstList.Length == 0 || secondList.Length == 0) return intervals.ToArray();

	while(firstIndex < firstList.Length || secondIndex < secondList.Length)
	{
		var firstItem = firstList[firstIndex];
		var secondItem = secondList[secondIndex];

		if (secondItem[1] - firstItem[0] < 0 || firstItem[1] - secondItem[0] < 0)
		{
			
		}
		else
		{
			var max = Math.Max(firstItem[0], secondItem[0]);
			var min = Math.Min(firstItem[1], secondItem[1]);
			intervals.Add(new int[] { max, min });
		}
		if (firstIndex >= firstList.Length - 1 && secondIndex >= secondList.Length - 1)
		{
			break;
		}
		var temoFirstIndex = firstIndex;
		if (firstIndex < firstList.Length - 1 &&
			(secondIndex >= secondList.Length - 1 || firstItem[1] < secondList[secondIndex + 1][0]))
		{
			firstIndex++;
		}
		if (secondIndex < secondList.Length - 1 &&
			(temoFirstIndex >= firstList.Length - 1 || secondItem[1] <  firstList[temoFirstIndex + 1][0]))
		{
			secondIndex++;
		}
	}

	return intervals.ToArray();
}
///////////////////////////////////////////////////////////////////////////////
int MaxNumberOfFamilies(int n, int[][] reservedSeats)
{
	var maxGroups = 0;
	var leftBlock = new int[] { 2, 3, 4, 5 };
	var centerBlock = new int[] { 4, 5, 6, 7 };
	var rightBlock = new int[] { 6, 7, 8, 9 };

	for (var row = 1; row <= n; row++)
	{
		var rowReserved = reservedSeats.Where(x => x[0] == row).Select(x => x[1]).ToArray();
		var reservedBlocks = new int[] { 0, 0, 0 };
		for (var i = 0; i < rowReserved.Length; i++)
		{
			if (leftBlock.Contains(rowReserved[i]))
			{
				reservedBlocks[0]++;
			}
			if (centerBlock.Contains(rowReserved[i]))
			{
				reservedBlocks[1]++;
			}
			if (rightBlock.Contains(rowReserved[i]))
			{
				reservedBlocks[2]++;
			}
		}
		reservedBlocks[1] = reservedBlocks[0] == 0 || reservedBlocks[2] == 0 ? 1 : reservedBlocks[1];
		var count = reservedBlocks.Count(x => x == 0);
		maxGroups += Math.Min(2, count);

		//var last = 1;
		//for (var i = 0; i < rowReserved.Length; i++)
		//{
		//	var empty = rowReserved[i] - last - 1;
		//	last = rowReserved[i];
		//	if (last == 2 || last == 6)
		//	{
		//		last++;
		//	}
		//	maxGroups += empty / 4;
		//}
		//var emptyLast = 10 - last - 1;
		//maxGroups += emptyLast / 4;
	}

	return maxGroups;
}
/////////////////////////////////////////////////////////////////
int[] TopKFrequent(int[] nums, int k)
{
	Dictionary<int, int> dict = new Dictionary<int, int>();

	foreach(var n in nums)
	{
		if (!dict.ContainsKey(n))
		{
			dict.Add(n, 0);
		}
		dict[n]++;
	}

	var list= new List<int>();
	foreach(var (key, val) in dict.OrderByDescending(x => x.Value).Take(k))
	{
		list.Add(key);
	}

	return list.ToArray();
}
/////////////////////////////////////////////////////////////////
int MaxArea(int[] height)
{
	var first = 0;
	var last = height.Length - 1;

	var sum = Math.Min(height[first], height[last]) * (last - first);

	var tempFirst = first;
	var tempLast = last;
	while (tempFirst < tempLast)
	{
		var toCheck = height[tempFirst] < height[tempLast] ? tempFirst : tempLast;
		var isLeft = !(height[tempFirst] < height[tempLast]);

		var next = FindNextBigger(height, toCheck, isLeft, isLeft ? tempFirst : tempLast);
		if (isLeft)
		{
			tempLast = next;
		}
		else
		{
			tempFirst = next;
		}
		var tempSum = Math.Min(height[tempFirst], height[tempLast]) * (tempLast - tempFirst);
		if (tempSum > sum)
		{
			sum = tempSum;
			first = tempFirst;
			last = tempLast;
		}
	}

	return sum;
}

int FindNextBigger(int[] height, int current, bool isLeft, int limit)
{
	var sign = isLeft ? -1 : 1;
	var next = current + sign;

	if(sign * next > sign * limit) return current;

	while(sign * next < sign * limit)
	{
		if (height[current] < height[next])
		{
			return next;
		}
		next += sign;
	}

	return next;
}
/////////////////////////////////////////////////////////////////////////////////////////
IList<IList<int>> PathSum(TreeNode root, int targetSum)
{
	var all = new List<IList<int>>();
	var current = new List<int>();

	CalculatePath(root, current, targetSum, all);

	return all;
}

void CalculatePath(TreeNode node, List<int> current, int target, IList<IList<int>> all)
{
	if (node is null) return;

	current.Add(node.val);
	var sum = current.Sum();

	if(sum == target && node.left == null && node.right == null)
	{
		all.Add(current.ToList());
	}
	CalculatePath(node.left, current.ToList(), target, all);
	CalculatePath(node.right, current.ToList(), target, all);
}


TreeNode ex1()
{
	var lf7 = new TreeNode(7);
	var lf2 = new TreeNode(2);
	var n11 = new TreeNode(11, lf7, lf2);
	var n4 = new TreeNode(4, n11);

	var lf5 = new TreeNode(5);
	var lf1 = new TreeNode(1);
	var lf13 = new TreeNode(13);
	var n4_r = new TreeNode(4, lf5, lf1);
	var n8 = new TreeNode(8, lf13, n4_r);

	var root = new TreeNode(5, n4, n8);

	return root;
}
TreeNode ex2()
{
	var lf2 = new TreeNode(2);
	var lf3 = new TreeNode(3);

	var root = new TreeNode(1, lf2, lf3);

	return root;
}
TreeNode ex3()
{
	var lfM3 = new TreeNode(-3);

	var root = new TreeNode(-2, null, lfM3);

	return root;
}


//Console.WriteLine(PathSum(ex1(), 22));
//Console.WriteLine(PathSum(ex2(), 5));
Console.WriteLine(PathSum(ex3(), -5));



//Console.WriteLine(MaxArea([1, 8, 6, 2, 58, 61, 8, 3, 7]));
//Console.WriteLine(MaxArea([1, 8, 6, 2, 5, 4, 8, 3, 7]));
//Console.WriteLine(MaxArea([1, 1]));

//Console.WriteLine(TopKFrequent([1, 2], 2));
//Console.WriteLine(TopKFrequent([1, 1, 1, 2, 2, 3], 2));
//Console.WriteLine(TopKFrequent([1], 1));

//Console.WriteLine(MaxNumberOfFamilies(2, [[2, 9], [2, 7], [2, 3], [1, 4], [2, 8], [1, 7], [2, 10], [1, 6], [2, 2], [1, 5]]));
//Console.WriteLine(MaxNumberOfFamilies(3, [[1, 2], [1, 3], [1, 8], [2, 6], [3, 1], [3, 10]]));
//Console.WriteLine(MaxNumberOfFamilies(2, [[2, 1], [1, 8], [2, 6]]));
//Console.WriteLine(MaxNumberOfFamilies(4, [[4, 3], [1, 4], [4, 6], [1, 7]]));


//Console.WriteLine(IntervalIntersection([[4, 11]], [[1, 2], [8, 11], [12, 13], [14, 15], [17, 19]]));
//Console.WriteLine(IntervalIntersection([[0, 2], [5, 10], [13, 23], [24, 25]], [[1, 5], [8, 12], [15, 24], [25, 26]]));
//Console.WriteLine(IntervalIntersection([[1, 3], [5, 9]], []));

//Console.WriteLine(Compress(['a', 'a', 'b', 'b', 'c', 'c', 'c']));
//Console.WriteLine(Compress(['a']));
//Console.WriteLine(Compress(['a', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b']));

//Console.WriteLine(string.Join(",", Intersection([1, 2, 2, 1], [2, 2])));
//Console.WriteLine(string.Join(",", Intersection([4, 9, 5], [9, 4, 9, 8, 4])));

//Console.WriteLine(GroupAnagrams(["", "b"]));
//Console.WriteLine(GroupAnagrams(["eat", "tea", "tan", "ate", "nat", "bat"]));
//Console.WriteLine(GroupAnagrams([""]));
//Console.WriteLine(GroupAnagrams(["a"]));

//Console.WriteLine(FourSum([0, 0, 0, 0], 0));
//Console.WriteLine(FourSum([1, 0, -1, 0, -2, 2], 0));
//Console.WriteLine(FourSum([2, 2, 2, 2, 2], 8));

//Console.WriteLine(Search([4, 5, 6, 7, 0, 1, 2], 0));
//Console.WriteLine(Search([4, 5, 6, 7, 0, 1, 2], 3));
//Console.WriteLine(Search([1], 0));

//Console.WriteLine(SearchMatrix([[1, 3]], 3));
//Console.WriteLine(SearchMatrix([[1, 3]], 2));
//Console.WriteLine(SearchMatrix([[1]], 1));
//Console.WriteLine(SearchMatrix([[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]], 60));
//Console.WriteLine(SearchMatrix([[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]], 13));

public class ListNode
{
	public int val;
	public ListNode next;
	public ListNode(int val = 0, ListNode next = null)
	{
		this.val = val;
		this.next = next;
	}
}

 public class TreeNode {
     public int val;
     public TreeNode left;
     public TreeNode right;
     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
         this.val = val;
         this.left = left;
         this.right = right;
     }
 }

//Console.WriteLine(AverageWaitingTime([[1, 2], [2, 5], [4, 3]]));
//Console.WriteLine(AverageWaitingTime([[5, 2], [5, 4], [10, 3], [20, 1]]));

//Console.WriteLine(MaxOperations([3, 1, 5, 1, 1, 1, 1, 1, 2, 2, 3, 2, 2], 1));
//Console.WriteLine(MaxOperations([1, 2, 3, 4], 5));
//Console.WriteLine(MaxOperations([3, 1, 3, 4, 3], 6));

//Console.WriteLine(MinimumDeletions("aabbbbaabababbbbaaaaaabbababaaabaabaabbbabbbbabbabbababaabaababbbbaaaaabbabbabaaaabbbabaaaabbaaabbbaabbaaaaabaa"));
//Console.WriteLine(MinimumDeletions("a"));
//Console.WriteLine(MinimumDeletions("aaaaaaaaaaaaaa"));
//Console.WriteLine(MinimumDeletions("aababbab"));
//Console.WriteLine(MinimumDeletions("bbaaaaabb"));

//Console.WriteLine(MaxVowels("abciiidef", 3));
//Console.WriteLine(MaxVowels("aeiou", 2));
//Console.WriteLine(MaxVowels("leetcode", 3));

//Console.WriteLine(NumberOfSubarrays([1, 1, 2, 1, 1], 3));
//Console.WriteLine(NumberOfSubarrays([2, 4, 6], 1));
//Console.WriteLine(NumberOfSubarrays([2, 2, 2, 1, 2, 2, 1, 2, 2, 2], 2));


//Console.WriteLine(ShiftingLetters("mkgfzkkuxownxvfvxasy", [505870226,437526072,266740649,224336793,532917782,311122363,567754492,595798950,81520022,684110326,137742843,275267355,856903962,148291585,919054234,467541837,622939912,116899933,983296461,536563513]));
//Console.WriteLine(ShiftingLetters("aaa", [1, 2, 3]));
//Console.WriteLine(ShiftingLetters("z", [1]));

//Console.WriteLine(FindAnagrams("cbaebabacd", "abc"));
//Console.WriteLine(FindAnagrams("abab", "ab"));

//Console.WriteLine(CharacterReplacement("ABAB", 2)); // 4
//Console.WriteLine(CharacterReplacement("AABABBA", 1)); // 4
//Console.WriteLine(CharacterReplacement("ABBB", 2)); // 4
//Console.WriteLine(Merge([[1, 3], [2, 6], [8, 10], [15, 18]]));
//Console.WriteLine(Merge([[1, 4], [0, 4]]));
//Console.WriteLine(Merge([[1, 4], [2, 3]]));
//Console.WriteLine(Merge([[2, 3],[1, 4]]));
//Console.WriteLine(string.Join(",", NextPermutation([1,2,3])));
//Console.WriteLine(LongestPalindrome);
//Console.WriteLine(LongestPalindrome);

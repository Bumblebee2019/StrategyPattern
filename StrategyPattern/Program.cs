using System.Collections.Concurrent;

public class SortingContext
{
    private SortingStrategy sortingStrategy;
    public SortingContext(SortingStrategy sortingStrategy)
    {
        this.sortingStrategy = sortingStrategy;
    }
    public void setSortingStrategy(SortingStrategy sortingStrategy)
    {
        this.sortingStrategy=sortingStrategy;   
    }
    public void performSort(int[] array)
    {
        sortingStrategy.sort(array);
    }
}

public interface SortingStrategy
{
    void sort(int[] array);
}

public class QuickSortStrategy : SortingStrategy
{
    public void sort(int[] array)
    {
        //Implement QuickSort here
        int n = array.Length - 1;
        quickSort(array, 0, n);
    }
    public static void quickSort(int[] a, int left, int right)
    {
        if(left < right)
        {
            var pi = partition(a, left, right);
            quickSort(a, left, pi - 1);
            quickSort(a, pi + 1, right);
        }
    }
    public static int partition(int[] a, int left, int right)
    {
        int index = left;
        int pivot = a[left];

        for(int i = left + 1; i <= right; ++i)
        {
            if(pivot > a[i])
            {
                ++index;
                Swap(a, index, i);
            }
        }
        Swap(a, index, left);
        return index;
    }
    public static void Swap(int[] array, int one, int two)
    {
        int temp = array[one];
        array[one] = array[two];
        array[two] = temp;
    }
}

public class MergeSortStrategy : SortingStrategy
{
    public void sort(int[] array)
    {
        int left = 0;
        int right = array.Length - 1;
        MergeSort(array, left, right);
    }
    public void MergeSort(int[] array, int left, int right)
    {
        if(left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(array, left, mid);
            MergeSort(array, mid+1, right);
            Merge(array, left, mid, right);
        }
    }
    public void Merge(int[] a, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;
        int[] L = new int[n1];
        int[] R = new int[n2];
        //int[] result = new int[a.Length];
        int i, j = 0;
        int k = 0;
        for(i = 0; i < n1; ++i)
        {
            L[i] = a[i + left];
        }
        for(j = 0; j < n2; ++j)
        {
            R[j] = a[j + mid + 1];
        }
        i = 0;
        j = 0;
        k = left;
        while(i < n1 && j < n2)
        {
            if (L[i] < R[j])
            {
                a[k] = L[i];
                ++i;
            }
            else
            {
                a[k] = R[j];
                ++j;
            }
            ++k;
        }
        while(i < n1)
        {
            a[k] = L[i];
            ++i; ++k;
        }
        while(j < n2)
        {
            a[k] = R[j];
            ++j; ++k;
        }
    }
}



public class Client
{
    public static void Main()
    { 
        SortingContext sortingContext = new SortingContext(new MergeSortStrategy());
        int[] array2 = { 8, 3, 7, 4, 2 };
        Console.WriteLine("Using Merge Sort");
        Console.Write("Array before sorting: ");
        foreach(var element in array2)
        {
            Console.Write(element);
        }
        Console.WriteLine();
        Console.Write("Array after sorting: ");
        sortingContext.performSort(array2); // Output: Sorting using Merge Sort
        foreach (var element in array2)
        {
            Console.Write(element);
        }
        Console.WriteLine();
      
        sortingContext.setSortingStrategy(new QuickSortStrategy());
        int[] array3 = { 6, 1, 3, 9, 5 };
        Console.WriteLine("Using Quick Sort");
        Console.Write("Array before sorting: ");
        foreach (var element in array3)
        {
            Console.Write(element);
        }
        sortingContext.performSort(array3); // Output: Sorting using Quick Sort
        Console.WriteLine();
        Console.Write("Array after sorting: ");
        sortingContext.performSort(array3); // Output: Sorting using Merge Sort
        foreach (var element in array3)
        {
            Console.Write(element);
        }
    }
}
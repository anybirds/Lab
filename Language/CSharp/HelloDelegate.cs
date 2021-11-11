using System;

// why parameter name required? 
delegate void MyDelegate(int arg);


class HelloDelegate {
    // contravariant generic modifier (in)
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-generic-modifier
    public delegate int Comparison<in T>(T l, T r);

    void MyMethod(int arg) {
        Console.WriteLine("MyMethod {0}", arg);
    }

    static void Swap<T>(ref T l, ref T r) {
        T temp = l;
        l = r;
        r = temp;
    }

    // may be improved by replacing T[] to ICollection, removing Comparer<T> and adding IComparable constraints to T.
    // indexers in interfaces 
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/indexers-in-interfaces
    static void Sort<T>(T[] arr, Comparison<T> comparison) {
        // recursive quick sort
        Action<T[], int, int> quickSort = null;
        quickSort = (T[] slice, int l, int r) => {
            if (l >= r) {
                return;
            }

            // separate array into two 
            int m = l;
            for (int i = l + 1; i <= r; i++) {
                if (comparison(slice[m], slice[i]) > 0) {
                    Swap<T>(ref slice[m], ref slice[i]);
                    Swap<T>(ref slice[i], ref slice[++m]);
                }
            }
            // recursive sorting
            quickSort(slice, l, m - 1);
            quickSort(slice, m + 1, r);
        };
        quickSort(arr, 0, arr.Length - 1);
    }

    static void Main(string[] args) {
        HelloDelegate helloDelegate = new HelloDelegate();
        MyDelegate myDelegate = helloDelegate.MyMethod;
        myDelegate(5); // MyMethod 5

        myDelegate += delegate (int arg) { Console.WriteLine("anonymous method {0}", arg); };
        myDelegate(10); // MyMethod 10 anonymous method 10

        int left = 1;
        int right = 2;
        Swap(ref left, ref right);
        Console.WriteLine("{0} {1}", left, right); // 2 1

        int[] arr = {8, 2, 5, 7, 3, 1, 4, 10, 9, 6};
        Sort(arr, delegate (int l, int r) { return l - r; });
        foreach (int n in arr) {
            Console.Write("{0} ", n); // 1 2 3 4 5 6 7 8 9 10
        }
        Console.WriteLine();
        
    }
}
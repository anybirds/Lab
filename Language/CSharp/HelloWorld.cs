using System;
using HelloNamespace;

struct TestOut {
    public int a;
    public int b;
    public TestOut(int a, int b) {
        this.a = a;
        this.b = b;
    }

    // overrides ValueType.ToString()
    public override string ToString() {
        return new string(String.Format("{0}, {1}", a, b).ToCharArray());
    }
}

/*
csc /target:exe /reference:HelloAssembly.dll HelloWorld.cs
*/
class HelloWorld {
    static void FillTestOut(out TestOut testOut) {
        // either way is possible
        testOut = new TestOut(1, 2);
        /*
        // all members should be filled
        testOut.a = 1;
        testOut.b = 2;
        */

        // can be used after filling in
        TestOut another = testOut;
        another.a = 3;
        Console.WriteLine(another);

    }

    static void ArrayArgTest(int[,] intArray) {
        Console.WriteLine(intArray.Rank);
    }

    static void VariableLengthTest(int a, int b) {
        Console.WriteLine("int, int");
    }
    
    static void VariableLengthTest(params int[] arr) {
        Console.WriteLine("params int[]");
    }

    static void NullCoalescingOperator(object obj, int? n) {
        object another = obj ?? null;
        int x = n ?? 0;
    }

    public static void Main(string[] args) {
        // calls external assembly method
        HelloAssembly.HelloFiveTimes();

        // C# 7.0 ref locals
        int a = 5;
        ref int b = ref a;
        a = 10;
        Console.WriteLine(b); // 10

        TestOut testOut;
        FillTestOut(out testOut);
        Console.WriteLine(testOut);

        ArrayArgTest(new int[,]{{1, 2}, {3, 4}});
        // ArrayArgTest({{1, 2}, {3, 4}}); // compile error

        VariableLengthTest(1, 2); // int, int
        VariableLengthTest(1, 2, 3); // params int[]

        // C# 6.0 interpolated verbatim string
        Console.WriteLine($@"C:\{a}");

        // read multiple integers from the same line 
        string line = Console.ReadLine();
        string[] tokens = line.Split();
        foreach (string word in tokens) {
            int integer;
            if (Int32.TryParse(word, out integer)) {
                Console.WriteLine(integer);
            }
        }
        
        // strings are immutable in C#
        string s1 = "123";
        string s2 = s1;
        s1 += "456"; // operator+ will create a new string
        Console.WriteLine(s2); // 123
    }
}
using System;
using System.Windows.Input;

// Custom Nullable<T> impl not possible https://stackoverflow.com/questions/25629207/nullablet-implementation
/*
struct HelloNullable<T> {
    private bool isNull;
    private T data;
    public HelloNullable(T data)
    {
        isNull = false;
        this.data = data;
    }
    public static implicit operator HelloNullable<T>(T t) {
        return new HelloNullable<T>(t);
    }
    public static explicit operator T(HelloNullable<T> t) {
        if (t.isNull) { throw new Exception(); } else { return t.data; }
    }
}
*/

class HelloObject {
    public static void Main(string[] args) {
        // everything is object in C#
        Console.WriteLine(100.ToString().Length); // 3

        Console.WriteLine(typeof(System.Object) == typeof(object)); // True
        Console.WriteLine(typeof(System.String) == typeof(string)); // True

        Console.WriteLine(typeof(int) == typeof(Nullable<int>)); // False
        Console.WriteLine(typeof(int?) == typeof(Nullable<int>)); // True

        /*
        HelloNullable<int> i = 10;
        int a = (int)i;
        Console.WriteLine(a); // 10
        i = null; // impl not supported by language
        try {
            a = (int)i; // exception
        } catch(Exception e) {
            Console.Write("Exception: {0}", e);
        }
        */
    }
}
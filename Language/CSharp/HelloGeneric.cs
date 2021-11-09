using System;
using System.Collections.Generic;

class Point<T>
{
    private T x;
    private T y;

    public Point(T x = default(T), T y = default(T)) {
        this.x = x;
        this.y = y;
    }

    // compile error 
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/differences-between-cpp-templates-and-csharp-generics
    /*
    static public Point<T> Add(Point<T> l, Point<T> r) {
        return new Point<T>(l.x + r.x, l.y + r.y);
    }
    */
}

class HelloGeneric {
    static T Max<T>(T l, T r) where T : IComparable<T> {
        return l.CompareTo(r) > 0 ? l : r;
    }

    static void Main(string[] args) {
        Console.WriteLine(Max<string>("ab", "ba"));
    }
}
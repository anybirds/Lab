using System;
using System.Collections;
using System.Collections.Generic;

interface ICollection<T> : IEnumerable<T> {
    int Count { get; }

    void Add(T item);
    void Clear();
    bool Contains(T item);
    void CopyTo(T[] array, int arrayIndex);
    bool Remove(T item);
}

interface IList<T> : ICollection<T> {
    T this[int index] { get; set; }   
    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

// generic list impl by managed array
class List<T> : IList<T> {
    private T[] array;

    public List() {
        count = 0;
        capacity = 0;
        array = null;
    }

    public List(int capacity) {
        count = 0;
        this.capacity = capacity;
        array = new T[capacity];
    }

    public List(IEnumerable<T> collection) : this() {
        foreach (T item in collection) {
            Add(item);
        }
    }

    private int count;
    public int Count {
        get {
            return count;
        }
    }

    private int capacity;
    public int Capacity {
        get {
            return capacity;
        }
        set {
            if (value < count) {
                throw new ArgumentOutOfRangeException();
            }
            capacity = value;
            T[] newArray = new T[capacity];
            if (array != null) {
                Array.Copy(array, newArray, count);
            }
            array = newArray;
        }
    }

    public void Add(T item) {
        if (count >= capacity) {
            Capacity = 1 << ((int)Math.Log(Convert.ToDouble(capacity), 2) + 1);
        }
        array[count++] = item;
    }

    public void Clear() {
        count = 0;
        capacity = 0;
        array = null;
    }

    public bool Contains(T item) {
        for (int i = 0; i < count ; i++) {
            if (item.Equals(array[i])) {
                return true;
            }
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex) {
        if (array == null) {
            throw new ArgumentNullException();
        } else if (arrayIndex < 0) {
            throw new ArgumentOutOfRangeException();
        } else if (array.Length - arrayIndex < count) {
            throw new ArgumentException();
        }
        for (int i = 0; i < count; i++) {
            array[i + arrayIndex] = this.array[i];
        }
    }

    public bool Remove(T item) {
        int index = IndexOf(item);
        if (index >= 0) {
            RemoveAt(index);
            return true;
        } else {
            return false;
        }
    }

    public T this[int index] {
        get {
            if (index < 0 || index >= count) {
                throw new ArgumentOutOfRangeException();
            }
            return array[index];
        }
        set {
            if (index < 0 || index >= count) {
                throw new ArgumentOutOfRangeException();
            }
            array[index] = value;
        }
    }

    public int IndexOf(T item) {
        for (int i = 0; i < count; i++) {
            if (item.Equals(array[i])) {
                return i;
            }
        }
        return -1;
    }

    public void Insert(int index, T item) {
        if (count >= capacity) {
            Capacity = 1 << ((int)Math.Log(Convert.ToDouble(capacity), 2) + 1);
        }
        for (int i = count; i >= index; i--) {
            array[i + 1] = array[i];
        }
        array[index] = item;
        count++;
    }

    public void RemoveAt(int index) {
        if (index < 0 || index >= count) {
            throw new ArgumentOutOfRangeException();
        }
        for (int i = index; i < count; i++) {
            array[i] = array[i + 1];
        }
        array[count--] = default(T);
    }

    public Enumerator GetEnumerator() {
        return new Enumerator(this);
    }

    // explicit interface implementations not included in assembly metadata
    // https://stackoverflow.com/questions/7268632/why-does-the-vs-metadata-view-does-not-display-explicit-interface-implemented-me
    IEnumerator IEnumerable.GetEnumerator() {
        return (IEnumerator)GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
        return (IEnumerator<T>)GetEnumerator();
    }

    // 
    public class Enumerator : IEnumerator<T> {
        private List<T> list;
        private int index;

        // how to hide constructor from outer space?
        public Enumerator(List<T> list) {
            this.list = list;
            index = -1;
        }

        public T Current {
            get {
                try {
                    return list[index];
                } catch(IndexOutOfRangeException) {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext() {
            return ++index < list.Count;
        }

        public void Dispose() {
            // nothing to dispose
        }

        object IEnumerator.Current {
            get {
                return (object)Current;
            }
        }

        // why should Reset be explicit interface implementation?
        void IEnumerator.Reset() {
            index = -1;
        }
    }
}

class HelloCollection {
    public class Part : IEquatable<Part>
    {
        public string PartName { get; set; }

        public int PartId { get; set; }

        public override string ToString()
        {
            return "ID: " + PartId + "   Name: " + PartName;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Part objAsPart = obj as Part;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return PartId;
        }
        public bool Equals(Part other)
        {
            if (other == null) return false;
            return (this.PartId.Equals(other.PartId));
        }
    // Should also override == and != operators.
    }

    static void Main(string[] args) {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Console.WriteLine("Count: {0}, Capacity : {1}", list.Count, list.Capacity); // Count: 3, Capacity: 4
        for (int i = 0; i < list.Count; i++) {
            Console.Write("{0} ", list[i]); // 1 2 3
        }
        Console.WriteLine();

        foreach (int item in list) {
            Console.Write("{0} ", item); // 1 2 3
        }

        // test by official document examples
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-5.0
        
        // Create a list of parts.
        List<Part> parts = new List<Part>();

        // Add parts to the list.
        parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });
        parts.Add(new Part() { PartName = "chain ring", PartId = 1334 });
        parts.Add(new Part() { PartName = "regular seat", PartId = 1434 });
        parts.Add(new Part() { PartName = "banana seat", PartId = 1444 });
        parts.Add(new Part() { PartName = "cassette", PartId = 1534 });
        parts.Add(new Part() { PartName = "shift lever", PartId = 1634 });

        // Write out the parts in the list. This will call the overridden ToString method
        // in the Part class.
        Console.WriteLine();
        foreach (Part aPart in parts)
        {
            Console.WriteLine(aPart);
        }

        // Check the list for part #1734. This calls the IEquatable.Equals method
        // of the Part class, which checks the PartId for equality.
        Console.WriteLine("\nContains(\"1734\"): {0}",
        parts.Contains(new Part { PartId = 1734, PartName = "" }));

        // Insert a new item at position 2.
        Console.WriteLine("\nInsert(2, \"1834\")");
        parts.Insert(2, new Part() { PartName = "brake lever", PartId = 1834 });

        //Console.WriteLine();
        foreach (Part aPart in parts)
        {
            Console.WriteLine(aPart);
        }

        Console.WriteLine("\nParts[3]: {0}", parts[3]);

        Console.WriteLine("\nRemove(\"1534\")");

        // This will remove part 1534 even though the PartName is different,
        // because the Equals method only checks PartId for equality.
        parts.Remove(new Part() { PartId = 1534, PartName = "cogs" });

        Console.WriteLine();
        foreach (Part aPart in parts)
        {
            Console.WriteLine(aPart);
        }
        Console.WriteLine("\nRemoveAt(3)");
        // This will remove the part at index 3.
        parts.RemoveAt(3);

        Console.WriteLine();
        foreach (Part aPart in parts)
        {
            Console.WriteLine(aPart);
        }

            /*

             ID: 1234   Name: crank arm
             ID: 1334   Name: chain ring
             ID: 1434   Name: regular seat
             ID: 1444   Name: banana seat
             ID: 1534   Name: cassette
             ID: 1634   Name: shift lever

             Contains("1734"): False

             Insert(2, "1834")
             ID: 1234   Name: crank arm
             ID: 1334   Name: chain ring
             ID: 1834   Name: brake lever
             ID: 1434   Name: regular seat
             ID: 1444   Name: banana seat
             ID: 1534   Name: cassette
             ID: 1634   Name: shift lever

             Parts[3]: ID: 1434   Name: regular seat

             Remove("1534")

             ID: 1234   Name: crank arm
             ID: 1334   Name: chain ring
             ID: 1834   Name: brake lever
             ID: 1434   Name: regular seat
             ID: 1444   Name: banana seat
             ID: 1634   Name: shift lever

             RemoveAt(3)

             ID: 1234   Name: crank arm
             ID: 1334   Name: chain ring
             ID: 1834   Name: brake lever
             ID: 1444   Name: banana seat
             ID: 1634   Name: shift lever
         */
    }
}
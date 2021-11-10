using System;
using System.Collections;
using System.Collections.Generic;

interface ICollection<in T> : IEnumerable<T> {
    int Count { get; }

    void Add(T item);
    void Clear();
    bool Contains();
    void CopyTo(T[] array, int arrayIndex);
    void Remove(T item);
}

interface IList<T> : ICollection<T> {
    T this[int index] { get; set; }   
    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

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
            Array.Copy(array, newArray, count);
            array = newArray;
        }
    }

    public void Add(T item) {
        if (count >= capacity) {
            Capacity = 1 << (Math.ILogB(Convert.ToDouble(capacity)) + 1);
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
            if (array[i] == item) {
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

    public void Remove(T item) {
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
            if (array[i] == item) {
                return i;
            }
        }
        return -1;
    }

    public void Insert(int index, T item) {
        if (count >= capacity) {
            Capacity = 1 << (Math.ILogB(Convert.ToDouble(capacity)) + 1);
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
        array[count--] = null;
    }

    // explicit interface implementations not included in assembly metadata
    // https://stackoverflow.com/questions/7268632/why-does-the-vs-metadata-view-does-not-display-explicit-interface-implemented-me
    public IEnumerator IEnumerable.GetEnumerator() {
        return (IEnumeartor)GetEnumerator();
    }

    public IEnumerator<T> IEnumerable<T>.GetEnumerator() {
        return (IEnumerator<T>)GetEnumerator();
    }

    // 
    public class Enumerator : IEnumerator<T> {
        private List<T> list;
        private index = -1;

        // how to hide constructor from outer space?
        public Enumerator(List<T> list) {
            this.list = list;
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

        public object IEnumerator.Current {
            get {
                return (object)Current;
            }
        }

        // why should Reset be explicit interface implementation?
        public void IEnumerator.Reset() {
            index = -1;
        }
    }
}

class HelloCollection {
    static void Main(string[] args) {
        
    }
}
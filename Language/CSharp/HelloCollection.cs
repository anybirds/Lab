using System;
using System.Collections;
using System.Collections.Generic;

interface ICollection<T> : IEnumerable<T> {
    int Count { get; }

    void Add(T item);
    void Clear();
    bool Contains();
    void CopyTo(T[] array, int arrayIndex);
    void Remove(T item);
}

interface IList<T> : ICollection<T> {
    T this[int index] { get; set; }   
    int indexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

class List<T> : IList<T> {
    public List() {

    }

    public List(int capacity) {

    }

    public List(IEnumerable<T> collection) {

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
            capacity = value;
        }
    }

    public void Add(T item) {

    }

    public void Clear() {

    }

    public bool Contains(T item) {

    }

    public void CopyTo(T[] array, int arrayIndex) {

    }

    public void Remove(T item) {

    }

    public T this[int index] {

    }

    public int IndexOf(T item) {

    }

    public void Insert(int index, T item) {

    }

    public void RemoveAt(int index) {

    }

    // explicit interface implementations not included in assembly metadata
    // https://stackoverflow.com/questions/7268632/why-does-the-vs-metadata-view-does-not-display-explicit-interface-implemented-me
    public IEnumerator IEnumerable.GetEnumerator() {

    }

    public IEnumerator<T> GetEnumerator() {

    }

    // 
    class Enumerator : IEnumerator<T> {
        public Enumerator() {

        }

        public T Current {
            get {

            }
        }

        public void MoveNext() {

        }

        public object IEnumerator.Current {
            get {
                return (object)Current;
            }
        }

        // why should Reset be explicit interface implementation?
        public void IEnumerator.Reset() {

        }
    }
    
}

class HelloCollection {
    static void Main(string[] args) {
        
    }
}
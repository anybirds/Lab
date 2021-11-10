using System;

class Base {
    public int Reused { get; set; }
    public Base(int reused) {
        this.Reused = reused;
    }
    public virtual void Print() { 
        Console.WriteLine("Virtual Base");
    }
}

class Derived : Base {
    // use new keyword to hide reused warning
    public new int Reused { get; set; }
    public Derived(int reused) : base(reused) {
        this.Reused = reused * 2;
    }
    public override void Print() {
        Console.WriteLine("Override Derived");
    }

    // public virtual void Print() { } // compile error
}

class Another : Base {
    public Another(int reused) : base(reused) {

    }

    public new virtual void Print() {
        Console.WriteLine("Virtual Another");
    }
}

interface IHelloInterface {
    int Hello { get; }
    void SayHello();
    void SayGoodbye();
}

class Monkey : IHelloInterface {
    // interface methods can be defined as both virtual or non-virtual
    public int Hello { get; set; } // can have set property

    public virtual void SayHello() {
        Console.WriteLine("ukiki!");
    }
    // should be makred as public
    public void SayGoodbye() {
        Console.WriteLine("uki...");
    }
}

class KingMonkey : Monkey {
    public override void SayHello()
    {
        Console.WriteLine("UKIKI!");
    }
}

class HelloClass {
    static void Main(string[] args) {
        Base b = new Derived(5);
        Console.WriteLine("Value of Reused in Base: {0}", b.Reused); // 5
        Derived d = b as Derived;
        if (d != null) {
            Console.WriteLine("Value of Reused in Derived: {0}", d.Reused); // 10
        }
        
        (new Base(5)).Print(); // Virtual Base
        b.Print(); // Override Derived
        d.Print(); // Override Derived
        
        Another a = new Another(5);
        b = (Base)a;
        b.Print(); // Virtual Base
        a.Print(); // Virtual Another

        Monkey monkey = new Monkey();
        KingMonkey kingMonkey = new KingMonkey();
        monkey.SayHello(); // ukiki!
        kingMonkey.SayHello(); // UKIKI!
        kingMonkey.SayGoodbye(); // uki...
    }
}
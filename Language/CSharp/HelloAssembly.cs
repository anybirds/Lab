using System;

/*
csc /target:library HelloAssembly.cs
*/
namespace HelloNamespace {
    public class HelloAssembly {
        public static void HelloFiveTimes() {
            for (int i=0; i<5; i++) {
                Console.WriteLine("Hello!");
            }
        }
    }
}
using System;

public interface IEnumerator {

}

// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=net-6.0
public interface IEnumerator<out T> : IEnumerator, IDisposable {

}

public interface IEnumerable {
    
}

// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-6.0
public interface IEnumerable<out T> : IEnumerable {

}

public interface ICollection<T> {

}

public interface IReadOnlyCollection<T> {

}
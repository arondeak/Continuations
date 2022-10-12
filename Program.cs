FeedEnumerable<float[]> input = new ();
var results = Sum(input).GetEnumerator();
input.SetNext(new [] {1f, 2, 3f});
results.MoveNext();
Console.WriteLine(results.Current);
input.SetNext(new [] {4f, 42f});
results.MoveNext();
Console.WriteLine(results.Current);
input.SetNext(new [] {1f});
results.MoveNext();
Console.WriteLine(results.Current);

static IEnumerable<float> Sum(IEnumerable<float[]> valsEnumerable)
{
    float sum = 0;
    foreach (var vals in valsEnumerable)
    {
        sum += vals.Length;
        yield return sum;
    }
}

class FeedEnumerator<T> : IEnumerator<T>
{
    object System.Collections.IEnumerator.Current
    {
        get {return Current;}
    }

    public T Current { get; set;}

    public bool MoveNext() => true;

    public void Reset() => throw new NotImplementedException("");

    public void Dispose() => throw new NotImplementedException("");
}

class FeedEnumerable<T> : IEnumerable<T>
{
    private FeedEnumerator<T> _feedEnumerator = new();
    
    public IEnumerator<T> GetEnumerator()
    {
        return _feedEnumerator;
    }

    public void SetNext(T next)
    {
        _feedEnumerator.Current = next;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
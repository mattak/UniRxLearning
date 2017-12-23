using System;

// 監視可能なものclass
public class Observable<TValue> : IObservable<TValue>
{
    private Func<IObserver<TValue>, IDisposable> creator;

    private Observable(Func<IObserver<TValue>, IDisposable> creator)
    {
        this.creator = creator;
    }

    public IDisposable Subscribe(IObserver<TValue> observer)
    {
        // Subscribeした瞬間に関数を実行するのがCold Observableの特徴
        return this.creator(observer);
    }

    // Observableを直接渡したくないので、Createメソッドを作っておく.
    public static IObservable<TValue> Create(Func<IObserver<TValue>, IDisposable> creator)
    {
        return new Observable<TValue>(creator);
    }
}
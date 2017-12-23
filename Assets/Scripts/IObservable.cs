using System;

// 監視可能であることを示す
public interface IObservable<TValue>
{
    // 監視者が購読する
    IDisposable Subscribe(IObserver<TValue> observer);
}
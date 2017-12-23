// 監視対象
public interface ISubject<TValue> : IObserver<TValue>, IObservable<TValue>
{
    // 3つのイベントは IObserverと同一のinterfaceなので委譲した
    // データを通知: NofityNext(value) => OnNext(value)
    // エラーを通知: NotifyError(value) => OnError(error)
    // データ終了を通知: NotifyComplete(value) => OnComplete()

    // 監視可能であることは IObservalbeに委譲した
    // IDisposable Subscribe(IObserver<TValue> observer);
}
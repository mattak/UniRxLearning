using System;

// Nextの値によって通知するかしないかを変更する Observable
public class WhereObservable<TNext> : IObservable<TNext>
{
    private Func<TNext, bool> operation;
    private IObservable<TNext> observable;

    public WhereObservable(IObservable<TNext> observable, Func<TNext, bool> operation)
    {
        this.operation = operation;
        this.observable = observable;
    }

    public IDisposable Subscribe(IObserver<TNext> observer)
    {
        var disposable = this.observable.Subscribe(Observer<TNext>.Create(
            next =>
            {
                // nextの値によって、次に処理を流すかどうかを決定. operationはboolを返却する
                if (this.operation(next)) observer.OnNext(next);
            },
            error => observer.OnError(error),
            () => observer.OnComplete()
        ));

        return disposable;
    }
}
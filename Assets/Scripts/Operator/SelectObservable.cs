using System;

// Nextの値を別の値に変更する Observable
public class SelectObservable<TNext1, TNext2> : IObservable<TNext2>
{
    private Func<TNext1, TNext2> operation;
    private IObservable<TNext1> observable;

    public SelectObservable(IObservable<TNext1> observable, Func<TNext1, TNext2> operation)
    {
        this.operation = operation;
        this.observable = observable;
    }

    public IDisposable Subscribe(IObserver<TNext2> observer)
    {
        var disposable = this.observable.Subscribe(Observer<TNext1>.Create(
            next =>
            {
                // nextの値をoperatorによって別の値に変更する
                var next2 = this.operation(next);
                observer.OnNext(next2);
            },
            error => observer.OnError(error),
            () => observer.OnComplete()
        ));

        return disposable;
    }
}
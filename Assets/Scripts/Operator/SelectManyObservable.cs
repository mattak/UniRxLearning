using System;
using System.Collections.Generic;

// 1つの値を受け取って、N個(0以上)の値を流す Observable
public class SelectManyObservable<TNext1, TNext2> : IObservable<TNext2>
{
    private Func<TNext1, IObservable<TNext2>> operation;
    private IObservable<TNext1> observable;

    public SelectManyObservable(IObservable<TNext1> observable, Func<TNext1, IObservable<TNext2>> operation)
    {
        this.observable = observable;
        this.operation = operation;
    }

    public IDisposable Subscribe(IObserver<TNext2> observer)
    {
        var disposables = new List<IDisposable>();
        var disposable1 = this.observable.Subscribe(Observer<TNext1>.Create(
            next1 =>
            {
                // nextの値を流すと、observableが帰ってくる. それを購読して次へ伝える
                var disposable2 = this.operation(next1).Subscribe(Observer<TNext2>.Create(
                    next2 => observer.OnNext(next2),
                    error => observer.OnError(error),
                    () => observer.OnComplete()
                ));
                disposables.Add(disposable2);
            },
            error => observer.OnError(error),
            () => observer.OnComplete()
        ));
        disposables.Add(disposable1);
        return new CollectionDisposable(disposables);
    }
}
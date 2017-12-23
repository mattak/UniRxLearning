using System;

// イベント関数をそのまま実行するだけの Observer
public class Observer<TNext> : IObserver<TNext>
{
    private Action<TNext> next;
    private Action<Exception> error;
    private Action complete;

    private Observer(Action<TNext> next, Action<Exception> error, Action complete)
    {
        this.next = next;
        this.error = error;
        this.complete = complete;
    }

    public void OnNext(TNext value)
    {
        this.next(value);
    }

    public void OnError(Exception error)
    {
        this.error(error);
    }

    public void OnComplete()
    {
        this.complete();
    }

    public static IObserver<TNext> Create(Action<TNext> next, Action<Exception> error, Action complete)
    {
        return new Observer<TNext>(next, error, complete);
    }
}

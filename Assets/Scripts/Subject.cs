using System;
using System.Collections.Generic;

public class Subject<TNext> : ISubject<TNext>
{
    private List<IObserver<TNext>> observers = new List<IObserver<TNext>>();

    public void OnNext(TNext next)
    {
        foreach (var observer in this.observers)
        {
            observer.OnNext(next);
        }
    }

    public void OnError(Exception error)
    {
        foreach (var observer in this.observers)
        {
            observer.OnError(error);
        }

        this.observers.Clear();
    }

    public void OnComplete()
    {
        foreach (var observer in this.observers)
        {
            observer.OnComplete();
        }

        this.observers.Clear();
    }

    public IDisposable Subscribe(IObserver<TNext> observer)
    {
        this.observers.Add(observer);
        // 購読管理のclassを返す
        return new Subscription(this, observer);
    }

    private void Unsubscribe(IObserver<TNext> observer)
    {
        this.observers.Remove(observer);
    }

    // 購読管理をするclass. Dispose()を呼ぶことで購読をやめる
    class Subscription : IDisposable
    {
        private IObserver<TNext> observer;
        private Subject<TNext> subject;

        public Subscription(Subject<TNext> subject, IObserver<TNext> observer)
        {
            this.subject = subject;
            this.observer = observer;
        }

        public void Dispose()
        {
            this.subject.Unsubscribe(this.observer);
        }
    }
}
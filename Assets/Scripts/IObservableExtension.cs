using System;
using System.Collections.Generic;

// IObserverをメソッドチェインするためのExtension
public static class IObservableExtension
{
    // 条件をしぼる Operator
    public static IObservable<TNext> Where<TNext>(this IObservable<TNext> observable, Func<TNext, bool> operation)
    {
        return new WhereObservable<TNext>(observable, operation);
    }

    // 別の値に変換する Operator
    public static IObservable<TNext2> Select<TNext1, TNext2>(
        this IObservable<TNext1> observable, Func<TNext1, TNext2> operation)
    {
        return new SelectObservable<TNext1, TNext2>(observable, operation);
    }

    // 値を1つ受け取って、IObservableに変換する Operator
    public static IObservable<TNext2> SelectMany<TNext1, TNext2>(
        this IObservable<TNext1> observable, Func<TNext1, IObservable<TNext2>> operation)
    {
        return new SelectManyObservable<TNext1, TNext2>(observable, operation);
    }

    // 値を1つ受け取って、複数の値に分割するして流す Operator
    public static IObservable<TNext2> SelectMany<TNext1, TNext2>(
        this IObservable<TNext1> observable, Func<TNext1, IEnumerable<TNext2>> operation)
    {
        return new SelectManyObservable<TNext1, TNext2>(observable, next1 =>
        {
            return Observable<TNext2>.Create(observer =>
            {
                var next2enumerable = operation(next1);

                foreach (var next2 in next2enumerable)
                {
                    observer.OnNext(next2);
                }

                return new EmptyDisposable();
            });
        });
    }

    // 面倒なので IObserverクラスをいちいち作らなくて良いようにする.
    public static IDisposable Subscribe<TNext>(
        this IObservable<TNext> observable,
        Action<TNext> next,
        Action<Exception> error,
        Action complete)
    {
        return observable.Subscribe(Observer<TNext>.Create(next, error, complete));
    }

    // next, errorのみでもOK
    public static IDisposable Subscribe<TNext>(
        this IObservable<TNext> observable,
        Action<TNext> next,
        Action<Exception> error)
    {
        return observable.Subscribe(next, error, () => { });
    }

    // nextのみでもOK
    public static IDisposable Subscribe<TNext>(
        this IObservable<TNext> observable,
        Action<TNext> next)
    {
        return observable.Subscribe(next, error => { }, () => { });
    }
}
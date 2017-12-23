using System;

// 監視者
public interface IObserver<TValue>
{
    // データがきた
    void OnNext(TValue value);

    // エラーが起きた
    void OnError(Exception error);

    // データはもう来ない
    void OnComplete();
}

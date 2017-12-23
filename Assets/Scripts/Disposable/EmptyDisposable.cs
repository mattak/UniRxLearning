using System;

// 購読解除するつもりがないときに返す Disposable
public class EmptyDisposable : IDisposable
{
    public void Dispose()
    {
    }
}
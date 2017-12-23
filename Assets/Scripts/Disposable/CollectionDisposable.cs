using System;
using System.Collections.Generic;

// 複数の講読をいっぺんに解除するための Disposable
public class CollectionDisposable : IDisposable
{
    private IList<IDisposable> disposables;

    public CollectionDisposable(IList<IDisposable> disposables)
    {
        this.disposables = this.disposables;
    }

    public void Dispose()
    {
        foreach (var disposable in this.disposables)
        {
            disposable.Dispose();
        }
    }
}
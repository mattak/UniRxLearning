using System;
using UnityEngine;

public class EveryUpdateObservable : IObservable<Unit>
{
    private Component component;

    public EveryUpdateObservable(Component component)
    {
        this.component = component;
    }

    public IDisposable Subscribe(IObserver<Unit> observer)
    {
        var updator = this.component.GetComponent<EveryUpdateComponent>();

        if (updator == null)
        {
            updator = this.component.gameObject.AddComponent<EveryUpdateComponent>();
        }

        updator.Observer = observer;
        return updator;
    }
}

public class EveryUpdateComponent : MonoBehaviour, IDisposable
{
    public IObserver<Unit> Observer { get; set; }

    private void Update()
    {
        // 毎frameでイベント送信する
        if (this.Observer != null)
        {
            this.Observer.OnNext(Unit.Default);
        }
    }

    private void OnDestroy()
    {
        this.Dispose();
    }

    public void Dispose()
    {
        if (this.Observer != null)
        {
            this.Observer.OnComplete();
        }
    }
}
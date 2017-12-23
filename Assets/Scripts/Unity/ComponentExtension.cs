using UnityEngine;

public static class ComponentExtension
{
    // フレームごとにイベント送信する
    public static IObservable<Unit> OnEveryUpdate(this Component component)
    {
        return new EveryUpdateObservable(component);
    }
}
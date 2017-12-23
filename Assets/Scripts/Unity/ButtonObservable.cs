using System;
using UnityEngine.UI;

// ボタンが押されたら、イベントを通知する Observable
public class ButtonObservable : IObservable<Unit>
{
    private Button button;
    
    public ButtonObservable(Button button)
    {
        this.button = button;
    }
    
    public IDisposable Subscribe(IObserver<Unit> observer)
    {
        this.button.onClick.AddListener(() =>
        {
            observer.OnNext(Unit.Default);
        });
        
        // 本当は講読管理をしっかり考えなければならない
        return new EmptyDisposable();
    }
}
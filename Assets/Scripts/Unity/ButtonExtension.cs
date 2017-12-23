using UnityEngine.UI;

public static class ButtonExtension
{
    // ボタンのOnClickを受けて、イベントを流す拡張関数
    public static IObservable<Unit> OnClickAsObservable(this Button button)
    {
        return new ButtonObservable(button);
    }
}
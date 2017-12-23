using NUnit.Framework;

public class ObserverTest
{
    [Test]
    public void ObserverPatternTest()
    {
        var subject = new SushiLaneSubject();
        var observer = new SushiObserver();

// 例: 1 => N変換
// まぐろをさばいて、大トロ, 中トロに変換する
        subject
            .Where(neta => neta == "まぐろ")
            .Select(neta => "トロ")
            .SelectMany(neta => new[] {"大" + neta, "中" + neta})
            .Subscribe(observer);

// 例: 1 => Observable変換
// ネタがきてもすべて無視する
        subject
            .SelectMany(neta => Observable<string>.Create(_observer =>
            {
                // _observer.OnNext(neta);
                return new EmptyDisposable();
            }))
            .Subscribe(observer);

        subject.OnNext("まぐろ");
        subject.OnComplete();

// output:
// 大トロおいしいです (^q^)
// 中トロおいしいです (^q^)
// ごちそうさま (^O^)
    }
}
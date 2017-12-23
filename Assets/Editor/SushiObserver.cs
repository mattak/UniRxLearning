using System;
using UnityEngine;

// 監視者 (寿司を食べる人)
public class SushiObserver : IObserver<string>
{
    public void OnNext(string neta)
    {
        Debug.LogFormat("{0}おいしいです (^q^)", neta);
    }

    public void OnError(Exception exception)
    {
        Debug.LogFormat("{0} (￣Д￣;)", exception.Message);
    }

    public void OnComplete()
    {
        Debug.Log("ごちそうさま (^O^)");
    }
}
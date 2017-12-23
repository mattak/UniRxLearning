using UnityEngine;

public class EveryUpdateExample : MonoBehaviour
{
    void Start()
    {
        // 10フレームごとにログを吐く
        this.OnEveryUpdate()
            .Select(_ => Time.frameCount)
            .Where(frame => frame % 10 == 0)
            .Subscribe(frame => Debug.LogFormat("{0} frame passed", frame));
    }
}
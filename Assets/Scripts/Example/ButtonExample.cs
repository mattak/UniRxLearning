using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonExample : MonoBehaviour
{
    void Start()
    {
        var button = this.GetComponent<Button>();
        
        button.OnClickAsObservable()
            .Subscribe(_ => Debug.Log("Clicked!"));
    }
}
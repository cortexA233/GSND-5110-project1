using KToolkit;
using TMPro;
using UnityEngine;


[KUI_Info("hint_ui", "HintUI")]
public class HintUI : KUIBase
{
    public override void InitParams(params object[] args)
    {
        base.InitParams(args);
        transform.Find("hint_text").GetComponent<TMP_Text>().text = args[0] as string;
        KTimerManager.instance.AddDelayTimerFunc((float)args[1], arg0 =>
        {
            DestroySelf();
        });
    }
}

using KToolkit;
using UnityEngine;
using UnityEngine.UI;


public class GameSuccessUI : KUIBase
{
    public override void InitParams(params object[] args)
    {
        base.InitParams(args);
        transform.Find("restart").GetComponent<Button>().onClick.AddListener((() =>
        {
            GameManager.instance.BackToMainMenu();
            DestroySelf();
        }));
    }
}

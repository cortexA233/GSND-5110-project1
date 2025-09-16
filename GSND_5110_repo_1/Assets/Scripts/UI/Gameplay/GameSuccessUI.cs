using KToolkit;
using UnityEngine;
using UnityEngine.UI;


[KUI_Info("game_success_ui", "GameSuccessUI")]
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

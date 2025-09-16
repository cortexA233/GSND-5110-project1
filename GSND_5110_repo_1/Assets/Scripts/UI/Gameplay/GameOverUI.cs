using KToolkit;
using UnityEngine;
using UnityEngine.UI;


[KUI_Info("game_over_ui", "GameOverUI")]
public class GameOverUI : KUIBase
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

using KToolkit;
using UnityEngine;
using UnityEngine.UI;


[KUI_Info("main_menu", "MainMenuUI")]
public class MainMenuUI : KUIBase
{
    public override void InitParams(params object[] args)
    {
        base.InitParams(args);
        transform.Find("start_btn").GetComponent<Button>().onClick.AddListener((() =>
        {
            GameManager.instance.StartNewGame(GameManager.instance.GetGameConfig().mainGamePairCount);
            DestroySelf();
        }));
        transform.Find("quit_btn").GetComponent<Button>().onClick.AddListener((() =>
        {
            Application.Quit();
        }));
    }
}

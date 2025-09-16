using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KToolkit;


[KUI_Info("GM/gm_page", "GMPage")]
public class GMPage : KUIBase
{
    Dictionary<string, UnityAction> allFuncs = new Dictionary<string, UnityAction>();
    private string inputArg_1;
    
    public override void OnStart()
    {
        base.OnStart();
        InitAllFuncs();
        GenerateAllFuncs();
        transform.Find("input_arg_1").GetComponent<TMP_InputField>().onValueChanged.AddListener(argStr =>
        {
            inputArg_1 = argStr;
        });
        transform.Find("quit").GetComponent<Button>().onClick.AddListener((() =>
        {
            KUIManager.instance.DestroyUI(this);
        }));
    }
    
    void InitAllFuncs()
    {
        allFuncs.Add("print some info", () =>
        {
            int testNum = 114514;
            KDebugLogger.Example_DebugLog("print", inputArg_1, testNum);
        });
        
        allFuncs.Add("print UI list", () =>
        {
            string res = "";
            foreach (var item in KUIManager.instance.DebugGetUIList())
            {
                res += item.ToString() + "  ";
            }
            KDebugLogger.Example_DebugLog(res);
        });
        
        allFuncs.Add("start new game", () =>
        {
            int.TryParse(inputArg_1, out int pairCount);
            if (pairCount <= 0)
            {
                pairCount = 5;
            }
            GameManager.instance.StartNewGame(pairCount);
        });
        
        allFuncs.Add("print main puzzle answer", () =>
        {
            MainPuzzleUI puzzle = KUIManager.instance.GetFirstUIWithType<MainPuzzleUI>();
            puzzle.PrintAnswers();
        });
        
        allFuncs.Add("hide countdown", () =>
        {
            KEventManager.SendNotification(KEventName.ShowCountDownTextNotification, false);
        });
        
        allFuncs.Add("show countdown", () =>
        {
            KEventManager.SendNotification(KEventName.ShowCountDownTextNotification, true);
        });
        
        allFuncs.Add("open ballon game", () =>
        {
            GameManager.instance.StartBallonMiniGame();
        });
        
        allFuncs.Add("Add Extra time", () =>
        {
            CountDownManager.instance.ChangeCountDownTime(5f);
        });
    }

    void GenerateAllFuncs()
    {
        foreach (var item in allFuncs)
        {
            var button = (Resources.Load<GameObject>("UI_prefabs/GM/gm_button"));
            button = GameObject.Instantiate(button, transform.Find("Scroll View/Viewport/Content"));
            button.transform.Find("title").GetComponent<Text>().text = item.Key;
            button.GetComponent<Button>().onClick.AddListener(item.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using KToolkit;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;


[KUI_Info("main_puzzle/main_puzzle_ui", "MainPuzzleUI")]
public class MainPuzzleUI : KUIBase
{
    private int pairCount;
    private Transform nodeRoot;
    private Transform lineRoot;
    TMP_Text errorCountText;
    TMP_Text countDownText;
    
    int errorCount;
    List<bool> checkList = new List<bool>();
    List<int> answersList = new List<int>();
    List<MainPuzzleCell> puzzleCells = new List<MainPuzzleCell>();
    private Dictionary<int, GameObject> lineDict = new Dictionary<int, GameObject>();
    private int currentLeftIndex = -1;
    
    public override void InitParams(params object[] args)
    {
        base.InitParams(args);
        pairCount = (int)args[0];
        
        nodeRoot = transform.Find("node_pair_root");
        lineRoot = transform.Find("line_root");
        errorCountText = transform.Find("error_count").GetComponent<TMP_Text>();
        countDownText = transform.Find("countdown_text").GetComponent<TMP_Text>();
        for (int i = 0; i < pairCount; i++)
        {
            answersList.Add(i);
            checkList.Add(false);
        }

        for (int i = 0; i < answersList.Count; i++)
        {
            int lastIndex = answersList.Count - i - 1;
            int randIndex = Random.Range(0, lastIndex);
            (answersList[randIndex], answersList[lastIndex]) = (answersList[lastIndex], answersList[randIndex]);
        }

        for (int i = 0; i < pairCount; i++)
        {
            puzzleCells.Add(CreateUICell<MainPuzzleCell>(nodeRoot, i));
        }
        RefreshAnswersCount();
        
        transform.Find("node_pair_root").GetComponent<Button>().onClick.AddListener((() =>
        {
            KEventManager.SendNotification(KEventName.MainPuzzleCancelConnection);
        }));
    }

    public override void OnStart()
    {
        base.OnStart();
        
        AddEventListener(KEventName.MainPuzzleBeginConnection, args =>
        {
            currentLeftIndex = (int)args[0];
            KDebugLogger.Cortex_DebugLog("begin connect", currentLeftIndex);
        });
        AddEventListener(KEventName.MainPuzzleEndConnection, args =>
        {
            if (currentLeftIndex >= 0)
            {
                KDebugLogger.Cortex_DebugLog("end connect", (int)args[0]);
                var newPair = new Tuple<int, int>(currentLeftIndex, (int)args[0]);
                if (answersList[newPair.Item1] == newPair.Item2)
                {
                    checkList[newPair.Item1] = true;
                }
                else
                {
                    checkList[newPair.Item1] = false;
                }

                if (lineDict.ContainsKey(newPair.Item1))
                {
                    GameObject.Destroy(lineDict[newPair.Item1]);
                    lineDict.Remove(newPair.Item1);
                }
                lineDict[newPair.Item1] = DrawLine(currentLeftIndex, (int)args[0]);
                RefreshAnswersCount();
                currentLeftIndex = -1;
            }
        });
        AddEventListener(KEventName.MainPuzzleCancelConnection, args =>
        {
            currentLeftIndex = -1;
        });
        AddEventListener(KEventName.ShowCountDownTextNotification, args =>
        {
            countDownText.gameObject.SetActive((bool)args[0]);
        });
        AddEventListener(KEventName.ShowMainPuzzle, args =>
        {
            gameObject.SetActive((bool)args[0]);
        });
    }

    public override void Update()
    {
        base.Update();
        SetCountDownText();
    }

    void RefreshAnswersCount()
    {
        errorCount = pairCount;
        foreach (var item in checkList)
        {
            if (item)
            {
                errorCount--;
            }
        }
        errorCountText.text = errorCount + " ERRORS LEFT";
        if (errorCount <= 0)
        {
            // todo: victory logic
            KEventManager.SendNotification(KEventName.AllConnectionSucceed);
        }
    }
    
    void SetCountDownText()
    {
        float displayTime = CountDownManager.instance.currentCountDownTime;
        int timeInteger = (int)(displayTime);
        float timeFloat = displayTime - timeInteger;
        var tempTimeFloat = timeFloat.ToString("F2").Split('.')[1];
        countDownText.text = string.Format("{0} : {1}", timeInteger.ToString("00"), tempTimeFloat);
    }

    GameObject DrawLine(int startIndex, int endIndex)
    {
        GameObject line = new GameObject("line_" + startIndex + "_" + endIndex, typeof(Image));
        line.transform.SetParent(lineRoot, false);
        Image img = line.GetComponent<Image>();
        img.color = Color.red; // line color

        RectTransform rt = line.GetComponent<RectTransform>();
        Vector3 start = nodeRoot.GetChild(startIndex).Find("left_node").position;
        Vector3 end = nodeRoot.GetChild(endIndex).Find("right_node").position;
        Vector3 dir = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        rt.sizeDelta = new Vector2(distance, 5f); // line width = 5
        rt.pivot = new Vector2(0, 0.5f); // start point is left
        rt.position = start;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rt.rotation = Quaternion.Euler(0, 0, angle);
        return line;
    }

    public void PrintAnswers()
    {
        string ansStr = "";
        foreach (var item in answersList)
        {
            ansStr += item + " ";
        }
        KDebugLogger.Cortex_DebugLog("answer is:", ansStr);
    }

}

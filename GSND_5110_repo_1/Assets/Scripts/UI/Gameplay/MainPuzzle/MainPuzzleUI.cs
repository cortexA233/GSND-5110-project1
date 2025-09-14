using System;
using System.Collections.Generic;
using UnityEngine;
using KToolkit;
using UnityEngine.UI;
using Random = UnityEngine.Random;


[KUI_Info("main_puzzle/main_puzzle_ui", "MainPuzzleUI")]
public class MainPuzzleUI : KUIBase
{
    private int pairCount;
    private Transform nodeRoot;
    private Transform lineRoot;
    
    int errorCount;
    List<bool> checkList = new List<bool>();
    List<MainPuzzleCell> puzzleCells = new List<MainPuzzleCell>();
    private Dictionary<Tuple<int, int>, GameObject> lineDict;
    
    public override void InitParams(params object[] args)
    {
        base.InitParams(args);
        pairCount = (int)args[0];
        
        AddEventListener(KEventName.MainPuzzleBeginConnection, args =>
        {
            
        });
        AddEventListener(KEventName.MainPuzzleEndConnection, args =>
        {
            // if()
        });
        
        nodeRoot = transform.Find("node_pair_root");
        lineRoot = transform.Find("line_root");
        List<int> answersList = new List<int>();
        for (int i = 0; i < pairCount; i++)
        {
            answersList.Add(i);
        }

        for (int i = 0; i < answersList.Count; i++)
        {
            int lastIndex = answersList.Count - i - 1;
            int randIndex = Random.Range(0, lastIndex);
            // int temp = answersList[randIndex];
            // answersList[randIndex] = answersList[lastIndex];
            // answersList[lastIndex] = temp;
            (answersList[randIndex], answersList[lastIndex]) = (answersList[lastIndex], answersList[randIndex]);
        }

        for (int i = 0; i < pairCount; i++)
        {
            KDebugLogger.Cortex_DebugLog("ans is: ", answersList[i]);
            puzzleCells.Add(CreateUICell<MainPuzzleCell>(nodeRoot, answersList[i]));
        }
    }
    
    void DrawLine(int startIndex, int endIndex)
    {
        GameObject line = new GameObject("line_" + startIndex + "_" + endIndex, typeof(Image));
        line.transform.SetParent(lineRoot, false);
        Image img = line.GetComponent<Image>();
        img.color = Color.white; // 可修改颜色

        RectTransform rt = line.GetComponent<RectTransform>();
        Vector3 start = lineRoot.GetChild(startIndex).position;
        Vector3 end = lineRoot.GetChild(endIndex).position;
        Vector3 dir = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        rt.sizeDelta = new Vector2(distance, 5f); // 线的宽度 = 5
        rt.pivot = new Vector2(0, 0.5f); // 让左边当作起点
        rt.position = start;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rt.rotation = Quaternion.Euler(0, 0, angle);
    }

}

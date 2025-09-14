using UnityEngine;
using KToolkit;
using UnityEngine.UI;


[KUI_Cell_Info("main_puzzle/node_pair", "MainPuzzleCell")]
public class MainPuzzleCell : KUICell
{
    private int nodeIndex;
    public override void OnCreate(params object[] args)
    {
        nodeIndex = (int)args[0];
        transform.Find("left_node").GetComponent<Button>().onClick.AddListener((() =>
        {
            KEventManager.SendNotification(KEventName.MainPuzzleBeginConnection, nodeIndex);
        }));
        transform.Find("right_node").GetComponent<Button>().onClick.AddListener((() =>
        {
            KEventManager.SendNotification(KEventName.MainPuzzleEndConnection, nodeIndex);
        }));
    }
}

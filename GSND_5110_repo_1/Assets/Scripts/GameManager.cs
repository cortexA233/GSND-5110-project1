using UnityEngine;
using KToolkit;

public class GameManager : KSingleton<GameManager>
{
    bool isGameStarted = false;
    protected override void Awake()
    {
        base.Awake();
        KFrameworkManager.instance.InitKFramework();
        AddEventListener(KEventName.KToolTestEvent, args =>
        {
            print("!!!open mini game");
        });
        
        AddEventListener(KEventName.CountDownEnd, args =>
        {
            EndCurrentGame(false);
        });
        AddEventListener(KEventName.AllConnectionSucceed, args =>
        {
            EndCurrentGame(true);
        });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (KUIManager.instance.GetFirstUIWithType<GMPage>() is null) 
            {
                KUIManager.instance.CreateUI<GMPage>();   
            }
            else
            {
                KUIManager.instance.DestroyAllUIWithType<GMPage>();
            }
        }
        #endif
        CountDownManager.instance.UpdateCountDown();
    }

    #region public

    public void StartNewGame(int pairCount=8)
    {
        CountDownManager.instance.StartCountDown();
        KUIManager.instance.CreateUI<MainPuzzleUI>(pairCount);
    }

    public void EndCurrentGame(bool isSuccess=false)
    {
        // todo
        KUIManager.instance.DestroyAllUIWithType<MainPuzzleUI>();
    }

    public SO_GameConfig GetGameConfig()
    {
        return Resources.Load<SO_GameConfig>("game_config");
    }

    #endregion
}

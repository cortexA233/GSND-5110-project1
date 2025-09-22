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
        EnablePostProcess(true);
        CountDownManager.instance.StartCountDown();
        KUIManager.instance.CreateUI<MainPuzzleUI>(pairCount);
    }

    public void StartBallonMiniGame(){
        Instantiate(Resources.Load<GameObject>("Balloon_game_prefab/mini_game"));
    }

    public void EndCurrentGame(bool isSuccess=false)
    {
        // todo
        EnablePostProcess(false);
        if (isSuccess)
        {
            KUIManager.instance.CreateUI<GameSuccessUI>();
        }
        else
        {
            KUIManager.instance.CreateUI<GameOverUI>();
        }
        KUIManager.instance.DestroyAllUIWithType<MainPuzzleUI>();
    }

    public void BackToMainMenu()
    {
        // todo
    }

    public SO_GameConfig GetGameConfig()
    {
        return Resources.Load<SO_GameConfig>("game_config");
    }

    public void EnablePostProcess(bool enable)
    {
        Camera.main.GetComponent<CameraFilterPack_FX_Glitch2>().enabled = enable;
        Camera.main.GetComponent<CameraFilterPack_NewGlitch4>().enabled = enable;
        // Camera.main.GetComponent<CameraFilterPack_NewGlitch3>().enabled = enable;
    }

    public void SetPostProcessValue(float value)
    {
        Camera.main.GetComponent<CameraFilterPack_FX_Glitch2>().Glitch = value;
        Camera.main.GetComponent<CameraFilterPack_NewGlitch4>().__Speed = value;
        // Camera.main.GetComponent<CameraFilterPack_NewGlitch3>().__Speed = value;
        Camera.main.GetComponent<CameraFilterPack_NewGlitch4>()._Fade = value;
        // Camera.main.GetComponent<CameraFilterPack_NewGlitch3>()._RedFade = value;
    }

    #endregion
}

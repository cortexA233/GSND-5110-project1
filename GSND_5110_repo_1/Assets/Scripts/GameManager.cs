using UnityEngine;
using KToolkit;

public class GameManager : KSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        KFrameworkManager.instance.InitKFramework();
        AddEventListener(KEventName.KToolTestEvent, args =>
        {
            print("!!!open mini game");
        });
        // GameManager.instance.OpenMinigame(param1, param2);;
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
    }
}

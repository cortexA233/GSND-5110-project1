using UnityEngine;
using KToolkit;

public class GameManager : KSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        KFrameworkManager.instance.InitKFramework();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

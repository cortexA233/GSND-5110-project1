using KToolkit;
using UnityEngine;


public class CountDownManager : KSingletonNoMono<CountDownManager>
{
    public float currentCountDownTime { get; private set; } = 0;
    private bool isPause = true;
    private float minigameStartTimer;
    
    public CountDownManager()
    {
    }

    public void StartCountDown()
    {
        minigameStartTimer = GameManager.instance.GetGameConfig().minigameTriggerFrequency;
        currentCountDownTime = GameManager.instance.GetGameConfig().countDownTime;
        isPause = false;
    }

    public void StopCountDown()
    {
        isPause = true;
    }

    public void UpdateCountDown()
    {
        if (isPause)
        {
            return;
        }
        
        if (currentCountDownTime > 0)
        {
            currentCountDownTime -= Time.deltaTime;
            minigameStartTimer -= Time.deltaTime;
        }
        else
        {
            isPause = true;
            currentCountDownTime = 0;
            KEventManager.SendNotification(KEventName.CountDownEnd);
        }

        if (minigameStartTimer <= 0)
        {
            minigameStartTimer = GameManager.instance.GetGameConfig().minigameTriggerFrequency;
            KEventManager.SendNotification(KEventName.ShowMainPuzzle, false);
            GameManager.instance.StartBallonMiniGame();
        }
    }
    
    public void ChangeCountDownTime(float time)
    {
        currentCountDownTime += time;
    }
}

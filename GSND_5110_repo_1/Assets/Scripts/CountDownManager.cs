using KToolkit;
using UnityEngine;


public class CountDownManager : KSingletonNoMono<CountDownManager>
{
    public float currentCountDownTime { get; private set; } = 0;
    private bool isPause = true;
    
    public CountDownManager()
    {
    }

    public void StartCountDown()
    {
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
        }
        else
        {
            isPause = true;
            currentCountDownTime = 0;
            KEventManager.SendNotification(KEventName.CountDownEnd);
        }
    }
    
    public void ChangeCountDownTime(float time)
    {
        currentCountDownTime += time;
    }
}

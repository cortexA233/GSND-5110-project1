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
        AudioManager.instance.PlayBGM("Audio/bomb_tick");
        minigameStartTimer = GameManager.instance.GetGameConfig().minigameTriggerFrequency;
        currentCountDownTime = GameManager.instance.GetGameConfig().countDownTime;
        isPause = false;
    }

    public void StopCountDown()
    {
        AudioManager.instance.StopBGM();
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
            // KEventManager.SendNotification(KEventName.ShowMainPuzzle, false);
            GameManager.instance.StartBallonMiniGame();
        }
        SetPostProcessByCountDownTime();
        SetAudioSpeedByCountDownTime();
    }

    void SetPostProcessByCountDownTime()
    {
        float countDownMaxLimit = GameManager.instance.GetGameConfig().countDownTime;
        float glitchValue = (countDownMaxLimit - currentCountDownTime) / countDownMaxLimit * 0.7f + 0.01f;
        GameManager.instance.SetPostProcessValue(glitchValue);
    }

    void SetAudioSpeedByCountDownTime()
    {
        AudioSource tickSource = AudioManager.instance.GetValidAudioSourceByAudioName("bomb_tick");
        // ;
        if (tickSource)
        {
            float countDownMaxLimit = GameManager.instance.GetGameConfig().countDownTime;
            float pitchValue = Mathf.Clamp(countDownMaxLimit / currentCountDownTime * 0.8f, 1f, 3f);
            tickSource.pitch = pitchValue;
        }
    }
    
    public void ChangeCountDownTime(float time)
    {
        currentCountDownTime += time;
    }
}

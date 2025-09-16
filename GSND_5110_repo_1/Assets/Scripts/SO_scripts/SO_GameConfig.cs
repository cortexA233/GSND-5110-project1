using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class SO_GameConfig : ScriptableObject
{
    [Header("countdown time / 倒计时时间")]
    public int countDownTime;

    [Header("minigame score to time ratio / 小游戏分数换算时间比例")]
    public float scoreToExtraTimeRatio;

    [Header("minigame time / 小游戏持续时间")]
    public float minigameCountdownTime;

    [Header("minigame frequency / 小游戏触发频率")]
    public float minigameTriggerFrequency;
}

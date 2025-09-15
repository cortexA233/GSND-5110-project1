using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class SO_GameConfig : ScriptableObject
{
    [Header("countdown time / 倒计时时间")]
    public int countDownTime;
}

using KToolkit;
using UnityEngine;

public class KtoolExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            KEventManager.SendNotification(KEventName.KToolTestEvent);
        }
    }
}

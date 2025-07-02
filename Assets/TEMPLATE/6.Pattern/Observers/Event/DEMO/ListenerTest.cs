using MyBox;
using UnityEngine;

public class ListenerTest : MonoBehaviour
{
    void Start()
    {
        MonsterEventManager.TempEvent.AddListener(this, onTempEvent);
    }

    private void onTempEvent(TempData data)
    {
        Bug.Log("EVENT REPORT: ".Colored(Color.yellow) + $"temp int:{data.tempInt} || temptFloat{data.tempFloat} || tempString {data.tempString}");
    }
};

using MyBox;
using UnityEngine;

public class PosterTest : MonoBehaviour
{
    [SerializeField] TempData data;

    [ButtonMethod]
    void PostEvent()
    {
        MonsterEventManager.TempEvent.Post(this, data);
    }
}

using MyBox;
using UnityEngine;

public class PosterTest : MonoBehaviour
{
    [SerializeField] TempData data;

    [ButtonMethod]
    void PostEvent()
    {
        TemplateEventManager.TempEvent.Post(this, data);
    }
}

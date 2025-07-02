using MyBox;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(VerticalLayoutGroup))]
public class PushNotificationManager : MonoBehaviour
{
    [InitializationField, SerializeField] public PushType PushType = PushType.FromTop;
    [SerializeField] VerticalLayoutGroup layout;
    [SerializeField] PushNotification prefab;
    [SerializeField] Transform notiRoot;
    [Separator("CONFIG")]
    [Tooltip("Time to this notification to animate show. If = 0, this will show immidiately")]
    [InitializationField, SerializeField] float showDuration = 0.1f;

    [Tooltip("Time to this notification to stay visible before hide")]
    [InitializationField, SerializeField] float visibleDuration = 3f;

    [Tooltip("Time to this notification to animate hide")]
    [InitializationField, SerializeField] float hideDuration = 0.15f;

    [Tooltip("Height of noti")]
    [InitializationField, SerializeField] float height = 100;

    [SerializeField] int MaxNotiCount = 3;

    Queue<PushNotification> pushNotifications = new Queue<PushNotification>();

    private void OnValidate()
    {
        layout = GetComponent<VerticalLayoutGroup>();
        notiRoot = this.transform;
        switch (PushType)
        {
            case PushType.FromTop:
                layout.childAlignment = TextAnchor.UpperCenter;
                break;
            case PushType.FromBottom:
                layout.childAlignment = TextAnchor.LowerCenter;
                break;
        }
    }

    public void Init()
    {
        for (int i = 0; i < MaxNotiCount; i++)
        {
            //create new object
            var newNoti = Instantiate(prefab, notiRoot);

            //init setting
            newNoti.Init(showDuration, visibleDuration, hideDuration, height);

            //hide it from start
            newNoti.gameObject.SetActive(false);

            pushNotifications.Enqueue(newNoti);
        }
    }

    private PushNotification Reuse()
    {
        var result = pushNotifications.Dequeue();
        pushNotifications.Enqueue(result);
        return result;
    }

    public void PushNewNotification(string message)
    {
        var noti = Reuse();
        noti.Show(message);
    }
}
public enum PushType
{
    FromTop = 0,
    FromBottom = 1,
}
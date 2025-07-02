using Template.Utilities;
using TMPro;
using UnityEngine;

public class TimeArea : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;

    public void SetTime(int totalSeconds)
    {
        timeText.text = totalSeconds.ToTimeFormat();
    }
}

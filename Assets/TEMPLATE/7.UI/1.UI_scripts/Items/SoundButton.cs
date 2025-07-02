using Monster.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Monster.UI
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] SoundID soundID;
        [SerializeField] bool hasVibrate = true;
        Button soundButton;
        private void Awake()
        {
            soundButton = GetComponent<Button>();
        }
        private void OnEnable()
        {
            soundButton.onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            GameService.PlaySound(soundID);
            if (hasVibrate)
            {
                GameService.PlayVibrate();
            }
        }
        private void OnDisable()
        {
            soundButton.onClick.RemoveListener(PlaySound);
        }
    }
}
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime;
    private float timer = 0f;
    private int frameCount = 0;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        frameCount++;
        timer += Time.unscaledDeltaTime;

        if (timer > 0.5f) // Update FPS every 2 seconds
        {
            float fps = frameCount / timer;
            fpsText.text = "FPS: " + Mathf.Round(fps);

            // Reset timer and frame count
            timer = 0f;
            frameCount = 0;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] float duration = 1f;
    private int index;
    private bool IsDone;
    [SerializeField] bool IsLoop = false;
    Action OnEnd;

    WaitForSeconds waitInterval;
    private void Start()
    {
        waitInterval = new WaitForSeconds(duration/(float)sprites.Count);
    }
    public void Play(Action OnEnd = null, bool loop = false)
    {
        IsLoop = loop;
        this.OnEnd = OnEnd;
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        IsDone = false;
        if(IsLoop)
        {
            while (true)
            {
                yield return waitInterval;
                index++;
                if (index >= sprites.Count)
                {
                    index = 0;
                    OnEnd?.Invoke();
                }
                else
                {
                    image.sprite = sprites[index];
                }
            }
        }
        else
        {
            index = 0;
            image.sprite = sprites[index];
            while(!IsDone)
            {
                yield return waitInterval;
                index++;
                if (index >= sprites.Count)
                {
                    IsDone = true;
                    this.gameObject.SetActive(false);
                    OnEnd?.Invoke();
                }
                else
                {
                    image.sprite = sprites[index];
                }
            }
        }
    }
}

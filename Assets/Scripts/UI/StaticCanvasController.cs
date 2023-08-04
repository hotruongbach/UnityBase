using Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCanvasController : MonoBehaviour
{

    private void OnEnable()
    {
       
    }
    private void OnDisable()
    {
       
    }

}
public class StaticCanvas: SingletonMonoBehaviour<StaticCanvasController>
{

}
using Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    //Data
    public DataSkin data;
    int[] arraySkin;
    int[] arraySkinDrop;
    int[] arrayVideo;

    [SerializeField] SkinnedMeshRenderer model;
    [SerializeField] SkinnedMeshRenderer playerSkin;
    [SerializeField] MeshRenderer carpetSkin;

    // Start is called before the first frame update
    void Start()
    {
        arrayVideo = new int[PlayerPrefsManager.ListAmountVideoSkin.Length];
        arraySkinDrop = new int[PlayerPrefsManager.ListAmountVideoSkin.Length];
        arraySkin = new int[PlayerPrefsManager.ListAmountVideoSkin.Length];
        for (int i = 0; i < PlayerPrefsManager.ListAmountVideoSkin.Length; i++)
        {
            arrayVideo[i] = PlayerPrefsManager.ListAmountVideoSkin[i];
            arraySkin[i] = PlayerPrefsManager.ListSkin[i];
            arraySkinDrop[i] = PlayerPrefsManager.SkinDrop[i];
        }
        ChangeSkin(PlayerPrefsManager.SkinUsing);
        ChangeModelSkinShop(PlayerPrefsManager.SkinUsing);
    }

    public void ChangeModelSkinShop(int id)
    {
        model.material = data.lstSkin[id].material;
    }
    public void ChangeSkin(int id)
    {
        playerSkin.material = data.lstSkin[id].material;
        carpetSkin.material = data.lstSkin[id].mCarpet;
    }
    public void UnlockSkin(int idSkin)
    {
        PlayerPrefsManager.SkinUsing = idSkin;
        arraySkin[idSkin] = 1;
        PlayerPrefsManager.ListSkin = arraySkin;
    }
    public void ChangeAmoutVideo(int idSkin)
    {
        arrayVideo[idSkin] += 1;
        PlayerPrefsManager.ListAmountVideoSkin = arrayVideo;
    }
    
    public void SkinDrop(int idSkin)
    {
        arraySkinDrop[idSkin] = 1;
        PlayerPrefsManager.SkinDrop = arraySkinDrop;
    }
}
public class SkinControl : SingletonMonoBehaviour<SkinController>
{

}

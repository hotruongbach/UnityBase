using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skin 
{
    public bool isUnlock;
    public bool onlyVideo;
    public Sprite spriteSkin;
    public Material material;
    public Material mCarpet;
    public float Price;
    public int amountVideo;
}
[CreateAssetMenu(fileName = "DataSkin", menuName = "Data/DataSkin")]
public class DataSkin : SingletonScriptableObject<DataSkin>
{
    public List<Skin> lstSkin = new List<Skin>();
    private void OnEnable()
    {
        if (PlayerPrefsManager.ListSkin == null || PlayerPrefsManager.ListSkin.Length == 0)
        {
            SetData();
        }
    }
    public void SetData()
    {
        int[] dataUnlock = new int[lstSkin.Count]; 
        int[] dataAmountVideoWatched = new int[lstSkin.Count]; 
        int[] dataSkinDrop = new int[lstSkin.Count]; 
        for (int i = 0; i < lstSkin.Count; i++)
        {
            dataAmountVideoWatched[i] = 0;
            if (lstSkin[i].isUnlock == true)
            {
                dataUnlock[i] = 1;
            }
            if (lstSkin[i].Price == 0)
            {
                dataSkinDrop[i] = 1;
            }
            else dataUnlock[i] = 0;
        }
        PlayerPrefsManager.ListSkin = dataUnlock;
        PlayerPrefsManager.SkinDrop = dataSkinDrop;
        PlayerPrefsManager.ListAmountVideoSkin = dataAmountVideoWatched;
    }
}

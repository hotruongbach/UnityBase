using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpriteLibrary", menuName = "GAME/SO/SpriteLibrary")]
public class SpriteLibrary : ScriptableObject
{
    public List<ResourceSpriteHolder> ResourceSpriteHolders = new List<ResourceSpriteHolder>();

    public Sprite GetSprite(ResourceType type)
    {
        return ResourceSpriteHolders.Find(x => x.Type == type).Sprite;
    }

    private void OnValidate()
    {
        foreach (var data in ResourceSpriteHolders)
        {
            data.Name = data.ToString();
        }
    }
}

[Serializable]
public record ResourceSpriteHolder
{
    [HideInInspector] public string Name;
    public int ID;
    public ResourceType Type;
    public Sprite Sprite;
    public override string ToString()
    {
        string result = "";
        result = $"{ID} : {Sprite.name}";
        return result;
    }
}

using System;

[Serializable]
public class ResourceData
{
    public ResourceType ResourceType;
    public int ResourceValue;

    public ResourceData(ResourceType resourceType, int value)
    {
        this.ResourceType = resourceType;
        this.ResourceValue = value;
    }
}

using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] FloatingType type;
    Vector3 camPos;
    Vector3 offset => type == FloatingType.Text ? gameConfig.billboardTextOffset : gameConfig.billboardSpriteOffset;
    Transform cam;
    Transform _transform;
    private void Awake()
    {
        camPos = Camera.main.transform.position;
        cam = Camera.main.transform;
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
        // Get the direction from the object to the camera
        Vector3 directionToCamera = camPos - _transform.position;

        // Make the object face the camera
        transform.rotation = Quaternion.LookRotation(directionToCamera + offset, cam.up);
    }
    public enum FloatingType
    {
        Text = 0,
        Sprite = 1
    }
}

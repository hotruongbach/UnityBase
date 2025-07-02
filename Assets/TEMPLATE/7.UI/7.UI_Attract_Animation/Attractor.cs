using Monster;
using UnityEngine;
using UnityEngine.Playables;

public class Attractor : MonoBehaviour
{
    [HideInInspector] public Vector3 Position;
    public Vector3 CurrentPosition => transform.position;
    [SerializeField] PlayableDirector director;
    private void Awake()
    {
        Position = transform.position;
    }
    public void OnAttractCompleted()
    {
        if(director != null)
        {
            director?.Stop();
            director?.Play();
        }
        //GameService.PlaySound(Monster.Audio.SoundID.Ding);
    }
}

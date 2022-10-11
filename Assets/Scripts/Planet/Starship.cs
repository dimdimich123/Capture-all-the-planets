using UnityEngine;
using Pathfinding;

/// <summary>
/// Configuring logic and visual component starships.
/// </summary>

[RequireComponent(typeof(AIDestinationSetter))]
public sealed class Starship : MonoBehaviour
{
    private AIDestinationSetter _aiDistanation;
    private PlanetController _target;
    private SpriteRenderer _sprite;
    private PlanetState _state;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    [System.NonSerialized] public Starship Next;
    [System.NonSerialized] public StarshipObjectPool ObjectPool;

    private void Awake()
    {
        _aiDistanation = GetComponentInChildren<AIDestinationSetter>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(Transform target, Color color, PlanetState state, AudioSource sound)
    {
        _aiDistanation.target = target;
        _target = target.GetComponent<PlanetController>();
        _sprite.color = color;
        _state = state;
        _audioSource = sound;
        _audioClip = sound.clip;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _target.gameObject)
        {
            _target.ChangeShipCount(_state);
            ObjectPool.ConfigureDeactivatedStarship(this);
            gameObject.SetActive(false);
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}

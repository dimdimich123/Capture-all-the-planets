using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
public class Starship : MonoBehaviour
{
    private AIDestinationSetter _aiDistanation;
    private PlanetController _target;
    private SpriteRenderer _sprite;
    private PlanetState _state;

    [System.NonSerialized] public Starship Next;
    [System.NonSerialized] public StarshipObjectPool ObjectPool;

    private void Awake()
    {
        _aiDistanation = GetComponentInChildren<AIDestinationSetter>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(Transform target, Color color, PlanetState state)
    {
        _aiDistanation.target = target;
        _target = target.GetComponent<PlanetController>();
        _sprite.color = color;
        _state = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _target.gameObject)
        {
            _target.ChangeShipCount(_state);
            ObjectPool.ConfigureDeactivatedStarship(this);
            gameObject.SetActive(false);
        }
    }
}

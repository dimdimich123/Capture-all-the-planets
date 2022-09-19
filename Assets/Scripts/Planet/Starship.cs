using UnityEngine;
using Pathfinding;

public class Starship : MonoBehaviour
{
    private AIDestinationSetter _target;
    private SpriteRenderer _sprite;

    [System.NonSerialized] public Starship Next;
    [System.NonSerialized] public StarshipObjectPool ObjectPool;

    private void Awake()
    {
        _target = GetComponent<AIDestinationSetter>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(Transform target, Color color)
    {
        _target.target = target;
        _sprite.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _target.target.gameObject)
        {
            ObjectPool.ConfigureDeactivatedStarship(this);
            gameObject.SetActive(false);
        }
    }
}

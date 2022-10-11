using System.Collections.Generic;
using UnityEngine;

public sealed class StarshipObjectPool
{
    private const int _startCount = 50;
    private readonly Starship _prefab;
    private readonly Transform _transform;
    private readonly float _minSpawnDistance;
    private readonly List<Starship> _starShips = new List<Starship>(_startCount);
    private AudioSource _shipSound;

    private Starship _firstAvailable = null;

    public StarshipObjectPool(Starship prefab, Transform transform)
    {
        _prefab = prefab;
        _transform = transform;
        _minSpawnDistance = transform.localScale.x / 2;
    }

    public void Init(AudioSource shipSound)
    {
        _shipSound = shipSound;

        for (int i = 0; i < _startCount; ++i)
        {
            GenerateStarship();
        }

        _firstAvailable = _starShips[0];
        
        for(int i = 0; i < _startCount - 1; ++i)
        {
            _starShips[i].Next = _starShips[i + 1];
        }

        _starShips[_startCount - 1].Next = null;
    }

    private void GenerateStarship()
    {
        Starship starship = Object.Instantiate(_prefab);
        starship.gameObject.SetActive(false);
        starship.ObjectPool = this;
        _starShips.Add(starship);
    }

    public void ConfigureDeactivatedStarship(Starship deactivatedObj)
    {
        deactivatedObj.Next = _firstAvailable;
        _firstAvailable = deactivatedObj;
    }

    public GameObject GetStarship(Transform target, Color color, PlanetState state)
    {
        if(_firstAvailable == null)
        {
            GenerateStarship();
            Starship starship = _starShips[_starShips.Count - 1];
            ConfigureDeactivatedStarship(starship);
        }

        Starship newStarship = _firstAvailable;
        _firstAvailable = newStarship.Next;

        ConfigureShip(newStarship, target, color, state);

        return newStarship.gameObject;
    }

    private void ConfigureShip(Starship ship, Transform target, Color color, PlanetState state)
    {
        float radius = _minSpawnDistance + Random.value;
        float x = Random.Range(-radius, radius);
        float y = Mathf.Sqrt(radius * radius - x * x);
        if(Random.Range(0, 2) > 0)
        {
            y = -y;
        }

        ship.transform.position = _transform.position + new Vector3(x, y, 0);

        ship.gameObject.SetActive(true);
        ship.Init(target, color, state, _shipSound);
    }
}

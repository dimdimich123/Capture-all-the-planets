using System.Collections.Generic;
using UnityEngine;

public class StarshipObjectPool
{
    private const int _startCount = 5;
    private readonly Starship _prefab;
    private readonly Transform _transform;
    private readonly List<Starship> _starShips = new List<Starship>(_startCount);

    private Starship _firstAvailable = null;

    public StarshipObjectPool(Starship prefab, Transform transform)
    {
        _prefab = prefab;
        _transform = transform;
        Init();
    }

    private void Init()
    {
        for(int i = 0; i < _startCount; ++i)
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
        Starship starship = Object.Instantiate(_prefab, _transform);
        starship.gameObject.SetActive(false);
        starship.ObjectPool = this;
        _starShips.Add(starship);
    }

    public void ConfigureDeactivatedStarship(Starship deactivatedObj)
    {
        deactivatedObj.Next = _firstAvailable;
        _firstAvailable = deactivatedObj;
    }

    public GameObject GetStarship()
    {
        if(_firstAvailable == null)
        {
            GenerateStarship();
            Starship starship = _starShips[_starShips.Count - 1];
            ConfigureDeactivatedStarship(starship);
        }

        Starship newStarship = _firstAvailable;
        _firstAvailable = newStarship.Next;
        Debug.Log(_starShips.Count);
        return newStarship.gameObject;
    }
}
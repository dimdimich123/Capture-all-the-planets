using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores and implements the player's choice of planets.
/// </summary>
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _pathBetweenPlanets;
    public static PlayerController Instance = null;

    private readonly List<PlanetController> _selectedPlanets = new List<PlanetController>();
    private readonly List<GameObject> _paths = new List<GameObject>();
    private PlanetController _attackTarget = null;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("PlayerController already have instance");
            return;
        }
        Instance = this;
    }

    public void SelectPlanet(PlanetController planet)
    {
        _selectedPlanets.Add(planet);
    }

    public void UnselectPlanet(PlanetController planet)
    {
        _selectedPlanets.Remove(planet);
    }

    public void SetAttackTarget(PlanetController planet)
    {
        if(_selectedPlanets.Count > 0)
        {
            _attackTarget = planet;
            StartAttack();
            UnselectAllPlanets();
            DestroyPath();
            _attackTarget = null;
        }
    }

    private void StartAttack()
    {
        foreach(PlanetController planet in _selectedPlanets)
        {
            planet.AttackPlanet(_attackTarget.transform);
        }
    }

    public void UnselectAllPlanets()
    {
        foreach (PlanetController planet in _selectedPlanets)
        {
            planet.UnselectPlanet();
        }
        _selectedPlanets.Clear();
    }

    public void CreatePathToPlanet(PlanetController planet)
    {
        foreach(PlanetController startPlanet in _selectedPlanets)
        {
            GameObject path = Instantiate(_pathBetweenPlanets, transform);

            Vector3 pos1 = Vector3.MoveTowards(startPlanet.transform.position, planet.transform.position, startPlanet.transform.localScale.x / 2 * 1.2f);
            Vector3 pos2 = Vector3.MoveTowards(planet.transform.position, startPlanet.transform.position, planet.transform.localScale.x / 2 * 1.2f);

            path.transform.position = (pos1 + pos2) / 2;

            float X = Vector3.Distance(pos1, pos2);

            float XspriteSize = path.transform.localScale.x / path.GetComponent<SpriteRenderer>().bounds.size.x;
            path.transform.localScale = new Vector2(X * XspriteSize, path.transform.localScale.y);

            path.transform.right = pos1 - pos2;

            _paths.Add(path);
        }
    }

    public void DestroyPath()
    {
        foreach(GameObject path in _paths)
        {
            Destroy(path);
        }
        _paths.Clear();
    }
}

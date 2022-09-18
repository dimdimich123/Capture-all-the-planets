using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private Starship _starshipPrefab;
    [SerializeField] private GameObject _stroke;
    [SerializeField] private SpriteRenderer _body;

    private PlanetaryStarshipFactory _shipFactory;
    private PlanetUI _planetUI;

    private StarshipObjectPool _objectPool;
    [SerializeField] private PlanetState _state = PlanetState.Neutral;

    private float _maxSpawnShipDistance;
    private float _minSpawnShipDistance;

    private Color _color = Color.white;

    private void Awake()
    {
        _minSpawnShipDistance = gameObject.transform.localScale.x / 2;
        _maxSpawnShipDistance = _minSpawnShipDistance + 1;

        _shipFactory = GetComponent<PlanetaryStarshipFactory>();
        _shipFactory.Init(25);

        UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text>();
        _planetUI = new PlanetUI(_shipFactory, text);

        _objectPool = new StarshipObjectPool(_starshipPrefab, transform);
    }

    public void AttackPlanet(Transform target)
    {
        for(int i = 0; i < _shipFactory.ShipCount / 2; ++i)
        {
            _objectPool.GetStarship(target, _color);
        }
    }

    private void Capitulate(PlanetState state)
    {
        if(_state == PlanetState.Neutral)
        {
            _state = state;
            _shipFactory.StartGenerate();
        }

        if(_state == PlanetState.Enemy)
        {
            _color = Color.black;
        }
        else
        {
            _color = Color.blue;
        }
        _body.color = _color;
    }

    private void OnEnable()
    {
        _planetUI.Enable();
    }

    private void OnDisable()
    {
        _planetUI.Disable();
    }
}

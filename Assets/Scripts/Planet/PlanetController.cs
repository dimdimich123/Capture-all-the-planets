using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private Starship _starshipPrefab = null;
    [SerializeField] private GameObject _stroke;
    private PlanetaryStarshipFactory _shipFactory;
    private PlanetUI _planetUI;

    private StarshipObjectPool _objectPool;
    private PlanetState _state = PlanetState.Neutral;

    private void Awake()
    {
        _shipFactory = GetComponent<PlanetaryStarshipFactory>();
        _shipFactory.Init(25);

        UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text>();
        _planetUI = new PlanetUI(_shipFactory, text);

        _objectPool = new StarshipObjectPool(_starshipPrefab, transform);
    }

    private void OnEnable()
    {
        _planetUI.Enable();
    }

    private void Capitulate(PlanetState state)
    {
        if(_state == PlanetState.Neutral)
        {
            _state = state;
            _shipFactory.StartGenerate();
        }
    }

    private void OnDisable()
    {
        _planetUI.Disable();
    }
}

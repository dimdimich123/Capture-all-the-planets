using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Starship _starshipPrefab;
    [SerializeField] private GameObject _stroke;
    [SerializeField] private SpriteRenderer _body;

    private Level—ontroller _level—ontroller;
    private PlanetaryStarshipFactory _shipFactory;
    private PlanetUI _planetUI;

    private StarshipObjectPool _objectPool;
    public PlanetState State { get; private set; } = PlanetState.Neutral;

    private Color _color = Color.gray;
    private bool _isSelect = false;

    public int ShipCount => _shipFactory.ShipCount;

    private void Awake()
    {
        _shipFactory = GetComponent<PlanetaryStarshipFactory>();

        UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text>();
        _planetUI = new PlanetUI(_shipFactory, text);

        _objectPool = new StarshipObjectPool(_starshipPrefab, transform);
    }

    public void Init(int shipCount, PlanetState state, Level—ontroller level—ontroller, AudioSource shipSound)
    {
        _objectPool.Init(shipSound);
        _level—ontroller = level—ontroller;
        _shipFactory.Init(shipCount);
        ConfigurePlanet(state);
    }

    private void ConfigurePlanet(PlanetState state)
    {
        if (state != PlanetState.Neutral)
        {
            _color = (state == PlanetState.Friendly) ? Color.blue : Color.red;
            State = state;
            _shipFactory.StartGenerate();
        }
        _body.color = _color;
    }

    public void AttackPlanet(Transform target)
    {
        int shipsCount = _shipFactory.GetHalfCountShips();
        for(int i = 0; i < shipsCount; ++i)
        {
            _objectPool.GetStarship(target, _color, State);
        }
    }

    public void ChangeShipCount(PlanetState state)
    {
        if(State != state)
        {
            _shipFactory.ReduceShipCount();
        }
        else
        {
            _shipFactory.IncreaseShipCount();
        }

        if(_shipFactory.ShipCount < 0)
        {
            Capitulate(state);
            _shipFactory.IncreaseShipCount();
        }
    }

    private void Capitulate(PlanetState state)
    {
        switch(State)
        {
            case PlanetState.Neutral:
                {
                    _shipFactory.StartGenerate();
                    _color = (state == PlanetState.Friendly) ? Color.blue : Color.red;
                    break;
                }  
            case PlanetState.Enemy: _color = Color.blue; break;
            case PlanetState.Friendly: _color = Color.red; break;
            default: throw new System.Exception($"Unknown PlanetState/ Error in {nameof(PlanetController)}");
        }

        State = state;
        _body.color = _color;

        _level—ontroller.CapitulatePlanet(State);
    }

    public void UnselectPlanet()
    {
        _stroke.SetActive(false);
        _isSelect = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (State == PlanetState.Friendly)
        {
            if(_isSelect)
            {
                PlayerController.Instance.UnselectPlanet(this);
                UnselectPlanet();
            }
            else
            {
                PlayerController.Instance.SelectPlanet(this);
                _stroke.SetActive(true);
                _isSelect = true;
            }
        }
        else if(State != PlanetState.Friendly)
        {
            PlayerController.Instance.SetAttackTarget(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(State != PlanetState.Friendly)
        {
            _stroke.SetActive(true);
            PlayerController.Instance.CreatePathToPlanet(this);
        }
        else
        {
            if(!_isSelect)
            {
                _stroke.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (State != PlanetState.Friendly)
        {
            _stroke.SetActive(false);
            PlayerController.Instance.DestroyPath();
        }
        else
        {
            if (!_isSelect)
            {
                _stroke.SetActive(false);
            }
        }
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

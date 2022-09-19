using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Starship _starshipPrefab;
    [SerializeField] private GameObject _stroke;
    [SerializeField] private SpriteRenderer _body;

    private PlanetaryStarshipFactory _shipFactory;
    private PlanetUI _planetUI;

    private StarshipObjectPool _objectPool;
    [SerializeField] private PlanetState _state = PlanetState.Neutral;

    private Color _color = Color.gray;
    private bool _isSelect = false;

    private void Awake()
    {
        _shipFactory = GetComponent<PlanetaryStarshipFactory>();
        _shipFactory.Init(60);

        UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text>();
        _planetUI = new PlanetUI(_shipFactory, text);

        _objectPool = new StarshipObjectPool(_starshipPrefab, transform);

        if (_state == PlanetState.Enemy)
        {
            _color = Color.red;
            _shipFactory.StartGenerate();
        }
        else if(_state == PlanetState.Friendly)
        {
            _color = Color.blue;
            _shipFactory.StartGenerate();
        }
        _body.color = _color;
    }

    public void AttackPlanet(Transform target)
    {
        int shipsCount = _shipFactory.GetHalfCountShips();
        for(int i = 0; i < shipsCount; ++i)
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

    public void UnselectPlanet()
    {
        _stroke.SetActive(false);
        _isSelect = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_state == PlanetState.Friendly)
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
        else if(_state != PlanetState.Friendly)
        {
            PlayerController.Instance.SetAttackTarget(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_state != PlanetState.Friendly)
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
        if (_state != PlanetState.Friendly)
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

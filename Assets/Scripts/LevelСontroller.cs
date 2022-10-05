using System.Collections.Generic;
using UnityEngine;

public sealed class LevelСontroller : MonoBehaviour
{
    private const int _startShipCount = 50;
    private const float _distance = 10;
    private EnemyAI _enemyAI;
    private List<PlanetController> _planets = new List<PlanetController>();

    [SerializeField] private Camera _camera;
    [SerializeField] private PlanetController _planetPrefab;
    [SerializeField] private SettingsContainer _settings;


    private void Awake()
    {
        GeneratePlanets();
        CreateEnemyAI();
    }

    private void GeneratePlanets()
    {
        Vector3 p3 = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _distance));
        p3.x -= _settings.Settings.MaxPlanetSize / 2;
        p3.y -= _settings.Settings.MaxPlanetSize / 2;
        List<KeyValuePair<Transform, float>> createdPlanets = new List<KeyValuePair<Transform, float>>();
        PlanetState state = PlanetState.Friendly;

        for (int i = 0; i < _settings.Settings.PlanetCount;)
        {
            float newPlanetRadius = Random.Range(_settings.Settings.MinPlanetSize, _settings.Settings.MaxPlanetSize) / 2;
            Vector3 newPlanetPosition = new Vector3(Random.Range(-p3.x, p3.x), Random.Range(-p3.y, p3.y), 0);

            bool isFreePlace = true;
            for (int j = 0; j < createdPlanets.Count; ++j)
            {
                Vector3 pos1 = Vector3.MoveTowards(newPlanetPosition, createdPlanets[j].Key.position, newPlanetRadius);
                Vector3 pos2 = Vector3.MoveTowards(createdPlanets[j].Key.position, newPlanetPosition, createdPlanets[j].Value);
                float distance = Vector3.Distance(pos1, pos2);
                if (distance < createdPlanets[j].Value + newPlanetRadius)
                {
                    isFreePlace = false;
                    break;
                }
            }

            if(isFreePlace)
            {
                Transform newPlanet = Instantiate(_planetPrefab).GetComponent<Transform>();
                newPlanet.position = newPlanetPosition;
                newPlanet.localScale = new Vector3(newPlanetRadius  *2, newPlanetRadius * 2, 1);

                int shipCount = Random.Range(_settings.Settings.MinShipCount, _settings.Settings.MaxShipCount);

                PlanetController newPlanetController = newPlanet.GetComponent<PlanetController>();
                if (i > 1)
                {
                    newPlanetController.Init(shipCount, PlanetState.Neutral);
                }
                else
                {
                    newPlanetController.Init(_startShipCount, state);
                    state = PlanetState.Enemy;
                }

                _planets.Add(newPlanetController);
                createdPlanets.Add(new KeyValuePair<Transform, float>(newPlanet, newPlanetRadius));
                i++;
            }
        }
    }

    private void CreateEnemyAI()
    {
        switch(_settings.Settings.Difficulty)
        {
            case GameDifficulty.Easy:
                _enemyAI = (EnemyAI)gameObject.AddComponent(typeof(EasyEnemy));
                break;
            case GameDifficulty.Normal:
                _enemyAI = (EnemyAI)gameObject.AddComponent(typeof(NormalEnemy));
                break;
            case GameDifficulty.Hard:
                _enemyAI = (EnemyAI)gameObject.AddComponent(typeof(HardEnemy));
                break;
            default:
                throw new System.Exception("Unknown game difficulty " + _settings.Settings.Difficulty + "  /  " + nameof(LevelСontroller));
        }
        _enemyAI.Init(_planets);
    }
}

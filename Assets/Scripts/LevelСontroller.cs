using System.Collections.Generic;
using UnityEngine;

public sealed class LevelСontroller : MonoBehaviour
{
    private const int _startShipCount = 50;
    private const float _distance = 10;

    [SerializeField] private Camera _camera;
    [SerializeField] private PlanetController _planetPrefab;
    [SerializeField] private GameSettings _settings;


    private void Awake()
    {
        GeneratePlanets();
    }

    private void GeneratePlanets()
    {
        Vector3 p3 = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _distance));
        p3.x -= _settings.MaxPlanetSize / 2;
        p3.y -= _settings.MaxPlanetSize / 2;
        List<KeyValuePair<Transform, float>> _createdPlanets = new List<KeyValuePair<Transform, float>>();
        PlanetState state = PlanetState.Friendly;

        for (int i = 0; i < _settings.PlanetCount;)
        {
            float newPlanetRadius = Random.Range(_settings.MinPlanetSize, _settings.MaxPlanetSize) / 2;
            Vector3 newPlanetPosition = new Vector3(Random.Range(-p3.x, p3.x), Random.Range(-p3.y, p3.y), 0);

            bool isFreePlace = true;
            for (int j = 0; j < _createdPlanets.Count; ++j)
            {
                Vector3 pos1 = Vector3.MoveTowards(newPlanetPosition, _createdPlanets[j].Key.position, newPlanetRadius);
                Vector3 pos2 = Vector3.MoveTowards(_createdPlanets[j].Key.position, newPlanetPosition, _createdPlanets[j].Value);
                float distance = Vector3.Distance(pos1, pos2);
                if (distance < _createdPlanets[j].Value + newPlanetRadius)
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

                int shipCount = Random.Range(_settings.MinShipCount, _settings.MaxShipCount);
                
                if (i > 1)
                {
                    newPlanet.GetComponent<PlanetController>().Init(shipCount, PlanetState.Neutral);
                }
                else
                {
                    newPlanet.GetComponent<PlanetController>().Init(_startShipCount, state);
                    state = PlanetState.Enemy;
                }
                _createdPlanets.Add(new KeyValuePair<Transform, float>(newPlanet, newPlanetRadius));
                i++;
            }
        }
    }
}

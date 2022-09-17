using System;
using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(PlanetController))]
public sealed class PlanetaryStarshipFactory : MonoBehaviour
{
    private const float _timeBetweenGeneration = 1f;
    private const int _generateCount = 5;
    public int ShipCount { get; private set; } = 0;

    public event Action<int> OnShipCreate;

    void Start()
    {
        OnShipCreate?.Invoke(ShipCount);
        StartCoroutine(GenerateShips());
    }

    public void Init(int shipCount)
    {
        ShipCount = shipCount;
    }

    private IEnumerator GenerateShips()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenGeneration);
            ShipCount += _generateCount;
            OnShipCreate?.Invoke(ShipCount);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

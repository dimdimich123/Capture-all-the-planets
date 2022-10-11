using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// AI Enemy of Easy Difficulty.
/// </summary>
/// <remarks>
/// The AI ​​will attack RANDOM planet where the number of ships
/// is less than half the number of ships on the planet with the AI's maximum number of ships.
/// </remarks>

public sealed class EasyEnemy : EnemyAI
{
    protected override IEnumerator TryAttack()
    {
        while (true)
        {
            _attackingPlanet = null;
            _attackedPlanet = null;
            _attackingPlanet = FindAttackingPlanet();
            _attackedPlanet = FindAttackedPlanet();
            if (_attackingPlanet && _attackedPlanet)
            {
                AttackPlanet();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void AttackPlanet()
    {
        _attackingPlanet.AttackPlanet(_attackedPlanet.transform);
    }

    protected override PlanetController FindAttackedPlanet()
    {
        List<PlanetController> _canAttackPlanet = new List<PlanetController>();
        foreach (PlanetController planet in _planets)
        {
            if (planet.State != PlanetState.Enemy && planet.ShipCount < _attackingPlanet.ShipCount / 2)
            {
                _canAttackPlanet.Add(planet);
            }
        }

        if (_canAttackPlanet.Count > 0)
        {
            return _canAttackPlanet[Random.Range(0, _canAttackPlanet.Count)];
        }
        else
        {
            return null;
        }
    }

    protected override PlanetController FindAttackingPlanet()
    {
        PlanetController attackingPlanet = null;
        int maxShipCount = 0;
        foreach (PlanetController planet in _planets)
        {
            if (planet.State == PlanetState.Enemy && planet.ShipCount > maxShipCount)
            {
                attackingPlanet = planet;
                maxShipCount = attackingPlanet.ShipCount;
            }
        }
        return attackingPlanet;
    }
}

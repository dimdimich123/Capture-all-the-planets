using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EasyEnemy : EnemyAI
{
    protected override IEnumerator TryAttack()
    {
        //yield return new WaitForSeconds(0.5f);
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
        //return _canAttackPlanet[Random.Range(0, _canAttackPlanet.Count)];
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

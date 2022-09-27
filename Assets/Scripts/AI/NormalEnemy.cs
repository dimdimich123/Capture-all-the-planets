using System.Collections;
using UnityEngine;

public class NormalEnemy : EnemyAI
{
    protected override IEnumerator TryAttack()
    {
        while(true)
        {
            _attackingPlanet = null;
            _attackedPlanet = null;
            _attackingPlanet = FindAttackingPlanet();
            _attackedPlanet = FindAttackedPlanet();
            if(_attackingPlanet && _attackedPlanet)
            {
                AttackPlanet();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override void AttackPlanet()
    {
        _attackingPlanet.AttackPlanet(_attackedPlanet.transform);
    }

    protected override PlanetController FindAttackedPlanet()
    {
        PlanetController attackedPlanet = null;
        foreach (PlanetController planet in _planets)
        {
            if (planet.State != PlanetState.Enemy && planet.ShipCount < _attackingPlanet.ShipCount / 2)
            {
                attackedPlanet = planet;
                break;
            }
        }
        return attackedPlanet;
    }

    protected override PlanetController FindAttackingPlanet()
    {
        PlanetController attackingPlanet = null;
        int maxShipCount = 0;
        foreach(PlanetController planet in _planets)
        {
            if(planet.State == PlanetState.Enemy && planet.ShipCount > maxShipCount)
            {
                attackingPlanet = planet;
                maxShipCount = attackingPlanet.ShipCount;
            }
        }
        return attackingPlanet;
    }
}

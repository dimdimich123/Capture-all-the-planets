using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy : EnemyAI
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
            yield return new WaitForSeconds(0.25f);
        }
    }

    protected override void AttackPlanet()
    {
        _attackingPlanet.AttackPlanet(_attackedPlanet.transform);
    }

    protected override PlanetController FindAttackedPlanet()
    {
        PlanetController neutralPlanet = null;
        PlanetController playerPlanet = null;
        int neutralMinShipCount = int.MaxValue;
        int playerlMinShipCount = int.MaxValue;

        foreach (PlanetController planet in _planets)
        {
            if (planet.State == PlanetState.Neutral && planet.ShipCount < _attackingPlanet.ShipCount / 2 && neutralMinShipCount > planet.ShipCount)
            {
                neutralPlanet = planet;
                neutralMinShipCount = planet.ShipCount;
            }

            if (planet.State == PlanetState.Friendly && planet.ShipCount < _attackingPlanet.ShipCount / 2 && playerlMinShipCount > planet.ShipCount)
            {
                playerPlanet = planet;
                playerlMinShipCount = planet.ShipCount;
            }
        }

        if (neutralPlanet)
        {
            return neutralPlanet;
        }
        else
        {
            return playerPlanet;
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

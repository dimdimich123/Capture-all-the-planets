using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected PlanetController _attackingPlanet = null;
    protected PlanetController _attackedPlanet = null;
    protected List<PlanetController> _planets = new List<PlanetController>();

    public void Init(List<PlanetController> planets)
    {
        _planets = planets;
    }

    protected abstract void AttackPlanet();
    protected abstract PlanetController FindAttackingPlanet();
    protected abstract PlanetController FindAttackedPlanet();

    private void OnEnable()
    {
        StartCoroutine(TryAttack());
    }

    protected abstract IEnumerator TryAttack();

    public void Disable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

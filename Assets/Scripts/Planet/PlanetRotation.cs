using UnityEngine;

/// <summary>
/// Rotates an object around forward axis.
/// </summary>

public sealed class PlanetRotation : MonoBehaviour
{
    private const float _minSpeed = 0.01f;
    private const float _maxSpeed = 0.1f;

    private Transform _transform;
    private float _speed;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _transform.Rotate(Vector3.forward, Random.Range(-180, 180));
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void OnEnable()
    {
        StartCoroutine(Rotate());
    }

    private System.Collections.IEnumerator Rotate()
    {
        while(true)
        {
            _transform.Rotate(Vector3.forward, _speed);
            yield return null;
        }
        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

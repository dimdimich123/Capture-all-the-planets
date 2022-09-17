using UnityEngine;

public class Starship : MonoBehaviour
{
    [System.NonSerialized] public Starship Next;
    [System.NonSerialized] public StarshipObjectPool ObjectPool;
}

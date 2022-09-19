using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "GameSettings", order = 1)]
public sealed class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class MinMax<T> where T : struct
    {
        public T Min;
        public T Max;
    }

    [Header("Planets settings")]
    [SerializeField] private int _planetCount;
    [SerializeField] private MinMax<float> _size;
    [SerializeField] private MinMax<int> _startShipsCount;

    [Header("Difficulty")]
    [SerializeField] private GameDifficulty _difficulty;

    public int PlanetCount => _planetCount;
    public float MinPlanetSize => _size.Min;
    public float MaxPlanetSize => _size.Max;

    public float MinShipCount => _startShipsCount.Min;
    public float MaxShipCount => _startShipsCount.Max;
    public GameDifficulty Difficulty => _difficulty;

}

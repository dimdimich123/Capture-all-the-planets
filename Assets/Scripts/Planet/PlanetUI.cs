using UnityEngine.UI;

/// <summary>
/// Displaying number of starship on the planet.
/// </summary>
public sealed class PlanetUI
{
    private PlanetaryStarshipFactory _shipFactory;
    private Text _text;

    public PlanetUI(PlanetaryStarshipFactory shipFactory, Text text)
    {
        _shipFactory = shipFactory;
        _text = text;
    }

    public void Enable()
    {
        _shipFactory.OnCountChange += UpdateText;
    }

    private void UpdateText(int shipCount)
    {
        _text.text = shipCount.ToString();
    }

    public void Disable()
    {
        _shipFactory.OnCountChange -= UpdateText;
    }
}

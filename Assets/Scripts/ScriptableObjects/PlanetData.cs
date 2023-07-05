using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "ScriptableObjects/PlanetData", order = 1)]
public class PlanetData : ScriptableObject
{
    public string planetName;
    public int sizeProportion;

    public PlanetData parentPlanet;
    public int distanceFromParentProportion;
    public int orbitSpeedProportion;
    public int selfTurnSpeedProportion;
}

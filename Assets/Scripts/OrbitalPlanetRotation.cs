using System.Collections;
using UniRx;
using UnityEngine;

public class OrbitalPlanetRotation : MonoBehaviour
{
    [SerializeField]
    PlanetData _planetData;

    GameObject parent;

    [SerializeField]
    string systemCenterName = "Sun";

    [SerializeField]
    int distanceMultiplier = 30;

    [SerializeField]
    float orbitalSpeedMultiplier = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

        parent = GameObject.Find(_planetData.parentPlanet.name);

        StartCoroutine(SetOrbitPosition());

        Observable.EveryUpdate()
            .Subscribe(
                _ => { OrbitRotation(); })
            .AddTo(this);
    }

    void OrbitRotation()
    {
        transform.RotateAround(
            parent.transform.position, Vector3.up,
            _planetData.orbitSpeedProportion * orbitalSpeedMultiplier *
                SpaceTimeManager.speedMultiplier * Time.deltaTime);
    }

    IEnumerator SetOrbitPosition()
    {
        // Wait until parent is in position if current planet are in an inner
        // systems
        while (parent.name != systemCenterName &&
               parent.transform.position.z < 10)
        {
            yield return null;
        }
        transform.position = parent.transform.position +
                             new Vector3(0, 0,
                                         _planetData.distanceFromParentProportion *
                                             distanceMultiplier);

        yield break;
    }
}

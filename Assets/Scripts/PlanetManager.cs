using UniRx;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField]
    PlanetData _planetData;

    GameObject parent;

    [SerializeField]
    int distanceMultiplier = 30;

    [SerializeField]
    float speedMultiplier = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

        parent = GameObject.Find(_planetData.parentPlanet.name);
        transform.position = parent.transform.position + new Vector3(
            0, 0, _planetData.distanceFromParentProportion * distanceMultiplier);

        Observable.EveryUpdate()
            .Subscribe(
                _ =>
                {
                    transform.RotateAround(parent.transform.position, Vector3.up,
                                       _planetData.orbitSpeedProportion *
                                           speedMultiplier * Time.deltaTime);


                    transform.localScale = Vector3.one * _planetData.sizeProportion / transform.parent.localScale.x;

                })
            .AddTo(this);
    }
}

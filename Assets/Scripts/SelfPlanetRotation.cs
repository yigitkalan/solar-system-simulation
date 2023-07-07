using DG.Tweening;
using UniRx;
using UnityEngine;

public class SelfPlanetRotation : MonoBehaviour
{
    [SerializeField]
    PlanetData _planetData;

    GameObject parent;

    [SerializeField]
    float selfRotateMultiplier = 0.5f;
    Tweener selfRotateTween;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find(_planetData.parentPlanet.name);

        RotateSelf();

        Observable.EveryUpdate()
            .Subscribe(
                _ => { SetScale(); })
            .AddTo(this);
    }

    void RotateSelf()
    {

        // make this faster
        // pick either 360 or -360 at random
        int randomDirection = Random.Range(0, 2) * 2 - 1;

        selfRotateTween = transform
                              .DORotate(new Vector3(0, 360 * randomDirection, 0),
                                        _planetData.selfTurnSpeedProportion *
                                            selfRotateMultiplier *
                                            SpaceTimeManager.speedMultiplier,
                                        RotateMode.FastBeyond360)
                              .SetEase(Ease.Linear)
                              .SetLoops(-1);
    }

    void SetScale()
    {
        transform.localScale = Vector3.one * _planetData.sizeProportion /
                               transform.parent.localScale.x;
    }
}

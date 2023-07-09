using DG.Tweening;
using UniRx;
using UnityEngine;

public class SelfPlanetRotation : MonoBehaviour
{
    [SerializeField]
    PlanetData _planetData;

    GameObject parent;

    Tween selfRotateTween;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find(_planetData.parentPlanet.name);

        RotateSelf();

        Observable.EveryUpdate()
            .Subscribe(
                _ =>
                {
                    SetScale();
                })
            .AddTo(this);
    }

    void RotateSelf()
    {
            print(SpaceTimeManager.speedMultiplier);
            selfRotateTween = transform
                                  .DORotate(new Vector3(0, _planetData.selfTurnSpeedProportion *
                                                SpaceTimeManager.speedMultiplier, 0), 0.05f,
                                            RotateMode.FastBeyond360)
                                  .SetLoops(-1, LoopType.Incremental);

    }

    void SetScale()
    {
        transform.localScale = Vector3.one * _planetData.sizeProportion /
                               transform.parent.localScale.x;
    }
}

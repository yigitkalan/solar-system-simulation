using DG.Tweening;
using UniRx;
using UnityEngine;

public class SelfPlanetRotation : MonoBehaviour
{
    [SerializeField]
    PlanetData _planetData;

    GameObject parent;


    [SerializeField]
    int selfRotateMultiplier = 100;
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
        selfRotateTween = transform
                              .DORotate(new Vector3(0, 360, 0),
                                        _planetData.selfTurnSpeedProportion/2,
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

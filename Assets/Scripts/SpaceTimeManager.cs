using UniRx;
using UnityEngine;

public class SpaceTimeManager : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 30)]
    float speedMultiplier;



    void Start()
    {
        //when speedMultiplier changes change the Time.timescale
        this.ObserveEveryValueChanged(x => x.speedMultiplier)
            .Subscribe(u => Time.timeScale = u)
            .AddTo(this);

    }
}

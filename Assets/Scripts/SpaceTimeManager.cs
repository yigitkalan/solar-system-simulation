using Lean.Touch;
using UniRx;
using UnityEngine;

public class SpaceTimeManager : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 30)]
    public static float speedMultiplier;

    LeanPinchScale leanPinchScale;
    void Awake(){
        speedMultiplier = 1;
    }


    void Start()
    {

        Observable.EveryUpdate()
            .Subscribe(_ => GetInput());

    }

    void GetInput()
    {
        if(Input.GetKey(KeyCode.UpArrow) && speedMultiplier < 30){
            speedMultiplier += 0.1f;
        }
        else if(Input.GetKey(KeyCode.DownArrow) && speedMultiplier > 0.1f){
            speedMultiplier -= 0.1f;
        }
    }
}

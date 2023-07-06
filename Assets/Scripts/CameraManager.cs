using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    List<Cinemachine.CinemachineVirtualCamera> cameras =
        new List<Cinemachine.CinemachineVirtualCamera>();

    void Start()
    {
        // disable all cameras
        cameras.ForEach(c => c.gameObject.SetActive(false));
        cameras[0].gameObject.SetActive(true); // enable first camera]

        // change enabled camera when pressed 0-9
        Observable.EveryUpdate()
            .Where(
                _ => Input.anyKey)
            .Subscribe(
                _ => OnPlanetChanged(GetPressed()));
    }
    int GetPressed()
    {
        //pressed from 0 to 9 with Input.GetKet(Keycode.Alpha0) to Input.GetKey(Keycode.Alpha9))
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKey(KeyCode.Alpha0 + i))
            {
                return i;
            }
        }
            return -1;
    }

    void OnPlanetChanged(int planetIndex)
    {
        cameras.ForEach(c =>
        {
            c.gameObject.SetActive(false);
        });
        print(planetIndex);
        cameras[planetIndex].gameObject.SetActive(true);
    }
}

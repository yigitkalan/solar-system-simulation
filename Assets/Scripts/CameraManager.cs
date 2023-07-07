using System.Collections.Generic;
using Cinemachine;
using Lean.Touch;
using UniRx;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    CinemachineVirtualCamera currentVirtualCamera;
    CinemachineComposer currentCamComposer;

    void Start()
    {
        // disable all cameras
        cameras.ForEach(c => c.Priority = 0);
        cameras[0].Priority = 1;
        currentVirtualCamera = cameras[0];
        currentCamComposer =
            currentVirtualCamera.GetCinemachineComponent<CinemachineComposer>();

        Observable.EveryUpdate()
            .Where(
                _ => GetPressed() != -1)
            .Subscribe(
                _ => OnPlanetChanged(GetPressed()));

        Observable.EveryUpdate().Subscribe(
            _ => Zoom());

        LeanTouch.OnFingerSwipe += (finger) =>
        {
            Vector2 swipe = finger.SwipeScreenDelta;
            if (swipe.x > 0)
            {
                currentCamComposer.m_ScreenX -= 0.1f;
            }
            else if (swipe.x < 0)
            {
                currentCamComposer.m_ScreenX += 0.1f;
            }
            if (swipe.y > 0)
            {
                currentCamComposer.m_ScreenY -= 0.1f;
            }
            else if (swipe.y < 0)
            {
                currentCamComposer.m_ScreenY += 0.1f;
            }
        };
    }
    int GetPressed()
    {
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
        cameras.ForEach(c => { c.Priority = 0; });
        cameras[planetIndex].Priority = 1;
        currentVirtualCamera = cameras[planetIndex];
        currentCamComposer =
            currentVirtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    void Zoom()
    {
        if (Input.GetKey(KeyCode.RightArrow) &&
            currentVirtualCamera.m_Lens.FieldOfView > 5f)
        {
            currentVirtualCamera.m_Lens.FieldOfView -= 0.5f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) &&
                   currentVirtualCamera.m_Lens.FieldOfView < 100f)
        {
            { currentVirtualCamera.m_Lens.FieldOfView += 0.5f; }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera _vcam;
    private CinemachineFramingTransposer _framingTransposer;
    [SerializeField] private float xOffset;
    [SerializeField] private float transitionDuration;
    private bool onRight;
    // Start is called before the first frame update
    void Start()
    {
        _vcam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
        _framingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_vcam.m_Follow.transform.localScale.x < 0f && onRight)
        {
            onRight = false;
            DOTween.To(() => _framingTransposer.m_TrackedObjectOffset.x, x => _framingTransposer.m_TrackedObjectOffset.x = x, -xOffset, transitionDuration);
        }
        else if(_vcam.m_Follow.transform.localScale.x > 0f && !onRight)
        {
            onRight = true;
            DOTween.To(() => _framingTransposer.m_TrackedObjectOffset.x, x => _framingTransposer.m_TrackedObjectOffset.x = x, xOffset, transitionDuration);
        }
    }
}

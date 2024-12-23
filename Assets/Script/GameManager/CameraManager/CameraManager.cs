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
    private float currentX;
    [SerializeField] private float transitionDuration;
    private float currentTimeHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        _vcam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
        _framingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        currentX = _framingTransposer.m_TrackedObjectOffset.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_vcam.m_Follow.transform.localScale.x < 0f)
        {
            //DOTween.To(() => _framingTransposer.m_TrackedObjectOffset.x, x => _framingTransposer.m_TrackedObjectOffset.x = x, -xOffset, 1f);
            currentTimeHorizontal += Time.deltaTime / transitionDuration;
            _framingTransposer.m_TrackedObjectOffset.x = Mathf.Lerp(currentX,-xOffset, currentTimeHorizontal);
        }
        else
        {
            //DOTween.To(() => _framingTransposer.m_TrackedObjectOffset.x, x => _framingTransposer.m_TrackedObjectOffset.x = x, xOffset, 1f);
            currentTimeHorizontal += Time.deltaTime / transitionDuration;
            _framingTransposer.m_TrackedObjectOffset.x = Mathf.Lerp(currentX, xOffset, currentTimeHorizontal);
        }

        if(_vcam.m_Follow.transform.localScale.x < 0f && _framingTransposer.m_TrackedObjectOffset.x == -xOffset
            || _vcam.m_Follow.transform.localScale.x > 0f && _framingTransposer.m_TrackedObjectOffset.x == xOffset)
        {
            currentTimeHorizontal = 0f;
            currentX = _framingTransposer.m_TrackedObjectOffset.x;
        }
    }
}

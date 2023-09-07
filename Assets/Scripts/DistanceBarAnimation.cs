using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBarAnimation : MonoBehaviour
{
    [SerializeField] GameObject _animStartPosition, _animEndPosition;
    [SerializeField] GameObject _iconCar;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;


    private void Update()
    {
        LerpIcon();
    }
    void LerpIcon()
    {
        if (_iconCar != null && _virtualCamera != null && _animStartPosition != null && _animEndPosition != null)
        {
            _iconCar.transform.position = Vector3.Lerp(_animStartPosition.transform.position, _animEndPosition.transform.position,
                _virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition);
        }
    }
}

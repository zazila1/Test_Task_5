using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private float _K_position = 0.01f;
    [SerializeField] private float _ViewOffsetRange = 0.05f;
    [SerializeField] private float _K_offset = 0.01f;
    private Camera _Camera;
    private Transform _CamTransform;
    private Vector3 _PrevPos = Vector3.zero;
    private Vector3 _PrevOffset = Vector3.zero;

    private Vector3 _Cd;
    private void Awake()
    {
        _Camera = Camera.main;
        _CamTransform = GetComponent<Transform>();
    }


    void LateUpdate()
    {
        MoveCameraToPlayer(_Player.transform.position);
    }

    private void FixedUpdate()
    {
        //CalculateNewCameraPosition();
    }

    private void MoveCameraToPlayer(Vector3 playerPosition)
    {
        // Наработки на будущее. Нужно сделать смещение камеры в зависимости от растояния до прицела
        
        //Vector3 mousePosition = _Camera.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 mousePosition = Input.mousePosition;
        // Vector3 playerPosInScreenCoords = _Camera.WorldToScreenPoint(playerPosition);
        // Vector3 viewOffset = mousePosition - playerPosInScreenCoords; //_Player.transform.up * _ViewOffsetRange;
        // Debug.Log($"mousePosition {mousePosition} // playerPosInScreenCoords {playerPosInScreenCoords} " +
        //           $"// viewOffset {viewOffset} // dist {Vector2.Distance(_Camera.ScreenToWorldPoint(mousePosition), playerPosition)} //");
        //
        // var w = _Camera.WorldToScreenPoint(Screen.width);
        // viewOffset = new Vector3(Mathf.Clamp(viewOffset.x, Screen.width * 0.1f, Screen.width * 0.8f),
        //     Mathf.Clamp(viewOffset.y, Screen.height * 0.1f, Screen.height * 0.8f), viewOffset.z);
        //
        // viewOffset = _Camera.ScreenToWorldPoint(viewOffset);
        
        Vector3 viewOffset = _Player.transform.up * _ViewOffsetRange;
        Vector3 newViewOffset = _PrevOffset * (1f - _K_offset) + viewOffset * _K_offset;
        _PrevOffset = newViewOffset;
        
        Vector3 newPos = _PrevPos * (1f - _K_position) + playerPosition * _K_position;
        _PrevPos = newPos + newViewOffset;

        Vector3 camPosition = new Vector3(newPos.x, newPos.y, _CamTransform.position.z);
        _CamTransform.position = camPosition;
    }
}

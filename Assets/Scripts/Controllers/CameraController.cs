using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private float _K_position = 0.4f;
    [SerializeField] private float viewOffset = 0;
    private Transform _CamTransform;
    private Vector3 _PrevPos = Vector3.zero;

    private void Awake()
    {
        _CamTransform = GetComponent<Transform>();
    }


    void LateUpdate()
    {
        MoveCameraToPlayer(_Player.transform.position);
    }
    
    private void MoveCameraToPlayer(Vector3 playerPosition)
    {
        var camPosition = _CamTransform.position;
        Vector3 newPos = _PrevPos * (1f - _K_position) + playerPosition * _K_position;
        _PrevPos = newPos + _Player.transform.up * viewOffset;

        camPosition = new Vector3(newPos.x, newPos.y, camPosition.z);
        _CamTransform.position = camPosition;
    }
}

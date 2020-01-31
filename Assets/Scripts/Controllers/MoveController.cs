using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _BasePlayerSpeed;
    
    private Transform _PlayerTransform;
    private Rigidbody2D _Rigidbody2D;
    private Camera _Camera;

    private void Awake()
    {
        _PlayerTransform = GetComponent<Transform>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Moving();
        Rotating();
    }

    private void Moving()
    {
        Vector3 verticalDirection = Vector3.up * Input.GetAxisRaw("Vertical");
        
        Vector3 horizontalDirection = Vector3.right * Input.GetAxisRaw("Horizontal");

        Vector3 finalVelocity = (verticalDirection + horizontalDirection).normalized * (_BasePlayerSpeed * Time.fixedDeltaTime);
        
        _Rigidbody2D.velocity = finalVelocity;
    }
    
    private void Rotating()
    {
        // Vector3 mousePosition = _Camera.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(_PlayerTransform.position + " /// " + mousePosition);
        //
        // Vector3 viewVector = _PlayerTransform.position - mousePosition;
        // Quaternion rotation = Quaternion.LookRotation(viewVector, _PlayerTransform.forward);
        //
        // _PlayerTransform.rotation = rotation;  
        // _PlayerTransform.eulerAngles = new Vector3(0, 0,_PlayerTransform.eulerAngles.z);

        var viewDir = Input.mousePosition - _Camera.WorldToScreenPoint(_PlayerTransform.position);

        var angle = Mathf.Atan2(viewDir.y, viewDir.x) * Mathf.Rad2Deg;

        var quaternion = Quaternion.AngleAxis(angle, _PlayerTransform.forward);

        //Debug.Log($"mousePosition = {Input.mousePosition} // mousePosition.word = {mousePosition} ");
    }

}

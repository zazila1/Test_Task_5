using UnityEngine;
using UnityEngine.UIElements;

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
        Vector3 mousePosition = _Camera.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(_PlayerTransform.position - mousePosition, Vector3.forward);
        
        _PlayerTransform.rotation = rotation;  
        _PlayerTransform.eulerAngles = new Vector3(0, 0,_PlayerTransform.eulerAngles.z);

        //Debug.Log($"mousePosition = {Input.mousePosition} // mousePosition.word = {mousePosition} ");
    }

}

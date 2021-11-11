using UnityEngine;

public class CamMovementController : MonoBehaviour {

    [SerializeField] private float _speed;

    void Update() => transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime, 0, 0));
    
}

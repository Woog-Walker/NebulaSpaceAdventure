using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    [SerializeField] Vector3 position_offset;
    [SerializeField] Vector3 rotation_angles;
    [Space]
    [SerializeField] Transform _player;
    [SerializeField] Transform _camera;

    private void FixedUpdate()
    {
        _camera.transform.position = new Vector3(_player.position.x, _player.position.y, _player.position.z) + position_offset;
        _camera.transform.rotation = Quaternion.Euler(rotation_angles);
    }

}
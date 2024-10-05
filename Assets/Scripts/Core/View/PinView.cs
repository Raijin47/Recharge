using UnityEngine;

public class PinView : MonoBehaviour
{
    private MeshFilter _mesh;

    private void Awake() => _mesh = GetComponent<MeshFilter>();

    private void OnEnable()
    {
        Game.Pin.OnSwapPin += SetMesh;
        _mesh.mesh = Game.Pin.CurrentPin;
        transform.localPosition = Game.Pin.PinOffset;
    }

    private void OnDisable() => Game.Pin.OnSwapPin -= SetMesh;

    private void SetMesh() 
    {
        _mesh.mesh = Game.Pin.CurrentPin;
        transform.localPosition = Game.Pin.PinOffset;
    } 
}
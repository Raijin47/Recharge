using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject[] _stage;

    private void Start() => Game.Level.AddLevel(this);

    public int ID => _id;

    public void SetPosition(Vector3 pos) => transform.position = pos;

    public void Activate(int stage) => _stage[stage].SetActive(true);

    public void Deactivate(int stage) => _stage[stage].SetActive(false);
}
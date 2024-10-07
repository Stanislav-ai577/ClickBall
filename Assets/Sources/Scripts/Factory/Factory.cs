using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Circle _circle;

    private void Awake()
    {
        _circle = Resources.Load<Circle>("Prefab/Circle");
    }

    public Circle CircleCreated(Vector3 position, Transform parent)
    {
        return Instantiate(_circle, position, Quaternion.identity, parent);
    }
}
using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Circle : MonoBehaviour
{
    public Action OnRemoveCircle;
    
    [SerializeField] private MeshRenderer _renderer;
    private Vector3 _startPosition;
    private Counter _counter;

    private void OnValidate()
    {
        _renderer ??= GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Circle circle))
        {
            Destroy(circle.gameObject);
        }
    }

    public Circle SetColor(Color color)
    {
        _renderer.materials[0].color = color;
        return this;
    }

    public Circle SetMove()
    {
        _startPosition = transform.position;
        transform.DOMove(_startPosition + new Vector3(1, 0), 1).OnComplete(() =>
        {
            transform.DOMove(_startPosition + new Vector3(-1, 0), 1);
        }).SetLoops(-1, LoopType.Yoyo);
        return this;
    }

    public Circle Setup(Counter counter)
    {
        _counter = counter;
        return this;
    }
    
    private void OnMouseDown()
    {
        OnRemoveCircle?.Invoke();
        Destroy(gameObject);
    }
    
    public void ClickCircle()
    {
        _counter.AddCount(1);
    }
    
    public void ClickBonusCircle()
    {
        _counter.AddCount(2);
    }
}
using System;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;
    private int _count;

    public void AddCount(int value)
    {
        if (value < 0)
            throw new Exception("Value most be positive");
        _count += value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _counter.text = _count.ToString();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Factory))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private Counter _counter;
    [SerializeField] private float _gizmosRadius;
    [SerializeField] private int _currecntCircleCount;
    [SerializeField] private int _goalCount;
    private int _randomChance;
    private void OnValidate()
    {
        _factory ??= GetComponent<Factory>();
    }

    private void Start()
    {
        StartCoroutine(CreatedCircleTick());
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _gizmosRadius);
    }

    private void GenerateRandomChane()
    {
        _randomChance = Random.Range(0, 100);
    }

    private void CheckCircleCount()
    {
        _currecntCircleCount--;
    }
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private IEnumerator CreatedCircleTick()
    {
        while (true)
        {
            yield return null;

            for (int i = _currecntCircleCount; _currecntCircleCount > _goalCount; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Circle circleCreated = null;
                Circle bonusCircleCreated = null;
                
                _currecntCircleCount++;
                
                GenerateRandomChane();
                
                if (_randomChance < 80)
                {
                    circleCreated = _factory.CircleCreated(new Vector3(Random.Range(-5, 5), Random.Range(-3, 3), Random.Range(-3, 3)), transform).Setup(_counter).SetMove().SetColor(Color.green);
                    circleCreated.OnRemoveCircle += circleCreated.ClickCircle;
                }

                if (_randomChance > 80)
                {
                    circleCreated = _factory.CircleCreated(new Vector3(Random.Range(-10, 10), Random.Range(-3, 3), Random.Range(-2, 2)), transform).Setup(_counter).SetMove().SetColor(Color.red);
                    circleCreated.OnRemoveCircle += RestartLevel;
                }

                if (_randomChance > 90)
                {
                   bonusCircleCreated = _factory.CircleCreated(new Vector3(Random.Range(-10, 10), Random.Range(-3, 3), Random.Range(0, 1)), transform).Setup(_counter).SetColor(Color.yellow);
                   bonusCircleCreated.OnRemoveCircle += bonusCircleCreated.ClickBonusCircle;
                }
                
                if (circleCreated != null) 
                    circleCreated.OnRemoveCircle += CheckCircleCount;
            }
        }
    }
    
    
}
// ExplosionController.cs
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float _maxRadius = 5f;
    [SerializeField] private float _expansionSpeed = 10f;
    [SerializeField] private float _maxForce = 100f;
    [SerializeField] private AnimationCurve _forceFalloffCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    [SerializeField] private LayerMask _affectedLayers = -1; // Все слои

    private ParticleSystem _particleSystem;
    private float _currentRadius = 0f;
    private bool _isExpanding = true;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        // Настраиваем Particle System под параметры взрыва
        var main = _particleSystem.main;
        main.startSpeed = _expansionSpeed * 0.5f;

        var shape = _particleSystem.shape;
        shape.radius = _maxRadius * 0.1f;
    }

    private void OnEnable()
    {
        ApplyExplosionForce();
    }

    private void Update()
    {
        if (!_isExpanding) return;

        _currentRadius += _expansionSpeed * Time.deltaTime;

        if (_currentRadius >= _maxRadius)
        {
            _isExpanding = false;
            //StartCoroutine(DestroyAfterDelay(1f));

            gameObject.SetActive(false);
            return;
        }
        UpdateVisualEffect();
        //ApplyExplosionForce();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
    }

    private void ApplyExplosionForce()
    {
        // Находим все объекты в радиусе взрыва
        Collider[] colliders = Physics.OverlapSphere(transform.position, _maxRadius, _affectedLayers);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float normalizedDistance = Mathf.Clamp01(distance / _maxRadius);

                // Применяем затухание силы по кривой
                float forceMultiplier = _forceFalloffCurve.Evaluate(normalizedDistance);
                float force = _maxForce * forceMultiplier;

                rb.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }

    private void UpdateVisualEffect()
    {
        // Обновляем визуальные параметры Particle System
        if (_particleSystem != null)
        {
            var shape = _particleSystem.shape;
            shape.radius = _currentRadius * 0.5f;
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        // Или возвращаем в пул, если используете pooling для эффектов
    }

    public void Initialize(ExplosionData explosionData)
    {
        transform.position = explosionData.Position;
        _maxRadius = explosionData.Radius;
        _maxForce = explosionData.Force;
        gameObject.SetActive(true);
    }
}
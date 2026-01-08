// ExplosionController.cs
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    [SerializeField] private float _maxRadius = 5f;
    [SerializeField] private float _expansionSpeed = 10f;
    [SerializeField] private float _maxForce = 100f;
    [SerializeField] private AnimationCurve _forceFalloffCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    [SerializeField] private LayerMask _affectedLayers = -1;

    private const float SpeedFactor = 0.5f;
    private const float RadiusFactor = 0.1f;

    private ParticleSystem _particleSystem;
    private float _currentRadius = 0f;
    private bool _isExpanding = true;
   

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        var main = _particleSystem.main;
        main.startSpeed = _expansionSpeed * SpeedFactor;

        var shape = _particleSystem.shape;
        shape.radius = _maxRadius * RadiusFactor;
    }

    private void OnEnable()
    {
        ApplyExplosionForce();
    }

    private void Update()
    {
        if (_isExpanding == false) 
            return;

        _currentRadius += _expansionSpeed * Time.deltaTime;

        if (_currentRadius >= _maxRadius)
        {
            _isExpanding = false;
            gameObject.SetActive(false);
            return;
        }
        UpdateVisualEffect();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
    }

    private void ApplyExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _maxRadius, _affectedLayers);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float normalizedDistance = Mathf.Clamp01(distance / _maxRadius);

                float forceMultiplier = _forceFalloffCurve.Evaluate(normalizedDistance);
                float force = _maxForce * forceMultiplier;

                rb.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }

    private void UpdateVisualEffect()
    {
        if (_particleSystem != null)
        {
            var shape = _particleSystem.shape;
            shape.radius = _currentRadius * RadiusFactor;
        }
    }

    public void Initialize(ExplosionData explosionData)
    {
        transform.position = explosionData.Position;
        _maxRadius = explosionData.Radius;
        _maxForce = explosionData.Force;
        gameObject.SetActive(true);
    }
}
using UnityEngine;

public class Glass : Interactable
{
  [SerializeField] private Transform pushOrigin;
  [SerializeField] private float pushForce = 50f;
  [SerializeField] private float breakableThreshold = 1.0f;
  [SerializeField] private Transform brokenGlassPrefab;

  private Rigidbody _rb;

  protected override void Awake()
  {
    base.Awake();
    _rb = GetComponent<Rigidbody>();
    _rb.isKinematic = true;
  }

  private void OnCollisionEnter(Collision other)
  {
    if (Mathf.Abs(other.relativeVelocity.y) >= breakableThreshold)
    {
      Break();
    }
  }

  private void Break()
  {
    Instantiate(brokenGlassPrefab, transform.position, transform.rotation);
    Destroy(gameObject);
  }

  public override void Activate(Interactor interactor)
  {
    base.Activate(interactor);
    _rb.isKinematic = false;
    Vector3 pushDirection = transform.position - interactor.transform.position;
    _rb.AddForceAtPosition(pushDirection * pushForce, pushOrigin.position, ForceMode.Impulse);
  }

  private bool IsMoving => _rb.linearVelocity.magnitude > 0.01f;

  public override bool CanInteract()
  {
    return base.CanInteract() && !IsMoving;
  }
}

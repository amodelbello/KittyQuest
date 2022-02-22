using UnityEngine;

public class Player : MonoBehaviour {
  private const int GravityScale = 2;
  [SerializeField] private float speed = 18.0f;
  [SerializeField] private float jumpForce = 10.0f;
  private Rigidbody2D _body;
  private BoxCollider2D _box;

  private void Start() {
    _body = GetComponent<Rigidbody2D>();
    _box = GetComponent<BoxCollider2D>();
  }

  private void Update() {
    var deltaX = Input.GetAxis("Horizontal") * speed;

    HandleMove(deltaX);

    var grounded = IsGrounded();
    HandleJump(grounded, deltaX);
  }

  private void HandleMove(float deltaX) {
    var movement = new Vector2(deltaX, _body.velocity.y);
    _body.velocity = movement;
  }

  private bool IsGrounded() {
    var bounds = _box.bounds;
    var max = bounds.max;
    var min = bounds.min;
    var corner1 = new Vector2(max.x, min.y - .1f);
    var corner2 = new Vector2(min.x, min.y - .2f);
    var hit = Physics2D.OverlapArea(corner1, corner2);

    var grounded = hit != null;
    return grounded;
  }

  private void HandleJump(bool grounded, float deltaX) {
    _body.gravityScale =
      grounded && Mathf.Approximately(deltaX, 0) ? 0 : GravityScale;

    if (grounded && Input.GetKeyDown(KeyCode.Space)) {
      _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
  }
}
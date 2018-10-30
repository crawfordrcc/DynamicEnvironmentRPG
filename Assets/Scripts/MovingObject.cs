using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime;
	public LayerMask blockingLayer;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2d;
	private float inverseMoveTime;
	public bool isMoving;
	private bool facing;
	public bool finishingMove;
	public bool paused;

	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
		rb2d = GetComponent<Rigidbody2D>();
		inverseMoveTime = 1f / moveTime;
		isMoving = false;
		facing = true;
		finishingMove = false;
		paused = false;
	}
	
	protected bool Move(int x, int y, out RaycastHit2D hit){
		if((x > 0 && facing) || (x < 0 && !facing))
			Flip();
		Vector2 start = transform.position;
		Vector2 origin = start + boxCollider.offset;
		Vector2 end = start + new Vector2(0.5f*x, 0.5f*y);
		Vector2 extents = boxCollider.size*0.5f;
		Vector2 direction = new Vector2((float)x, (float)y);

		boxCollider.enabled = false;
		//hit = Physics2D.Linecast(start, end, blockingLayer);
		hit = Physics2D.BoxCast(origin, extents, 0.0f, direction, 0.5f, blockingLayer);
		boxCollider.enabled = true;
		if(hit.transform == null ){
			StartCoroutine(SmoothMovement(end));
			return true;
		}
		
		isMoving = false;
		finishingMove = false;
		return false;
	}

	protected IEnumerator SmoothMovement(Vector3 end){
		float remainingDistance = (transform.position - end).sqrMagnitude;
		while (remainingDistance > float.Epsilon){
			Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime);
			rb2d.MovePosition(newPosition);
			remainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
		rb2d.MovePosition(end);
		StopMovement();
	}
	
	public virtual void StopMovement(){
		finishingMove = true;
		isMoving = false;
	}
	
	protected virtual void AttemptMove<T>(int x, int y) where T : Component{
		RaycastHit2D hit;
		bool canMove = Move(x, y, out hit);
		if(hit.transform == null)
			return;
		T hitComponent = hit.transform.GetComponent<T>();
		if (!canMove && hitComponent)
			OnCantMove(hitComponent);
	}

	protected abstract void OnCantMove<T>(T Component) where T : Component;

	private void Flip(){
		facing = !facing;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

}

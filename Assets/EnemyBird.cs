using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyBird : MonoBehaviour {

	public Transform target;

	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	public Path path;

	//bird speed per sec
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	public float nextWaypointDistance = 3;

	private int currentWayPoint = 0;

	private bool searchingForPlayer = false;

	void Start () {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		if (target == null) {

			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}

			return;

		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());

	}

	IEnumerator SearchForPlayer () {
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if (sResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		} else {
			target = sResult.transform;
			searchingForPlayer = false;
			StartCoroutine (UpdatePath ());
			yield return false;
		}
	}

	IEnumerator UpdatePath () {
		if (target == null) {

			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}

			yield return false;

		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds ( 1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete (Path p) {
		Debug.Log ("We god a path. Did it have an error?" + p.error);
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
	}

	void FixedUpdate () {
		if (target == null) {

			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}

			return;

		}

		//TODO: always look at pl

		if (path == null)
			return;

		if (currentWayPoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;

			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		Vector3 dir = (path.vectorPath [currentWayPoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		rb.AddForce (dir, fMode);

		float dist = Vector2.Distance (transform.position, path.vectorPath[currentWayPoint]);
		if (dist < nextWaypointDistance) {
			currentWayPoint++;
			return;
		}

	}
}

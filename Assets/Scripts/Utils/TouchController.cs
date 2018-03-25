using UnityEngine;

public class TouchController
{
	public enum Direction {
		NONE,
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	private float prePosY, prePosX;
	public float threshold = 40.0f;
	public Direction directionX = Direction.NONE;
	public Direction directionY = Direction.NONE;

	public void Update()
	{
		if (Input.touchCount > 0) {
			Touch t = Input.GetTouch(0);

			switch (t.phase) {
			case TouchPhase.Began:
				prePosY = t.position.y;
				prePosX = t.position.x;

				break;

			case TouchPhase.Moved:
				float tempY = t.position.y - prePosY, tempX = t.position.x - prePosX;

				if (Mathf.Abs(tempY) > threshold) {
					if (tempY > 0) {
						directionY = Direction.UP;
					} else if (tempY < 0) {
						directionY = Direction.DOWN;
					}

					prePosY = t.position.y;
				}

				if (Mathf.Abs(tempX) > threshold) {
					if (tempX > 0) {
						directionX = Direction.RIGHT;
					} else if (tempX < 0) {
						directionX = Direction.LEFT;
					}

					prePosX = t.position.x;
				}


				break;

			case TouchPhase.Ended:
				directionX = Direction.NONE;
				directionY = Direction.NONE;

				break;
			}
		}
	}
}

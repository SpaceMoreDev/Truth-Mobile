using UnityEngine;

public class SwipeDetection : MobileControl
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f,1f)]
    private float directionThreshold = .9f;

    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    private void OnEnable()
    {
        OnStartTouch += SwipeStart;
        OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        OnStartTouch -= SwipeStart;
        OnEndTouch -= SwipeEnd;
    }
    void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime - startTime) <= maximumTime )
        {
            print("swiped");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector3 direction2D = new Vector2(direction.x,direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }
    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            print("Swiped UP");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            print("Swiped DOWN");

        }
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            print("Swiped LEFT");

        }
        else if(Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            print("Swiped RIGHT");

        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class screenWarping : MonoBehaviour
{

    public Camera mainCamera;
    BoxCollider2D boxCollider2D;
    public UnityEvent<Collider2D> exitTrigger;
    [SerializeField]
    private float warpOffset = 0.2f;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }
    public void UpdateBoundsSize()
    {
        float y = mainCamera.orthographicSize * 2;
        Vector2 boxColliderSize = new Vector2(y * mainCamera.aspect, y);
        boxCollider2D.size = boxColliderSize;

    }

    private void OgerExit2D(Collider2D collision)
    {
        exitTrigger?.Invoke(collision);
    }

    public bool AmIOutOfBounds(Vector3 worldPosition)
    {
        return
            Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider2D.bounds.min.x) ||
            Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider2D.bounds.min.y);
    }

    public Vector2 CalculateWarpedPosition(Vector2 worldPosition)
    {
        bool xBoundResult = Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider2D.bounds.min.x);
        bool yBoundResult = Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider2D.bounds.min.y);

        Vector2 signVector = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1) + Vector2.Scale(new Vector2(warpOffset, warpOffset), signVector);
        }
        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) + new Vector2(warpOffset * signVector.x, warpOffset);
        }
        else if (yBoundResult)
        {
        return new Vector2(worldPosition.x, worldPosition.y * -1)+ new Vector2(warpOffset,warpOffset * signVector.y);
        }else
        {
            return worldPosition;
        }

    }
    
}

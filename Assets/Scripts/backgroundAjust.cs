using UnityEngine;

[ExecuteAlways]
public class bakcgroundAjust : MonoBehaviour
{
    public Camera Camera;
    public SpriteRenderer SpriteRenderer;
    void Start()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        SpriteRenderer.size = new Vector2(worldScreenWidth, worldScreenHeight);
    }

}

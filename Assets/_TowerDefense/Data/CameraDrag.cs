using UnityEngine;

public class CameraDrag : KennMonoBehaviour
{
    [SerializeField] protected Camera cam;
    [SerializeField] protected SpriteRenderer mapRenderer;
    protected Vector3 dragOrigin;
    protected float mapMinX, mapMaxX, mapMinY, mapMaxY; 

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        this.SetLimitedMap();
    }

    protected virtual void Update()
    {
        this.MoveCamera();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
    }

    protected virtual void MoveCamera()
    {
        if(Input.GetMouseButtonDown(0)) dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(0))
        {
            Vector3 different = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = this.ClampCamera(cam.transform.position + different);
        }
    }

    protected virtual Vector3 ClampCamera(Vector3 target)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = this.mapMinX + camWidth;
        float maxX = this.mapMaxX - camWidth;
        float minY = this.mapMinY + camHeight;
        float maxY = this.mapMaxY - camHeight;

        float newX = Mathf.Clamp(target.x, minX, maxX);
        float newY = Mathf.Clamp(target.y, minY, maxY);

        return new Vector3(newX, newY, target.z);
    }

    protected virtual void SetLimitedMap()
    {
        this.mapMinX = this.mapRenderer.transform.position.x - this.mapRenderer.bounds.size.x / 2f;
        this.mapMaxX = this.mapRenderer.transform.position.x + this.mapRenderer.bounds.size.x / 2f;
        this.mapMinY = this.mapRenderer.transform.position.y - this.mapRenderer.bounds.size.y / 2f;
        this.mapMaxY = this.mapRenderer.transform.position.y + this.mapRenderer.bounds.size.y / 2f;
    }
}

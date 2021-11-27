using UnityEngine;

public class Object : MonoBehaviour
{
    [HideInInspector]
    public Vector3 center = Vector3.zero;
    [SerializeField]
    private Vector3 _offset = Vector3.zero;
    [SerializeField]
    public float height = 1;
    [SerializeField]
    public float width = 1;

    protected void SetCenter()
    {
        center = transform.position + _offset;
    }

    protected void DrawOutLine()
    {
        SetCenter();
        Vector3 size = new Vector3(width, height);
        Gizmos.DrawWireCube(center, size);
    }
}

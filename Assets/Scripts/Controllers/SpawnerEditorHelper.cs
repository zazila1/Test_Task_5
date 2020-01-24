using UnityEngine;

[ExecuteInEditMode]
public class SpawnerEditorHelper : MonoBehaviour
{
    private Spawner _Spawner;

    private void Start()
    {
        _Spawner = GetComponent<Spawner>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        float theta = 0;
        float x = _Spawner._SpawnerRadius * Mathf.Cos(theta);
        float y = _Spawner._SpawnerRadius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, 0, y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = _Spawner._SpawnerRadius * Mathf.Cos(theta);
            y = _Spawner._SpawnerRadius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y, 0);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }
}

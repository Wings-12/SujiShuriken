using UnityEngine;

public class ObjectCreateTest : MonoBehaviour
{
    [SerializeField] GameObject m_prefab;

    void Start()
    {
        if (!m_prefab)
        {
            m_prefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        GameObject go = Instantiate(m_prefab, Vector3.zero, Quaternion.identity);
        Debug.LogFormat("{0} is spawned at {1}", go.name, go.transform.position.ToString());
    }
}
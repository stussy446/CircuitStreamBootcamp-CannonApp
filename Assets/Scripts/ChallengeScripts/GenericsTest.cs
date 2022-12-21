using UnityEngine;
using TMPro;

public class GenericsTest : MonoBehaviour, IGenericsTest
{
    private void CustomAddComponent<T>(GameObject gObject) where T : Component
    {
        gObject.AddComponent<T>();
    }

    private void Awake()
    {
        CustomAddComponent<BoxCollider>(this.gameObject);
        Debug.Log($"added BoxCollider to {this.gameObject} object");

        CustomAddComponent<MeshRenderer>(this.gameObject);
        Debug.Log($"added Meshrenderer to {this.gameObject} object");

        CustomAddComponent<Animator>(this.gameObject);
        Debug.Log($"added Animator to {this.gameObject} object");

        CustomAddComponent<TextMeshPro>(this.gameObject);

        //ParentStuff<Transform>(FindObjectOfType<CannonController>().transform);

    }



    public void Log<T>(T score)
    {
        Debug.Log($"GenericsTest {score}");
    }

    public void ParentStuff<T>(Transform obj)
    {
        gameObject.transform.SetParent(obj);
    }
}

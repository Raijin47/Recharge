using RopeToolkit;
using UnityEngine;
using Unity.Mathematics;
using UnityEditor;

public class StageConnector : MonoBehaviour
{
    public Rope Rope;

    public Base Plug;
    public Base Target;


    [Space(10)]
    public bool Init = false;
    //public Connect[] _connect;
    

    private void OnValidate()
    {
        if(Init)
        {
            var connection = Rope.GetComponents<RopeConnection>();

            connection[0].transformSettings.transform = Plug.Anchor;
            connection[1].transformSettings.transform = Target.Anchor;

            Vector3 point1 = connection[0].transformSettings.transform.position;
            Vector3 point2 = connection[1].transformSettings.transform.position;

            Rope.spawnPoints[0] = new float3(point1.x, point1.y, point1.z - transform.position.z);
            Rope.spawnPoints[1] = new float3(point2.x, point2.y, point2.z - transform.position.z);

            EditorUtility.SetDirty(Rope);
            EditorUtility.SetDirty(Rope.gameObject);


            //foreach (Connect obj in _connect)
            //{
            //    var connection = obj.Rope.GetComponents<RopeConnection>();

            //    connection[0].transformSettings.transform = obj.Plug.Anchor;
            //    connection[1].transformSettings.transform = obj.Target.Anchor;

            //    Vector3 point1 = connection[0].transformSettings.transform.position;
            //    Vector3 point2 = connection[1].transformSettings.transform.position;

            //    obj.Rope.spawnPoints[0] = new float3(point1.x, point1.y, point1.z - transform.position.z);
            //    obj.Rope.spawnPoints[1] = new float3(point2.x, point2.y, point2.z - transform.position.z);

            //    EditorUtility.SetDirty(obj.Rope);
            //    EditorUtility.SetDirty(obj.Rope.gameObject);
            //}
            
            Init = false;
        }
    }
}

[System.Serializable]
public class Connect
{
    public Rope Rope;
    public Plug Plug;
    public Base Target;
}
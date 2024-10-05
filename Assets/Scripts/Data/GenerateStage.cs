using UnityEngine;

public class GenerateStage : MonoBehaviour
{
    [SerializeField] private StageData _data;
    [SerializeField] private Transform _content;

    private void Start()
    {
        Game.Action.OnRestart += Generate;
    }


    private void Generate()
    {
        for(int i = 0; i < _data.Plug.Length; i++)
        {
            if(_data.Plug[i].Phone != null)
            {

            }
            var phone = Instantiate(_data.Plug[i].Phone.Phone, _content);
            phone.transform.SetLocalPositionAndRotation(_data.Plug[i].Phone.StartPos, Quaternion.Euler(_data.Plug[i].Phone.StartRot));
            
            var Plug = Instantiate(_data.Plug[i].Plug, _content);
            Plug.transform.SetLocalPositionAndRotation(_data.Plug[i].StartPos, Quaternion.Euler(_data.Plug[i].StartRot));

            Plug.Target = phone;
        }




    }



}
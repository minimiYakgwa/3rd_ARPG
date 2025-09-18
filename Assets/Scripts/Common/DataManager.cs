using UnityEngine;
using System.IO;
public class DataManager : SingletonBehaviour<DataManager>
{

    [SerializeField]
    class Wrapper<T>
    {
        public T[] data;
    }


    protected override void Init()
    {
        base.Init();

        
    }
    private const string Data_Path = "DataTable";
    private T[] LoadDataFronJson<T>(string filename)
    {
        var path = Path.Combine(Data_Path, filename);
        var json = Resources.Load<TextAsset>(path);
        var wrraper = JsonUtility.FromJson<Wrapper<T>>(json.text);
        return wrraper.data;
    }
}

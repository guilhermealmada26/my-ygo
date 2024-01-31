using UnityEngine;

namespace BBG.Saving
{
    [CreateAssetMenu(menuName = "Saving/JsonFileSaver")]
    public class JsonFileSaver : FileSaver
    {
        [SerializeField] bool prettyPrint;

        protected override T Deserialize<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }

        protected override string Serialize(object data)
        {
            return JsonUtility.ToJson(data, prettyPrint);
        }
    }
}
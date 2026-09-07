using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "DeviceConfig", menuName = "Config/DeviceConfig")]
    public class DeviceConfig : ScriptableObject
    {
        public string Id;
        public string displayName;
        public Sprite icon;
        public DeviceType deviceType;
        public GameObject DevicePrefab;
        public int Operationtime;
        public List<string> InputGemIds = new List<string>();
        public List<string> OutputGemIds = new List<string>();
    }
}
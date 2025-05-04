using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public LevelController levelController;
    public GameObject Player;
    public Dictionary<string,GameObject> maps = new Dictionary<string, GameObject>();

    private void Awake()
    {
        levelController = LevelController.Instance;

        //Ìí¼ÓµØÍ¼½ø×Öµä
        maps.Add("Meadow", GameObject.Find("Meadow"));
        maps.Add("City", GameObject.Find("City"));

    }

    public void GameStart()
    {
        ChangeMap(levelController.NowMap);
    }

    public void ChangeMap(string mapName)
    {
        foreach(KeyValuePair<string,GameObject> pair in maps)
        {
            if (pair.Key == mapName)
            {
                pair.Value.SetActive(true);
                Transform transform = pair.Value.transform;
                Player.transform.position = new Vector3
                    (transform.position.x,transform.position.y-10,Player.transform.position.z);
            }
            else
            {
                pair.Value.SetActive(false);
            }

            Debug.Log(pair.Key);
        }
    }
}

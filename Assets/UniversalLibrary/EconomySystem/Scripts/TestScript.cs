using Datasaver;
using Resources.Ui;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Resources.Test
{
    public class TestScript : MonoBehaviour
    {
        public ItemFiller prefab;
        public ResourceManager resourceManager;
        public GameObject Parrent;

        private bool Is = false;

        DataSaver saver = new DataSaver();
        public string path;


        private void Start()
        {
            path = Application.persistentDataPath + "save.json";
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InitResources();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                foreach (Resource res in resourceManager.Resources)
                {
                    res.AddValue(10);
                }
            }

            List<Task> tasks = new List<Task>();

            if (Input.GetKeyDown(KeyCode.D))
            {
                saver.Serialize<List<Resource>>(resourceManager.Resources, path);
                //tasks.Add(saver.SerializeAsync<List<Resource>>(resourceManager.Resources, path));
                //Task.WhenAll(tasks).ContinueWith(t => { });
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                resourceManager.AddResources(saver.Deserialize<Resource>(path));

                foreach (Resource resource in resourceManager.Resources)
                {
                    var b = Instantiate(prefab, Parrent.transform);
                    b.Init(resource);
                }
            }
        }

        private void InitResources()
        {
            var value = Enum.GetValues(typeof(ResourceTypeEnum));

            foreach (var a in value)
            {
                Debug.Log(a.ToString());
                Resource resource = new Resource((ResourceTypeEnum)a);
                resourceManager.AddResource(resource);
                ItemFiller b = Instantiate(prefab, Parrent.transform);
                b.Init(resource);
            }
        }
    }
}
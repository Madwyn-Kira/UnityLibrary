using Datasaver;
using Resources.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Resources
{
    public class ResourceManager : MonoBehaviour
    {
        public List<Resource> Resources;
        
        public GameObject parent;
        public ItemFiller prefab;

        private DataSaver saver = new DataSaver();
        private string path;

        [SerializeField] private List<ResourceTypeEnum> _defaultResources;

        private void Awake()
        {
            path = Application.persistentDataPath + "ResourceManager.json";

            LoadResources();
        }

        /// <summary>
        /// Добавляет ресурс в список ресурсов (если его еще нет)
        /// </summary>
        /// <param name="resource"></param>
        /// <exception cref="Exception"></exception>
        public void AddResource(Resource resource)
        {
            if (Resources is null)
            {
                Resources = new List<Resource>();
            }

            if (Resources.Where(r => r.Type == resource.Type).Count() == 0)
            {
                Resources.Add(resource);
            }
        }

        /// <summary>
        /// Добавляет список ресурсов в список
        /// </summary>
        /// <param name="resources"></param>
        public void AddResources(List<Resource> resources)
        {
            foreach (Resource resource in resources)
            {
                AddResource(resource);
            }
        }

        /// <summary>
        /// Списывает ресурсы из инвентаря
        /// </summary>
        /// <param name="cost"></param>
        public void ChargeResources(List<Payment> cost)
        {
            if (IsCanCharged(cost))
            {
                foreach (Payment p in cost)
                {
                    Resources.First(r => r.Type == p.Type).RemoveValue((int)p.Cost);
                }
            }
        }

        /// <summary>
        /// Проверяет можно ли провести транзакцию
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool IsCanCharged(List<Payment> cost)
        {
            bool result = true;

            foreach (Payment p in cost) 
            {
                Resource resource = Resources.FirstOrDefault(r => r.Type == p.Type);
                if (resource == null || !resource.IsCanRemove((int)p.Cost))
                {
                    result = false;
                }
            }

            return result;
        }

        private void InstantiateItems()
        {
            foreach (Resource res in Resources)
            {
                var item = Instantiate(prefab, parent.transform);
                item.Init(res);
            }
        }

        private void LoadResources()
        {
            if (File.Exists(path))
            {
                AddResources(saver.Deserialize<Resource>(path));
            }
            else
            {
                if (_defaultResources != null)
                {
                    foreach (var res in _defaultResources)
                    {
                        AddResource(new Resource(res));
                    }
                }
            }
            InstantiateItems();
        }

        private void OnDestroy()
        {
            saver.Serialize(Resources, path);
        }
    }

    [Serializable]
    public struct Payment
    {
        public ResourceTypeEnum Type;
        public uint Cost;
    }
}
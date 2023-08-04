//-------------------------------------------------------------------------------------
//	PoolManager.cs
//
//	Created by 浅墨
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace ObjectPoolPatternExample
{

    /// <summary>
    /// 对象池类
    /// </summary>
    public class Pool
    {
        private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();
        private bool fixedSize;
        private GameObject poolObjectPrefab;
        private int poolSize;
        private string poolName;
        private Transform parent;

        public Pool(string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize, Transform parent)
        {
            this.poolName = poolName;
            this.poolObjectPrefab = poolObjectPrefab;
            this.poolSize = initialCount;
            this.fixedSize = fixedSize;
            this.parent = parent;

            for (int index = 0; index < initialCount; index++)
            {
                AddObjectToPool(NewObjectInstance());
            }
        }

        private void AddObjectToPool(PoolObject po)
        {
            //add to pool
            po.gameObject.SetActive(false);
            availableObjStack.Push(po);
            po.IsPooled = true;
        }

        private PoolObject NewObjectInstance()
        {
            GameObject go = (GameObject)GameObject.Instantiate(poolObjectPrefab, parent);
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                po = go.AddComponent<PoolObject>();
            }
            //set name
            po.PoolName = poolName;
            return po;
        }

        public PoolObject NextAvailableObject(Vector3 position, Quaternion rotation)
        {
            PoolObject po = null;

            if (availableObjStack.Count > 0)
            {
                po = availableObjStack.Pop();
            }

            else if (fixedSize == false)
            {
                //increment size var, this is for info purpose only
                poolSize++;
                //Debug.Log(string.Format("Growing pool {0}. New size: {1}", poolName, poolSize));
                //create new object
                po = NewObjectInstance();
            }
            else
            {
                Debug.LogWarning("No object available & cannot grow pool: " + poolName);
            }

            GameObject result = null;
            if (po != null)
            {
                po.IsPooled = false;
                result = po.gameObject;
                result.SetActive(true);

                result.transform.position = position;
                result.transform.rotation = rotation;
            }

            return po;

        }

        public void ReturnObjectToPool(PoolObject po)
        {

            if (poolName.Equals(po.PoolName))
            {

                // We could have used availableObjStack.Contains(po) to check if this object is in pool.
                // While that would have been more robust, it would have made this method O(n) 
                if (po.IsPooled)
                {
                    Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");
                }
                else
                {
                    AddObjectToPool(po);
                }

            }
            else
            {
                Debug.LogError(string.Format("Trying to add object to incorrect pool {0} {1}", po.PoolName, poolName));
            }
        }
    }

    /// <summary>
    /// PoolManager
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager instance;
        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PoolManager();
                }
                return instance;
            }
        }

        private Dictionary<string, Pool> poolMap = new Dictionary<string, Pool>();
        public Dictionary<string, Pool> PoolMap
        {
            get { return poolMap; }
            set { poolMap = value; }
        }

        [Header("[Pool Info]")]
        public PoolInfo[] poolInfo;

        PoolManager()
        {
            //set instance
            instance = this;
        }

        public void Init()
        {
            CheckForDuplicatePoolNames();
            CreatePools();
        }

        private void CheckForDuplicatePoolNames()
        {
            for (int index = 0; index < poolInfo.Length; index++)
            {
                string poolName = poolInfo[index].poolName;
                if (poolName.Length == 0)
                {
                    Debug.LogError(string.Format("Pool {0} does not have a name!", index));
                }
                for (int internalIndex = index + 1; internalIndex < poolInfo.Length; internalIndex++)
                {
                    if (poolName.Equals(poolInfo[internalIndex].poolName))
                    {
                        Debug.LogError(string.Format("Pool {0} & {1} have the same name. Assign different names.", index, internalIndex));
                    }
                }
            }
        }

        private void CreatePools()
        {
            foreach (PoolInfo currentPoolInfo in poolInfo)
            {

                Pool pool = new Pool(currentPoolInfo.poolName, currentPoolInfo.prefab, currentPoolInfo.poolSize, currentPoolInfo.fixedSize, currentPoolInfo.parent);

                Debug.Log("Creating Pool: " + currentPoolInfo.poolName);

                //add to mapping dict
                poolMap[currentPoolInfo.poolName] = pool;
            }
        }

        public PoolObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation)
        {
            //GameObject result = null;
            PoolObject result = null;

            if (poolMap.ContainsKey(poolName))
            {
                Pool pool = poolMap[poolName];
                result = pool.NextAvailableObject(position, rotation);
                //scenario when no available object is found in pool
                if (result == null)
                {
                    Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
                }

            }
            else
            {
                Debug.LogError("Invalid pool name specified : " + poolName);
            }

            return result;
        }

        public void ReturnObjectToPool(PoolObject po)
        {
            //PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                Debug.LogWarning("Specified object is not a pooled instance: " + po.name);
            }
            else
            {
                if (poolMap.ContainsKey(po.PoolName))
                {
                    Pool pool = poolMap[po.PoolName];
                    pool.ReturnObjectToPool(po);
                }
                else
                {
                    Debug.LogWarning("No pool available with name: " + po.PoolName);
                }
            }
        }
    }

    [System.Serializable]
    public class PoolInfo
    {
        public string poolName;
        public GameObject prefab;
        public int poolSize;
        public bool fixedSize;
        public Transform parent;
    }
}

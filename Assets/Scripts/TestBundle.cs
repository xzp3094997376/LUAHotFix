using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBundle : MonoBehaviour
{
    public GameObject c1;
    public GameObject c2;
    public GameObject sp1;
    public GameObject sp2;
    // Start is called before the first frame update
    void Start()
    {
        AssetBundleManager.serverURL = "http://127.0.0.1:80";
        CheckData();

       Object _obj=gameObject;
       Transform t=_obj as Transform; 
       Debug.Log(t);
    }
    void CheckData()
    {
        StartCoroutine(AssetBundleManager.Instance.CheckUpgrade(ret =>
        {
            if (ret)
            {
                AssetBundleManager.Instance.Init(() =>
                {
                    Debug.Log("资源检查完毕");
                });
            }
        }
           )
           );
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AssetBundleManager.Instance.LoadInstance<Transform>("cube","Cube",trans=> { 
            if(trans!=null)
                {
                    c1 = trans.gameObject;
                    trans.SetParent(transform);
                }
            });
            AssetBundleManager.Instance.LoadInstance<Transform>("cube", "Cube", trans =>
            {
                if (trans != null)
                {
                    c2 = trans.gameObject;
                    trans.SetParent(transform);
                }
            });
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AssetBundleManager.Instance.LoadInstance<Transform>("sp", "sp1", trans =>
            {
                if (trans != null)
                {
                    trans.SetParent(transform);
                    sp1 = trans.gameObject;
                }
            });
            AssetBundleManager.Instance.LoadInstance<Transform>("sp", "sp2", trans =>
            {
                if (trans != null)
                {
                    trans.SetParent(transform);
                    sp2 = trans.gameObject;
                }
            });
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(c1!=null)
            {
                DestoryAB(c1,"cb");
            }
            if (c2 != null)
            {
                DestoryAB(c2, "cb");
            }          
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (sp1 != null)
            {
                DestoryAB(sp1, "sp");
            }
            if (sp2 != null)
            {
                DestoryAB(sp2, "sp");
               
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            //该函数的主要作用是查找并卸载不再使用的资源。游戏场景越复杂、资源越多，该函数的开销越大 
            Resources.UnloadUnusedAssets();
            
        }
    }
    private void DestoryAB(GameObject go,string assetName)
    {
        GameObject.Destroy(go);
        AssetBundleManager.UnloadAssetBundle(assetName, true);
        Resources.UnloadUnusedAssets();
    }
}

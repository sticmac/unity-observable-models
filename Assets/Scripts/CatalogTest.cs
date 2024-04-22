using System.Collections;
using System.Collections.Generic;
using Sticmac.ObservableModels.Catalogs;
using UnityEngine;

public class CatalogTest : MonoBehaviour
{
    [SerializeField] private ObservableModelsCatalog<string, int> _catalog;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_catalog["testkey"]);
    }
}

using System.Collections;
using System.Collections.Generic;
using Sticmac.ObservableModels.Collections;
using UnityEngine;

public class ListTest : MonoBehaviour
{
    [SerializeField] private ObservableListModel<int> _list;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _list.Add(1);
        _list.Add(2);
    }

    void OnEnable()
    {
        _list.OnValueChanged += OnListChanged;
    }

    void OnDisable()
    {
        _list.OnValueChanged -= OnListChanged;
    }

    private void OnListChanged(IList<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
    }
}

using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] private GameObject _doorLeaf;
    private bool isOpened;
    
    public void Open()
    {
        if (!isOpened)
        {
            _doorLeaf.gameObject.SetActive(false);
            isOpened = true;
        }
    }

    public void Close()
    {
        if (isOpened)
        {
            _doorLeaf.gameObject.SetActive(true);
            isOpened = false;
        }
    }
}
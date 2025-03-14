using Unit;
using UnityEngine;
using UnityEngine.UI;

public class EntitySelect : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private OptionSelectedGrid optionsGrid;
    private Button button;
    private IInteractable interactable;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }


    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        optionsGrid.RemoveAll();
        if (interactable != null)
        {
            optionsGrid.Init(interactable.Actions); 
        }
    }


    public void SetInteractable(IInteractable interactable)
    {
        this.interactable = interactable;
        icon.sprite = interactable.Icon;

    }

    public void Remove()
    {
        icon.sprite = null;
        interactable = null;
    }

}

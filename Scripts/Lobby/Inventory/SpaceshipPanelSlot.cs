using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Controls slots in the spaceship panel
/// </summary>
namespace RoSS
{
    public class SpaceshipPanelSlot : MonoBehaviour, IDropHandler, IInventoryItemOwner
    {
        public Image Icon;
        public ItemSO ItemSO;
        //public ItemTypeSO Type;
        public ItemType Type;
        SpaceshipPanelItemState _state;

        [SerializeField] GameObject _frameGO;
        [SerializeField] GameObject _frameFocusGO;
        [SerializeField] GameObject _iconGO;

        public void AddItem(InventoryItem item)
        {
            item.transform.SetParent(gameObject.transform);
            item.transform.position = transform.position;
            item.SetOwner(this);
            ItemSO = item.GetItemSO();
            UpdateState();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (_state != SpaceshipPanelItemState.Active) return;
            if (!eventData.pointerDrag.TryGetComponent<InventoryItem>(out var inventoryItem)) return;
            if (!inventoryItem.TryGetComponent<DragAndDrop>(out var dragableItem)) return;

            AddItem(inventoryItem);
        }
        public void UpdateState()
        {

            if (ItemSO != null) _state = SpaceshipPanelItemState.Disabled;
            else if (Type == InventorySystem.Instance.ActiveTab.Type) _state = SpaceshipPanelItemState.Active;
            else _state = SpaceshipPanelItemState.Enabled;
            UpdateView();
        }

        public void SetActive(bool active)
        {
            if (active) _state = (Type == InventorySystem.Instance.ActiveTab.Type ? SpaceshipPanelItemState.Active : SpaceshipPanelItemState.Enabled);
            else _state = _state = SpaceshipPanelItemState.Disabled;
            UpdateView();
        }

        void UpdateView()
        {
            switch (_state)
            {
                case SpaceshipPanelItemState.Enabled:
                    _frameGO.SetActive(true);
                    _frameFocusGO.SetActive(false);
                    _iconGO.SetActive(true);
                    break;

                case SpaceshipPanelItemState.Active:
                    _frameGO.SetActive(true);
                    _frameFocusGO.SetActive(true);
                    _iconGO.SetActive(true);
                    break;

                case SpaceshipPanelItemState.Disabled:
                    _frameGO.SetActive(false);
                    _frameFocusGO.SetActive(false);
                    _iconGO.SetActive(false);
                    break;
            }
        }


    }
    public enum SpaceshipPanelItemState
    {
        Disabled,   // not visible
        Enabled,    //visible, but not active 
        Active      //visible and active
    }

}
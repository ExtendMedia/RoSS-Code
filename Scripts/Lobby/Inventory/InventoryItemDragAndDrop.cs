using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Drag and drop functionality for inventory items
/// </summary>
namespace RoSS
{
    public class InventoryItemDragAndDrop : DragAndDrop
    {
        [SerializeField] InventoryItem _inventoryItem;
        [SerializeField] Image _image;

        IInventoryItemOwner _beginDragOwner;
        Transform _beginDragParent;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            _beginDragOwner = _inventoryItem.GetOwner();
            if (IsSpaceshipPanelSlot(_beginDragOwner))
                SetSpaceshipPanelSlotActive(_beginDragOwner, true);
            if (_image != null) _image.raycastTarget = false;
            _beginDragParent = transform.parent;
            gameObject.transform.SetParent(InventorySystem.Instance.draggingLayer);

        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (_image != null) _image.raycastTarget = true;
            IInventoryItemOwner newOwner = _inventoryItem.GetOwner();
            if (_beginDragOwner == newOwner)
            {
                transform.SetParent(_beginDragParent);
                if (IsSpaceshipPanelSlot(_beginDragOwner))
                    SetSpaceshipPanelSlotActive(_beginDragOwner, false);
                base.OnEndDrag(eventData);
            }
            else
            {
                if (IsSpaceshipPanelSlot(_beginDragOwner))
                {
                    (_beginDragOwner as SpaceshipPanelSlot).ItemSO = null;
                    SetSpaceshipPanelSlotActive(_beginDragOwner, true);
                }
            }
        }

        bool IsSpaceshipPanelSlot(IInventoryItemOwner owner) => _beginDragOwner is SpaceshipPanelSlot;
        void SetSpaceshipPanelSlotActive(IInventoryItemOwner owner, bool active) => (owner as SpaceshipPanelSlot).SetActive(active);

    }
}
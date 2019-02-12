using static Assets.Scripts.Items.ItemSO;

namespace Assets.Scripts.Items
{
    public interface IItem
    {
        int Id { get; }        
        string Name { get; }
        bool IsStackable { get; }
        GearSlot Slot { get; }

        void Use();
    }
}

using System.Collections.Generic;

[System.Serializable]
public static class SerializationUtility
{
    public static SerializableItemGrid ToSerializable(ItemGrid grid)
    {
        var serializableGrid = new SerializableItemGrid();
        //foreach (var item in grid.GetAllItems())
        return serializableGrid;
    }

    public static ItemGrid FromSerializable(SerializableItemGrid serializableGrid)
    {
        var itemGrid = new ItemGrid();

        return itemGrid;
    }
}

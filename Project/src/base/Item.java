package base;

import virologist.Virologist;

/**
 * An abstract class representing some item. The item can be picked
 * up and dropped.
 */
public abstract class Item {
    /**
     * Decide what to happen when the virologist picks up the item.
     *
     * @param virologist the virologist who picks up the item
     */
    public abstract void onPickUp(Virologist virologist);

    /**
     * Decide what to happen when the virologist drops the item.
     *
     * @param virologist the virologist who drops the item
     */
    public abstract void onDrop(Virologist virologist);
}

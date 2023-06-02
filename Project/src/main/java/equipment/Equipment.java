package equipment;

import base.Id;
import base.Item;
import virologist.Virologist;

/**
 * An abstract Item representing an equipment.
 */
public abstract class Equipment extends Item {
    /**
     * The unique id of the equipment.
     */
    final Id id = new Id();

    /**
     * Apply the effect of the equipment to a virologist.
     *
     * @param v the virologist to apply the effect to
     */
    abstract void applyEffect(Virologist v);

    /**
     * Remove the effect of the equipment from the virologist
     *
     * @param v the virologist to remove the effect from
     */
    abstract void removeEffect(Virologist v);

    @Override
    public void onPickUp(Virologist virologist) {
        virologist.getInventory().addEquipment(this);
        applyEffect(virologist);
    }

    @Override
    public void onDrop(Virologist virologist) {
        virologist.getInventory().removeEquipment(this);
        removeEffect(virologist);
    }
}

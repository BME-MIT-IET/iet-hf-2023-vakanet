package equipment;

import effect.Capacity;
import virologist.Virologist;

/**
 * Equipment class representing a bag. The Capacity effect is applied to it's owner.
 */
public class Bag extends Equipment {
    /**
     * The capacity effect of the bag.
     */
    final Capacity c = new Capacity();

    @Override
    public String toString() {
        return "Bag#" + id.getId();
    }

    @Override
    void applyEffect(Virologist v) {
        c.applyEffect(v);
    }

    @Override
    void removeEffect(Virologist v) {
        c.removeEffect(v);
    }
}

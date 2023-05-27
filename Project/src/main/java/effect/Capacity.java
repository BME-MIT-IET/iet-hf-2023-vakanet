package effect;

import base.Id;
import virologist.Virologist;

/**
 * An effect that increases the Ingredient-capacity of the Virologist
 */
public class Capacity extends Effect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    public Capacity() {
        super("Extra Capacity");
    }

    @Override
    void apply(Virologist v) {
        // double the ingredient capacity of the virologist
        v.getInventory().getIngredients().doubleCapacity();
    }

    @Override
    void remove(Virologist v) {
        // undo applyEffect
        v.getInventory().getIngredients().halveCapacity();
    }

    @Override
    public Id getId() {
        return id;
    }
}

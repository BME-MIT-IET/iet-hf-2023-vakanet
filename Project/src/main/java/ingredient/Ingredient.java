package ingredient;

import base.GameException;
import base.Id;
import base.Item;
import virologist.Virologist;

/**
 * Abstract Item class that represents a certain amount of material.
 */
public abstract class Ingredient extends Item {
    /**
     * Name of the ingredient.
     */
    final String name;
    /**
     * The amount of material.
     */
    int quantity;

    Ingredient(String name, int quantity) {
        this.name = name;
        this.quantity = quantity;
    }

    public int getQuantity() {
        return quantity;
    }

    /**
     * Increase the quantity of the ingredient.
     *
     * @param quantity the amount to increase with
     */
    public void add(int quantity) {
        this.quantity += quantity;
    }

    /**
     * Reduce the quantity of the ingredient.
     *
     * @param quantity the amount to decrease with
     */
    protected void subtract(int quantity) {
        if (quantity > this.quantity)
            throw new GameException("tried to remove " + quantity + " " + name + " but there is only " + this.quantity);
        this.quantity -= quantity;
    }

    /**
     * Remove an amount of ingredient.
     *
     * @param quantity amount to remove
     * @return the amount of removed ingredient
     */
    abstract Ingredient remove(int quantity);

    @Override
    public String toString() {
        return name + " " + quantity;
    }

    @Override
    public void onPickUp(Virologist virologist) {
        virologist.getInventory().addIngredient(this);
    }

    @Override
    public void onDrop(Virologist virologist) {
        // no need for dropping, picking up modifies the quantity
        // if the remaining quantity is 0 remove self
        if (quantity == 0) {
            virologist.getInventory().removeIngredient(this);
        }
    }

    public abstract Id getId();
}

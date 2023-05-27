package city;

import effect.BearDance;
import gui.Window;
import gui.game.GameScene;
import ingredient.Ingredient;
import virologist.Virologist;

/**
 * A field modeling a warehouse. The warehouse can have an ingredient on it
 */
public class Warehouse extends Field {
    /**
     * The warehouse *might* have an ingredient
     */
    Ingredient ingredient;

    /**
     * Constructor for warehouse with ingredient.
     *
     * @param i the ingredient in the warehouse
     */
    public Warehouse(Ingredient i) {
        super();
        ingredient = i;
    }

    /**
     * If there is an ingredient, give it to the virologist.
     *
     * @param v the virologist
     */
    @Override
    public void addItemTo(Virologist v) {
        if (ingredient != null && ingredient.getQuantity() != 0) {
            GameScene gs = Window.getGameScene();
            if (gs != null)
                gs.logAction(ingredient + " was picked up by  " + v.getCharacterName());
            ingredient.onPickUp(v);
        }

    }

    /**
     * Add a virologist to the field, and if the virologist is infected with a bearvirus, the ingredient gets destroyed.
     *
     * @param virologist the virologist to add
     */
    @Override
    public void addVirologist(Virologist virologist) {
        super.addVirologist(virologist);
        if (virologist.getAppliedEffects().contains(new BearDance())) ingredient = null; // destroy ingredient
    }

    @Override
    public String toString() {
        return "Warehouse#" + id.getId();
    }
}

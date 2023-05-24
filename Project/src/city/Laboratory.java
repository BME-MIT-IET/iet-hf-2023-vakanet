package city;

import agent.Recipe;
import base.Game;
import gui.Window;
import gui.game.GameScene;
import virologist.Virologist;

/**
 * A field modeling a laboratory. In each laboratory there is a recipe.
 */
public class Laboratory extends Field {
    /**
     * The recipe of the laboratory.
     */
    final Recipe recipe;

    /**
     * Constructor, set the recipe of the laboratory
     *
     * @param recipe the recipe in the laboratory
     */
    public Laboratory(Recipe recipe) {
        super();
        this.recipe = recipe;
        Game.getInstance().addRecipe(recipe); // register recipe
    }

    /**
     * Add the recipe to the inventory of the virologist.
     *
     * @param v the virologist to give the item to
     */
    @Override
    public void addItemTo(Virologist v) {
        GameScene gs = Window.getGameScene();
        if (gs != null)
            gs.logAction(recipe + " was successfully learned by " + v.getCharacterName());

        recipe.onPickUp(v);
        Game.getInstance().checkWinner(v);
    }

    @Override
    public String toString() {
        return "Laboratory#" + id.getId();
    }
}

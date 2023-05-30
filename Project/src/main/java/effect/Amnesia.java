package effect;

import base.Id;
import virologist.Virologist;

/**
 * An effect representing amnesia. It causes the attacked virologist
 * to forget all previously learned genetic codes
 */
public class Amnesia extends Effect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    public Amnesia() {
        super("Amnesia");
    }

    @Override
    public void applyEffect(Virologist ro) {
        // forget all recipes
        ro.getRecipes().forEach(r -> ro.removeRecipe(r));
    }

    @Override
    public Id getId() {
        return id;
    }
}

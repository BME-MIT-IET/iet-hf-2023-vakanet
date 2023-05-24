package agent;

import base.GameException;
import base.Id;
import base.Item;
import effect.Effect;
import ingredient.Ingredient;
import virologist.ItemCollection;
import virologist.Virologist;

import java.util.stream.Collectors;

/**
 * A class representing a recipe. With the recipe, the Virologist can either create
 * a Virus, causing the given Effect, or a Vaccine against that Virus.
 */
public class Recipe extends Item {
    /**
     * The required amount of ingredients to create the agent from the recipe.
     */
    final ItemCollection<Ingredient> required;
    final Id id = new Id();
    /**
     * The Effect which the created Virus causes.
     */
    private final Class<? extends Effect> effect;
    /**
     * The name of the Effect which the created Virus causes.
     */
    private final String effectName;

    public Recipe(Class<? extends Effect> effect, ItemCollection<Ingredient> required) {
        this.effect = effect;
        var instance = Effect.getInstance(effect);
        effectName = instance.getName();
        this.required = required;
    }

    /**
     * Try to remove the required ingredients from the available ingredients.
     * If the available ingredients are not enough, they are left unmodified, and an exception is thrown.
     *
     * @param available the available ingredients
     */
    public void trySubtractRequired(ItemCollection<Ingredient> available) {
        var needed = required.getView()
                .stream()
                .collect(Collectors.toMap(Ingredient::getId, i -> i));

        var avail = available.getView()
                .stream()
                .collect(Collectors.toMap(Ingredient::getId, i -> i));

        // check if there is enough
        needed.forEach((id, i) -> {
            if (!avail.containsKey(id) || avail.get(id).getQuantity() < i.getQuantity()) {
                throw new GameException(i + " is required");
            }
        });

        try {
            for (var i : required.getView()) available.remove(i);
        } catch (GameException e) {
            throw new RuntimeException("Something went very wrong: " + e.getMessage());
        }

    }

    /**
     * Try to create the Virus causing the given effect.
     * If the available ingredients aren't enough, an exception is thrown.
     *
     * @param available the available ingredients
     * @return the virus instance
     */
    public Virus createVirus(ItemCollection<Ingredient> available) {
        trySubtractRequired(available);
        return new Virus(effect);
    }

    /**
     * Try to create the Vaccine instance, preventing the matching Virus.
     * If the available ingredients aren't enough, an exception is thrown.
     *
     * @param available the available ingredients
     * @return the vaccine instance
     */
    public Vaccine createVaccine(ItemCollection<Ingredient> available) {
        trySubtractRequired(available);
        return new Vaccine(effect);
    }


    @Override
    public String toString() {
        return effectName + " recipe#" + id.getId();
    }

    @Override
    public void onPickUp(Virologist virologist) {
        virologist.getInventory().addRecipe(this);
    }

    @Override
    public void onDrop(Virologist virologist) {
        virologist.getInventory().removeRecipe(this);
    }

    public Id getId() {
        return id;
    }
}

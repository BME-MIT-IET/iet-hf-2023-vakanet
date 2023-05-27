package ingredient;

import base.Id;
import virologist.ItemCollection;

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.stream.Collectors;

/**
 * Implementation of the ItemCollection for Ingredients.
 */
public class IngredientMap implements ItemCollection<Ingredient> {
    /**
     * The ingredients are stored in a map, one instance of each ingredient.
     */
    final Map<Id, Ingredient> ingredients = new HashMap<>();

    /**
     * The maximum capacity for each ingredient.
     */
    private int capacity = 100;

    public IngredientMap() {
    }

    /**
     * Constructor for silent initialization.
     *
     * @param init initial state
     */
    public IngredientMap(Collection<Ingredient> init) {
        init.forEach(i -> ingredients.put(i.getId(), i));
    }

    @Override
    public void add(Ingredient item) {
        // if the entry doesn't exist, insert an instance with 0 quantity
        if (!ingredients.containsKey(item.getId())) {
            ingredients.put(item.getId(), item.remove(0));
        }

        final var ingredient = ingredients.get(item.getId());

        // determine the maximum amount of ingredient that can be added
        final var removed = item.remove(Math.min(item.quantity, capacity - ingredient.quantity));
        System.out.println("Collected " + removed);

        ingredient.add(removed.quantity);
    }

    @Override
    public Ingredient remove(Ingredient item) {
        // if the entry doesn't exist, insert an instance with 0 quantity
        if (!ingredients.containsKey(item.getId())) {
            ingredients.put(item.getId(), item.remove(0));
        }

        // if more is removed than there is an exception is thrown
        final var removed = ingredients.get(item.getId()).remove(item.quantity);

        // remove if 0 quantity
        if (ingredients.get(item.getId()).quantity == 0) {
            ingredients.remove(item.getId());
        }

        System.out.println("Removed " + removed);
        return removed;
    }

    @Override
    public Collection<Ingredient> getView() {
        return Collections.unmodifiableCollection(ingredients.values());
    }

    /**
     * Multiply the capacity by 2. This is needed for the Capacity Effect.
     */
    public void doubleCapacity() {
        capacity *= 2;
    }

    /**
     * Divide the capacity by 2. This is needed for the Capacity Effect.
     */
    public void halveCapacity() {
        capacity /= 2;
    }

    @Override
    public String toString() {
        return ingredients.values()
                .stream()
                .map(Object::toString)
                .collect(Collectors.joining(", "));
    }
}

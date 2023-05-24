package agent;

import virologist.ItemCollection;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;
import java.util.stream.Collectors;

/**
 * Implementation of ItemCollection for Recipes. Only one reference of each Recipe is stored
 * so the internal collection is a set.
 */
public class RecipeSet implements ItemCollection<Recipe> {
    /**
     * The set of recipes.
     */
    final Set<Recipe> recipes = new HashSet<>();

    @Override
    public void add(Recipe item) {
        if (recipes.add(item)) {
            System.out.println("Collected " + item);
        }
    }

    @Override
    public Recipe remove(Recipe item) {
        if (!recipes.remove(item)) {
            throw new IllegalArgumentException("No such Recipe");
        }

        System.out.println("Removed " + item);
        return item;
    }

    @Override
    public Collection<Recipe> getView() {
        return Collections.unmodifiableCollection(recipes);
    }

    @Override
    public String toString() {
        return recipes.stream()
                .map(Object::toString)
                .collect(Collectors.joining(", "));
    }

}

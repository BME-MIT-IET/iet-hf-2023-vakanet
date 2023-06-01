package shell;

import agent.Recipe;
import base.GameException;
import effect.Amnesia;
import effect.BearDance;
import effect.Chorea;
import effect.Paralysis;
import ingredient.AminoAcid;
import ingredient.IngredientMap;
import ingredient.Nucleotide;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Supplier;

/**
 * A class to help with creating Recipe-s.
 */
public class RecipeCreator {
    /**
     * Assignments of string representations of effects, to functions
     * to create the corresponding Recipe object.
     */
    final static Map<String, Supplier<Recipe>> recipeCreator = new HashMap<>();

    static {
        recipeCreator.put("Chorea", () -> {
            var c = new IngredientMap(Arrays.asList(new AminoAcid(10), new Nucleotide(2)));
            return new Recipe(Chorea.class, c);
        });

        recipeCreator.put("Amnesia", () -> {
            var c = new IngredientMap(Arrays.asList(new AminoAcid(70), new Nucleotide(110)));
            return new Recipe(Amnesia.class, c);
        });

        recipeCreator.put("Paralysis", () -> {
            var c = new IngredientMap(Arrays.asList(new AminoAcid(20), new Nucleotide(40)));
            return new Recipe(Paralysis.class, c);
        });

        recipeCreator.put("BearDance", () -> {
            var c = new IngredientMap(Arrays.asList(new AminoAcid(150), new Nucleotide(100)));
            return new Recipe(BearDance.class, c);
        });

    }

    /**
     * Create a recipe from an effect name.
     *
     * @param name name of the effect
     * @return the created recipe
     */
    public Recipe create(String name) {
        if (!recipeCreator.containsKey(name)) throw new GameException(name + ": no such recipe");
        return recipeCreator.get(name).get();
    }
}

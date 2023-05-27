package shell;

import base.GameException;
import ingredient.AminoAcid;
import ingredient.Ingredient;
import ingredient.Nucleotide;

import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

/**
 * A class to help with creating ingredients
 */
public class IngredientCreator {
    /**
     * Assignments of names of ingredients, to functions to create the corresponding
     * Ingredient object with the quantity passed as a parameter.
     */
    final static Map<String, Function<Integer, Ingredient>> ingredientCreator = new HashMap<>();

    static {
        ingredientCreator.put("AminoAcid", AminoAcid::new);
        ingredientCreator.put("Nucleotide", Nucleotide::new);
    }

    /**
     * Create an ingredient.
     *
     * @param name   name of the ingredient
     * @param amount quantity of the ingredient
     * @return the created ingredient
     */
    public Ingredient create(String name, Integer amount) {
        if (!ingredientCreator.containsKey(name)) throw new GameException(name + ": no such ingredient");
        return ingredientCreator.get(name).apply(amount);
    }
}

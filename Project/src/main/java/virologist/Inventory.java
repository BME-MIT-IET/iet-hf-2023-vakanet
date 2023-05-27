package virologist;

import agent.Agent;
import agent.AgentList;
import agent.Recipe;
import agent.RecipeSet;
import equipment.Axe;
import equipment.Equipment;
import equipment.EquipmentSet;
import ingredient.Ingredient;
import ingredient.IngredientMap;

import java.util.Collection;

/**
 * Inventory class, it stores everything that can be picked up, or be used in some way.
 * The Different type of items need to be stored differently.
 */
public class Inventory {
    /**
     * Collection of ingredients.
     */
    final IngredientMap ingredients = new IngredientMap();

    /**
     * Collection of agents.
     */
    final AgentList agents = new AgentList();

    /**
     * Collection of recipes.
     */
    final RecipeSet recipes = new RecipeSet();

    /**
     * Collection of equipments.
     */
    final EquipmentSet equipments = new EquipmentSet();

    @Override
    public String toString() {
        return "Equipments: " + equipments +
                "\nIngredients: " + ingredients +
                "\nRecipes: " + recipes +
                "\nAgents: " + agents;
    }

    public boolean hasAxe() {
        for (Equipment equipment : equipments.getView()) {
            if (equipment instanceof Axe)
                return true;
        }
        return false;
    }

    public void addIngredient(Ingredient i) {
        ingredients.add(i);
    }

    public void addAgent(Agent a) {
        agents.add(a);
    }

    public void addRecipe(Recipe r) {
        recipes.add(r);
    }

    public void addEquipment(Equipment e) {
        equipments.add(e);
    }

    public Ingredient removeIngredient(Ingredient i) {
        return ingredients.remove(i);
    }

    public Agent removeAgent(Agent a) {
        return agents.remove(a);
    }

    public Recipe removeRecipe(Recipe r) {
        return recipes.remove(r);
    }

    public Equipment removeEquipment(Equipment e) {
        return equipments.remove(e);
    }

    public Collection<Recipe> getRecipes() {
        return recipes.getView();
    }

    public IngredientMap getIngredients() {
        return ingredients;
    }

    public Collection<Agent> getAgents() {
        return agents.getView();
    }

    public Collection<Equipment> getEquipments() {
        return equipments.getView();
    }
}

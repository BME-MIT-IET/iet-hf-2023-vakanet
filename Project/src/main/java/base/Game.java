package base;

import agent.Recipe;
import virologist.Virologist;

import java.util.HashSet;
import java.util.Set;

/**
 * Game controller singleton class, responsible for providing unique ids.
 */
public class Game {
    /**
     * Singleton Game instance
     */
    private static Game instance;
    /**
     * List of the recipe Id-s in the game. When a new recipe is created it needs to be registered.
     */
    private final Set<Id> recipes = new HashSet<>();
    /**
     * Id number for unique id generation
     */
    private long id;
    /**
     * True if the game has started.
     */
    private boolean started = false;

    private Game() {
        id = 1; // id 0 is invalid, negative ids are reserved
    }

    /**
     * This is the method to get the instance
     * If the instance doesn't exist it gets created
     *
     * @return the singleton instance
     */
    public static Game getInstance() {
        if (instance == null)
            instance = new Game();
        return instance;

    }

    /**
     * Increments the unique id number, and returns the old one
     *
     * @return a new unique id number
     */
    public synchronized long getNewUniqueId() {
        var ret = id;
        id += 1;
        return ret;
    }

    /**
     * Register a new recipe.
     *
     * @param r the recipe to register
     */
    public void addRecipe(Recipe r) {
        recipes.add(r.getId());
    }

    /**
     * Check a virologist has won the game.
     * The virologist is the winner of the game if it collected all
     * the recipes available in laboratories.
     * If there are no laboratories, the game can't be won.
     *
     * @param v the virologist in question
     */
    public void checkWinner(Virologist v) {
        // Can't win if the game was not started
        if (!started) return;

        var inv = v.getInventory().getRecipes();
        var win = inv.stream().allMatch(r -> recipes.contains(r.getId()));

        if (win) {
            System.out.println(v + " won the game.");
            System.exit(0);
        }
    }

    public void loadGame() {

    }

    public void startGame() {
        started = true;
    }

    public void saveGame() {

    }
}

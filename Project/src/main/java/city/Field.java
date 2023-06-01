package city;

import base.GameException;
import base.Id;
import base.Item;
import gui.Window;
import gui.game.GameScene;
import virologist.Virologist;

import java.util.*;

/**
 * A field in the city.
 */
public class Field {
    /**
     * The unique id of the field.
     */
    final Id id = new Id();

    /**
     * The list of virologists on the field.
     */
    final List<Virologist> characters = new ArrayList<>();

    /**
     * The list of the neighboring fields.
     */
    final Set<Field> neighbours = new HashSet<>();

    /**
     * Add the virologist to the field.
     *
     * @param virologist the virologist to add
     */
    public void addVirologist(Virologist virologist) {
        characters.add(virologist);
        virologist.setField(this); // set the field of the virologist
    }

    /**
     * Get the list of Virologists currently on the field.
     *
     * @return the list of virologists
     */
    public List<Virologist> getCharacters() {
        return Collections.unmodifiableList(characters);
    }

    /**
     * Get the list of neighboring fields.
     *
     * @return the list of neighboring fields
     */
    public List<Field> getNeighbours() {
        return List.copyOf(neighbours);
    }

    /**
     * Get the list of stealable items paired with the owner of the item.
     *
     * @return list of stealable item-owner pairs
     */
    public List<AbstractMap.SimpleEntry<Item, Virologist>> getStealable() {
        final var stealable = new ArrayList<AbstractMap.SimpleEntry<Item, Virologist>>();
        characters.forEach(v -> v.getStealable().forEach(s -> stealable.add(new AbstractMap.SimpleEntry<>(s, v))));
        return stealable;
    }

    /**
     * Add a new neighboring field.
     *
     * @param f the new neighbor
     */
    public void addNeighbour(Field f) {
        neighbours.add(f);
    }

    /**
     * Add an item to the virologist if possible. Child classes need to override this.
     *
     * @param v the virologist to give the item to
     */
    public void addItemTo(Virologist v) {
        // Intentionally left blank by default
    }

    /**
     * Move a virologist from this field to a neighboring field.
     *
     * @param v the virologist to be moved
     * @param f the field the virologist is to be moved to
     */
    public void move(Virologist v, Field f) {
        boolean hasField = v.getField().getNeighbours().contains(f);
        if (!hasField) throw new GameException(f + " is not a neighboring field, can't move there");

        characters.remove(v);
        f.addVirologist(v);
        String message = v + " moved to " + f;
        System.out.println(message);
        GameScene gs = Window.getGameScene();
        if (gs != null)
            gs.logAction(message);
    }

    @Override
    public String toString() {
        return "Field#" + id.getId();
    }
}

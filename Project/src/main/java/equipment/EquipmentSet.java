package equipment;

import base.GameException;
import virologist.ItemCollection;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;
import java.util.stream.Collectors;

/**
 * Implementation of ItemCollection for Equipment. Equipments are stored in a set,
 * however only a certain number of them can be in the collection at one time.
 */
public class EquipmentSet implements ItemCollection<Equipment> {
    /**
     * Capacity of collection.
     */
    final static int CAPACITY = 3;
    /**
     * Collection of Equipments.
     */
    final Set<Equipment> equipments = new HashSet<>();

    @Override
    public void add(Equipment item) {
        if (equipments.size() >= CAPACITY) {
            throw new GameException("Can't have more than " + CAPACITY + " equipments!");
        }

        equipments.add(item);
        System.out.println("Collected " + item);
    }

    @Override
    public Equipment remove(Equipment item) {
        if (!equipments.remove(item)) {
            throw new GameException("No such Equipment");
        }

        System.out.println("Removed " + item);
        return item;
    }

    @Override
    public Collection<Equipment> getView() {
        return Collections.unmodifiableCollection(equipments);
    }

    @Override
    public String toString() {
        return equipments.stream()
                .map(Object::toString)
                .collect(Collectors.joining(", "));
    }
}

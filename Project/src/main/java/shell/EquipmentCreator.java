package shell;

import base.GameException;
import equipment.*;

import java.util.HashMap;
import java.util.Map;
import java.util.function.Supplier;

/**
 * A class to help with creating Equipment-s.
 */
public class EquipmentCreator {
    /**
     * Assignments of string representations of equipments, to functions
     * to create the corresponding Equipment object.
     */
    final static Map<String, Supplier<Equipment>> equipmentCreator = new HashMap<>();

    static {
        // set up assignments
        equipmentCreator.put("Gloves", new Supplier<Equipment>() {
            @Override
            public Equipment get() {
                return new Gloves();
            }
        });

        equipmentCreator.put("Bag", new Supplier<Equipment>() {
            @Override
            public Equipment get() {
                return new Bag();
            }
        });

        equipmentCreator.put("Cloak", new Supplier<Equipment>() {
            @Override
            public Equipment get() {
                return new Cloak();
            }
        });

        equipmentCreator.put("Axe", new Supplier<Equipment>() {
            @Override
            public Equipment get() {
                return new Axe();
            }
        });
    }

    /**
     * Create an equipment from a string.
     *
     * @param name name of the equipment to create
     * @return the created equipment
     */
    public Equipment create(String name) {
        if (!equipmentCreator.containsKey(name)) throw new GameException(name + ": no such equipment");
        return equipmentCreator.get(name).get();
    }
}

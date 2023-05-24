package shell;

import base.GameException;
import effect.*;

import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

/**
 * A class to help with creating effects.
 */
public class EffectCreator {
    /**
     * Assignments of string representations of effects, to functions
     * to create the corresponding Effect object, taking a string argument.
     */
    final static Map<String, Function<String, Effect>> effectCreator = new HashMap<>();

    static {
        effectCreator.put("Amnesia", arg -> new Amnesia());
        effectCreator.put("BearDance", arg -> new BearDance());
        effectCreator.put("Capacity", arg -> new Capacity());
        effectCreator.put("Chorea", arg -> new Chorea());
        effectCreator.put("Immunity", arg -> new Immunity(effectCreator.get(arg).apply("").getClass()));
        effectCreator.put("Paralysis", arg -> new Paralysis());
        effectCreator.put("Repelling", arg -> new Repelling());
        effectCreator.put("Resistance", arg -> new Resistance());
        effectCreator.put("Stealable", arg -> new Stealable());
    }

    /**
     * Create an effect from a string. Immunity can be created with "Immunity", "[effect name]"
     *
     * @param name name of the effect to create
     * @return the created effect
     */
    public Effect create(String name, String arg) {
        if (!effectCreator.containsKey(name)) throw new GameException(name + ": no such effect");
        return effectCreator.get(name).apply(arg);
    }

}

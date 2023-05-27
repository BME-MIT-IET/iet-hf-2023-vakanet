package shell;

import agent.Agent;
import agent.Vaccine;
import agent.Virus;
import base.GameException;
import effect.*;

import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

/**
 * A class to help with creating effects.
 */
public class AgentCreator {
    final static Map<String, Function<String, Agent>> creator = new HashMap<>();

    static {
        creator.put("Amnesia", arg -> create(arg, Amnesia.class));
        creator.put("BearDance", arg -> create(arg, BearDance.class));
        creator.put("Chorea", arg -> create(arg, Chorea.class));
        creator.put("Paralysis", arg -> create(arg, Paralysis.class));
    }

    static Agent create(String type, Class<? extends Effect> e) {
        if (type.equals("virus")) {
            return new Virus(e);
        } else if (type.equals("vaccine")) {
            return new Vaccine(e);
        } else {
            throw new GameException(type + " is not an agent type");
        }
    }

    public Agent create(String type, String name) {
        if (!creator.containsKey(name)) throw new GameException(name + ": no such agent");
        return creator.get(name).apply(type);
    }

}

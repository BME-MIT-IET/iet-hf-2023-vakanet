package agent;

import virologist.ItemCollection;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Implementation of the ItemCollection for Agents. Agents are stored in a list because they decompose,
 * none of them are the same.
 */
public class AgentList implements ItemCollection<Agent> {
    final List<Agent> agents = new ArrayList<>();

    @Override
    public void add(Agent item) {
        agents.add(item);
        item.collection = this; // set the container of the agent to this container
        System.out.println("Collected " + item);

    }

    @Override
    public Agent remove(Agent item) {
        if (!agents.remove(item)) {
            throw new IllegalArgumentException("Agent not found");
        }

        System.out.println("Removed " + item);
        return item;
    }

    @Override
    public Collection<Agent> getView() {
        return Collections.unmodifiableCollection(agents);
    }

    @Override
    public String toString() {
        return agents.stream()
                .map(Object::toString)
                .collect(Collectors.joining(", "));
    }

}

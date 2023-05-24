package city;

import base.GameException;
import base.Id;
import ingredient.AminoAcid;
import ingredient.Nucleotide;
import shell.EquipmentCreator;
import shell.RecipeCreator;
import virologist.Virologist;

import java.util.*;
import java.util.stream.Collectors;

/**
 * A class representing a city.
 */
public class City {
    /**
     * The fields in the city.
     */
    final Map<Id, Field> fields = new HashMap<>();

    /**
     * The virologists in the city.
     */
    final Map<Id, Virologist> virologists = new HashMap<>();

    /**
     * Check if a graph given defined with edges is a connected graph.
     *
     * @param neighbors the edges of the graph
     * @return true if the graph is a connected graph, false otherwise
     */
    static boolean isConnected(Map<Id, HashSet<Id>> neighbors) {
        if (neighbors.size() == 0) return true;

        // BFS
        var queue = new LinkedList<>(List.of(neighbors.keySet().stream().findFirst().get()));
        var visited = new HashSet<>();

        while (queue.size() != 0) {
            var f = queue.remove(0);
            if (visited.contains(f)) continue;

            visited.add(f);
            queue.addAll(neighbors.get(f));
        }

        // check if all the vertices were visited
        return visited.size() == neighbors.size();
    }

    /**
     * Fill with random fields.
     */
    public void random() {
        if (fields.size() != 0) throw new GameException("Fields were already initialized");

        var rand = new Random();

        // create laboratories
        var effects = new ArrayList<>(List.of("Amnesia", "Chorea", "Paralysis"));
        Collections.shuffle(effects);
        var rc = new RecipeCreator();

        // bear virus lab
        var bvl = new BearVirusLaboratory(rc.create(effects.remove(0)));
        fields.put(bvl.id, bvl);

        // normal labs
        effects.forEach(e -> {
            var l = new Laboratory(rc.create(e));
            fields.put(l.id, l);
        });

        // create shelters
        var equipments = new ArrayList<>(List.of("Axe", "Bag", "Cloak", "Gloves"));
        var ec = new EquipmentCreator();

        equipments.forEach(e -> {
            var s = new Shelter(ec.create(e));
            fields.put(s.id, s);
        });

        // create warehouses
        var nAminoacid = 800;
        while (nAminoacid > 0) {
            var q = rand.nextInt(80);
            var w = new Warehouse(new AminoAcid(q));
            fields.put(w.id, w);
            nAminoacid -= q;
        }

        var nNucleotide = 800;
        while (nNucleotide > 0) {
            var q = rand.nextInt(80);
            var w = new Warehouse(new Nucleotide(q));
            fields.put(w.id, w);
            nNucleotide -= q;
        }

        // create normal fields
        for (int i = 0; i < fields.size() / 9; i++) {
            var f = new Field();
            fields.put(f.id, f);
        }

        // create edges, make connected graph
        var neighbors = fields.keySet().stream().collect(Collectors.toMap(id -> id, id -> new HashSet<Id>()));
        var ids = new ArrayList<>(fields.keySet());
        Collections.shuffle(ids);

        // while there is a small number of edges, or the graph isn't connected create random edges
        for (int i = 0; i < fields.size() / 3 || !isConnected(neighbors); i++) {
            // choose random fields
            var a = ids.get(rand.nextInt(ids.size()));
            var b = ids.get(rand.nextInt(ids.size()));

            // make sure they are not the same, and there is no edge between them
            if (a.equals(b) || neighbors.get(a).contains(b) || neighbors.get(b).contains(a)) continue;

            // save edge
            neighbors.get(a).add(b);
            neighbors.get(b).add(a);

            // set field neighbors
            fields.get(a).addNeighbour(fields.get(b));
            fields.get(b).addNeighbour(fields.get(a));
        }
    }

    /**
     * Add a field to the city.
     *
     * @param f the field to be added to the city
     */
    public void addField(Field f) {
        fields.put(f.id, f);
    }

    /**
     * Add a virologist to the city.
     *
     * @param v the virologist to be added to the city.
     */
    public void addVirologist(Virologist v) {
        virologists.put(v.getId(), v);
    }

    /**
     * Get a field by its id.
     *
     * @param id the id of the field
     * @return the field corresponding o the id
     */
    public Field getField(Id id) {
        if (!fields.containsKey(id)) throw new GameException(id.getId() + ": no such field");
        return fields.get(id);
    }

    /**
     * Get a virologist by its id.
     *
     * @param id the id of the virologist
     * @return the virologist corresponding o the id
     */
    public Virologist getVirologist(Id id) {
        if (!virologists.containsKey(id)) throw new GameException("No such virologist");
        return virologists.get(id);
    }

    public ArrayList<Virologist> spawnPlayers(ArrayList<String> players) {
        Random rnd = new Random();
        ArrayList<Virologist> virologists = new ArrayList<>();
        int index = rnd.nextInt(fields.size());
        for (int i = 0; i < players.size(); i++) {
            var fieldArray = fields.values().toArray();
            while (fieldArray[index] instanceof BearVirusLaboratory)
                index = rnd.nextInt(fields.size());

            Field field = (Field) fieldArray[index];

            Virologist v = new Virologist(field, players.get(i));
            virologists.add(v);

            field.addVirologist(v);

        }
        return virologists;
    }
}

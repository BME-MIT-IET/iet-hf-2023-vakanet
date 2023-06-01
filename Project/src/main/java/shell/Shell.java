package shell;

import agent.Recipe;
import base.Game;
import base.GameException;
import base.Id;
import city.*;
import equipment.Cloak;
import equipment.Equipment;
import ingredient.Ingredient;
import virologist.Virologist;

import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.function.Consumer;

/**
 * A class implementing a text based user interface for the game.
 * A basic REPL.
 */
public class Shell {
    static final IngredientCreator ingredientCreator = new IngredientCreator();
    static final RecipeCreator recipeCreator = new RecipeCreator();
    static final EquipmentCreator equipmentCreator = new EquipmentCreator();
    static final EffectCreator effectCreator = new EffectCreator();
    static final AgentCreator agentCreator = new AgentCreator();
    static final Map<String, Consumer<String[]>> commands = new HashMap<>();
    static final City city = new City();
    static Virologist selected;

    static {
        commands.put("exit", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            System.exit(0);
        });

        commands.put("new-field", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            final Field f = new Field();
            city.addField(f);
            System.out.println(f);
        });

        commands.put("new-warehouse", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");
            final Ingredient i = ingredientCreator.create(args[1], Integer.parseInt(args[2]));
            final Field f = new Warehouse(i);
            city.addField(f);
            System.out.println(f);
        });

        commands.put("new-laboratory", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final Recipe r = recipeCreator.create(args[1]);
            final Field f = new Laboratory(r);
            city.addField(f);
            System.out.println(f);
        });

        commands.put("new-bearvirus-laboratory", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final Recipe r = recipeCreator.create(args[1]);
            final Field f = new BearVirusLaboratory(r);
            city.addField(f);
            System.out.println(f);
        });

        commands.put("new-shelter", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final Equipment r = equipmentCreator.create(args[1]);
            final Field f = new Shelter(r);
            city.addField(f);
            System.out.println(f);
        });

        commands.put("neighbor", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");

            final Id a = new Id(Long.parseLong(args[1]));
            final Id b = new Id(Long.parseLong(args[2]));

            Field A = city.getField(a);
            Field B = city.getField(b);

            A.addNeighbour(B);
            B.addNeighbour(A);
        });

        commands.put("new-virologist", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");
            final Id i = new Id(Long.parseLong(args[2]));
            final Field f = city.getField(i);

            final Virologist v = new Virologist(f, args[1]);
            f.addVirologist(v);
            city.addVirologist(v);

            System.out.println(v);
        });

        commands.put("select", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            selected = city.getVirologist(new Id(Long.parseLong(args[1])));
            System.out.println(getSelected());
        });

        commands.put("inventory", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            final var v = getSelected();
            System.out.println(v.getInventory());
        });

        commands.put("get-neighbors", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            final var v = getSelected();
            final var n = v.getField().getNeighbours();

            for (var f : n) {
                System.out.println(f);
            }
        });

        commands.put("move", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var v = getSelected();
            final var target = city.getField(new Id(Long.parseLong(args[1])));

            v.move(target);
        });

        commands.put("collect", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            getSelected().collect();
        });

        commands.put("craft", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");
            final var v = getSelected();
            final var rid = new Id(Long.parseLong(args[2]));

            if (args[1].equals("virus")) {
                v.craft(false, rid);
            } else if (args[1].equals("vaccine")) {
                v.craft(true, rid);
            } else {
                throw new GameException(args[1] + " is not a valid agent type");
            }
        });

        commands.put("get-players", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            final var v = getSelected();
            final var vs = v.getField().getCharacters();

            for (var p : vs) {
                System.out.println(p);
            }
        });

        commands.put("get-stealable", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            final var stealable = getSelected().getField().getStealable();

            for (int i = 0; i < stealable.size(); i++) {
                final var v = stealable.get(i).getValue();
                final var item = stealable.get(i).getKey();
                System.out.println(i + ". " + item + " " + v);
            }
        });

        commands.put("steal", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var v = getSelected();
            final var stealable = v.getField().getStealable();

            final int index = Integer.parseInt(args[1]);
            try {
                final var item = stealable.get(index).getKey();
                final var target = stealable.get(index).getValue();
                v.steal(item, target);
            } catch (IndexOutOfBoundsException e) {
                throw new GameException("No such stealable item");
            }
        });

        commands.put("use", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");
            final var v = getSelected();
            final var aid = new Id(Long.parseLong(args[1]));
            final var vid = new Id(Long.parseLong(args[2]));
            v.use(aid, vid);
        });

        commands.put("axe", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var v = getSelected();
            final var vid = new Id(Long.parseLong(args[1]));
            v.useAxe(vid);
        });

        commands.put("set-cloak", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var state = Cloak.State.valueOf(args[1]);
            Cloak.setState(state);
        });

        commands.put("add-equipment", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var v = getSelected();
            final var e = equipmentCreator.create(args[1]);
            e.onPickUp(v);
        });

        commands.put("add-ingredient", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");
            final var v = getSelected();
            final var i = ingredientCreator.create(args[1], Integer.parseInt(args[2]));
            i.onPickUp(v);
        });

        commands.put("add-recipe", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var v = getSelected();
            final var r = recipeCreator.create(args[1]);
            r.onPickUp(v);
        });

        commands.put("add-effect", args -> {
            if (args.length != 2 && args.length != 3) throw new GameException("expected 1 or 2 arguments");
            final var v = getSelected();
            final var e = effectCreator.create(args[1], args.length == 3 ? args[2] : "");
            e.applyEffect(v);
        });

        commands.put("add-agent", args -> {
            if (args.length != 3) throw new GameException("expected 2 arguments");
            final var v = getSelected();
            final var a = agentCreator.create(args[1], args[2]);
            v.getInventory().addAgent(a);
        });

        commands.put("get-effects", args -> {
            if (args.length != 1) throw new GameException("expected no arguments");
            final var v = getSelected();
            final var es = v.getAppliedEffects();
            es.forEach(System.out::println);
        });

        commands.put("sleep", args -> {
            if (args.length != 2) throw new GameException("expected 1 argument");
            final var millis = Long.parseLong(args[1]);
            try {
                Thread.sleep(millis);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        });

        commands.put("start-game", args -> {
            if (args.length != 1) throw new GameException("Expected no arguments");
            Game.getInstance().startGame();
        });

        commands.put("random-city", args -> {
            if (args.length != 1) throw new GameException("Expected no arguments");
            city.random();
        });
    }

    public static Virologist getSelected() {
        if (selected == null) throw new GameException("no selected virologist");
        return selected;
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        var shell = new Shell();

        while (sc.hasNextLine()) {
            shell.eval(sc.nextLine());
        }
    }

    public void eval(String input) {
        try {
            final String line = input.strip().split("#")[0].strip();
            final String[] command = line.split(" +");
            if (line.isEmpty()) return;
            if (!commands.containsKey(command[0]))
                throw new GameException("'" + command[0] + "': no such command");

            commands.get(command[0]).accept(command);
        } catch (GameException ge) {
            // Game-exceptions are soft exceptions, they can be printed as messages
            System.out.println(ge.getMessage());
        }
    }

    public City getCity() {
        return city;
    }
}

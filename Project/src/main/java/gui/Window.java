package gui;

import city.City;
import gui.game.GameScene;
import gui.menu.MenuScene;
import shell.Shell;

import javax.swing.*;
import java.awt.*;
import java.io.OutputStream;
import java.io.PrintStream;
import java.util.HashMap;
import java.util.Scanner;

public class Window extends JFrame {
    // Singleton Window instance
    private static Window instance = null;
    // Scene management
    private static final HashMap<Scene, JPanel> scenes = new HashMap<>();
    private static JPanel activeScene = null;
    /**
     * A Shell object to execute commands.
     */
    final Shell shell = new Shell();
    // Window components and properties
    private final Dimension defaultSize = new Dimension(1280, 720);
    private final String defaultTitle = "A Világtalan Virológusok Világa";
    /**
     * A buffer for the shell's output.
     */
    final private StringBuffer outBuf = new StringBuffer();
    /**
     * A Scanner to read the shell output buffer.
     */
    final private Scanner outSc = new Scanner(cb -> {
        String buf = outBuf.toString();
        if (!buf.contains(System.lineSeparator())) return 0; // wait for newline

        var line = buf.lines().findFirst();
        if (line.isEmpty()) return 0; // shouldn't ever happen, but just in case

        String s = line.get() + System.lineSeparator();
        int size = s.length();

        cb.put(s); // copy to scanner
        outBuf.delete(0, size); // remove from buffer

        return size;
    });
    private final City city;

    private Window() {
        var stdout = System.out; // original stdout, use this for printing
        // override System.out for the shell, to read its output
        System.setOut(new PrintStream(new OutputStream() {
            @Override
            public void write(int b) {
                outBuf.append((char) b);
            }
        }));
        (new Thread(() -> {
            // just print the output for now
            while (outSc.hasNextLine()) {
                String line = outSc.nextLine();
                if (line.length() == 0) continue;
                stdout.println(line);
            }
        })).start();

        shell.eval("random-city"); // generate random city
        city = shell.getCity();


        // Set up scene map & set main menu as the currently active scene.
        addScene(Scene.MAIN_MENU, new MenuScene());
        activeScene = scenes.get(Scene.MAIN_MENU);
        this.setContentPane(activeScene);

        // JFrame setup
        setTitle(defaultTitle + " - Main Menu");
        setSize(defaultSize);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        // And finally we can become visible
        setVisible(true);
    }

    /**
     * Get instance for singleton window.
     *
     * @return window instance.
     */
    public static Window getInstance() {
        if (instance == null) {
            instance = new Window();
        }
        return instance;
    }

    /**
     * Changes the currently acitve scene.
     *
     * @param scene new scene.
     */
    public static void changeScene(Scene scene) {
        activeScene = scenes.get(scene);
        if (activeScene != null) {
            instance.setContentPane(activeScene);
            instance.revalidate();
            Window.getInstance().setTitle(getInstance().defaultTitle + " - " + scene.getName());
        }
        if (scene.equals(Scene.GAME)) {
            GameScene gameScene = (GameScene) activeScene;
            if (gameScene != null) {
                gameScene.limitSplitPaneSize();
            }
        }
    }

    /**
     * Add scene to scene map.
     *
     * @param name  Scene type.
     * @param scene Scene panel.
     */
    public static void addScene(Scene name, JPanel scene) {
        scenes.put(name, scene);
    }

    public static GameScene getGameScene() {
        return (GameScene) scenes.get(Scene.GAME);
    }

    public City getCity() {
        return city;
    }
}

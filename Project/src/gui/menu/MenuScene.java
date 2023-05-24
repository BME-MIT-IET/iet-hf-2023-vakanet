package gui.menu;

import gui.Scene;
import gui.Window;

import javax.swing.*;
import java.awt.*;

public class MenuScene extends JPanel {
    private JLabel titleLabel;
    private JButton playBtn, loadBtn, exitBtn;

    public MenuScene() {
        this.setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        initializeComponents();
        createActionListeners();
    }

    /**
     * Initialize components for the main menu scene.
     */
    private void initializeComponents() {
        titleLabel = new JLabel("A Világtalan Virológusok Világa");
        playBtn = new JButton("Create Game");
        loadBtn = new JButton("Load Game");
        exitBtn = new JButton("Exit");

        titleLabel.setAlignmentX(CENTER_ALIGNMENT);
        playBtn.setAlignmentX(CENTER_ALIGNMENT);
        loadBtn.setAlignmentX(CENTER_ALIGNMENT);
        exitBtn.setAlignmentX(CENTER_ALIGNMENT);

        titleLabel.setFont(new Font(Font.SANS_SERIF, Font.BOLD, 24));

        /*
          Add components to panel
          VerticalGlues to center content.
          RigidAreas to add spacing between components.
         */
        this.add(Box.createVerticalGlue());
        this.add(titleLabel);
        this.add(Box.createRigidArea(new Dimension(0, 15)));
        this.add(playBtn);
        this.add(Box.createRigidArea(new Dimension(0, 10)));
        this.add(loadBtn);
        this.add(Box.createRigidArea(new Dimension(0, 10)));
        this.add(exitBtn);
        this.add(Box.createVerticalGlue());
    }

    /**
     * Add function to buttons in the main menu.
     */
    private void createActionListeners() {
        playBtn.addActionListener((e) -> {
            Window.addScene(Scene.GAME_CREATION, new GameCreationScene());
            Window.changeScene(Scene.GAME_CREATION);
        });
        // TODO: Load saved game
        exitBtn.addActionListener((e) -> System.exit(0));
    }
}

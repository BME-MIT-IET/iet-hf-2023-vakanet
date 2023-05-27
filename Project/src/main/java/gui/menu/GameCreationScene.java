package gui.menu;

import gui.Scene;
import gui.Window;
import gui.game.GameScene;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class GameCreationScene extends JPanel {
    // List of player names
    private ArrayList<String> virologists = new ArrayList<>();
    // Components
    private JPanel labelPanel, splitPanel, playerListPanel, actionPanel, actionButtonPanel;
    private JTextField playerNameField;
    private JLabel textFieldLabel, titleLabel, subTitleLabel;
    private JButton addPlayerBtn, startGameBtn, cancelBtn;

    public GameCreationScene() {
        initializeComponents();
        createLayout();
        createActionListeners();
    }

    /**
     * Initialize components for the game creation scene.
     */
    private void initializeComponents() {
        // Sub-panels and their layouts
        labelPanel = new JPanel();
        labelPanel.setLayout(new BoxLayout(labelPanel, BoxLayout.Y_AXIS));
        actionPanel = new JPanel();
        actionPanel.setLayout(new BoxLayout(actionPanel, BoxLayout.Y_AXIS));
        playerListPanel = new JPanel();
        playerListPanel.setLayout(new BoxLayout(playerListPanel, BoxLayout.Y_AXIS));
        splitPanel = new JPanel();
        actionButtonPanel = new JPanel();

        // Labels
        titleLabel = new JLabel("Game Creation");
        titleLabel.setAlignmentX(CENTER_ALIGNMENT);
        titleLabel.setFont(new Font(Font.SANS_SERIF, Font.BOLD, 24));
        subTitleLabel = new JLabel("Add players to continue.");
        subTitleLabel.setAlignmentX(CENTER_ALIGNMENT);
        subTitleLabel.setFont(new Font(Font.SANS_SERIF, Font.PLAIN, 14));

        textFieldLabel = new JLabel("Enter player name:");
        textFieldLabel.setAlignmentX(CENTER_ALIGNMENT);

        // Buttons
        cancelBtn = new JButton("Cancel");
        addPlayerBtn = new JButton("Add Player");
        startGameBtn = new JButton("Start Game");

        // Text field
        playerNameField = new JTextField(20);
        playerNameField.setMaximumSize(playerNameField.getPreferredSize());
        playerNameField.setAlignmentX(CENTER_ALIGNMENT);
    }

    /**
     * Add components to their containers and create final layout for scene
     */
    private void createLayout() {
        // Add components to sub-panels
        labelPanel.add(titleLabel);
        labelPanel.add(Box.createRigidArea(new Dimension(0, 5)));
        labelPanel.add(subTitleLabel);

        actionButtonPanel.add(addPlayerBtn);
        actionButtonPanel.add(startGameBtn);
        actionButtonPanel.add(cancelBtn);

        actionPanel.add(textFieldLabel);
        actionPanel.add(playerNameField);
        actionPanel.add(actionButtonPanel);

        splitPanel.add(playerListPanel, BorderLayout.WEST);
        splitPanel.add(actionPanel, BorderLayout.EAST);

        // Set parent panel layout and add sub-panels
        this.setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
        this.add(Box.createVerticalGlue());
        this.add(labelPanel);
        this.add(Box.createRigidArea(new Dimension(0, 20)));
        this.add(splitPanel);
        this.add(Box.createVerticalGlue());
    }

    /**
     * Add actions to buttons in the scene,
     */
    private void createActionListeners() {
        cancelBtn.addActionListener((e) -> {
            this.virologists = new ArrayList<>();
            this.playerListPanel.removeAll();
            Window.changeScene(Scene.MAIN_MENU);
        });
        startGameBtn.addActionListener((e) -> createGame());
        addPlayerBtn.addActionListener((e) -> addPlayer(playerNameField.getText()));
    }

    /**
     * Add player to game.
     *
     * @param playerName name of the player.
     */
    private void addPlayer(String playerName) {
        // The name shouldn't be empty
        if (!playerName.isEmpty() && !playerName.isBlank()) {
            virologists.add(playerName);
            playerListPanel.add(new JLabel("(" + virologists.size() + ") " + playerName));
            playerListPanel.add(Box.createRigidArea(new Dimension(0, 4)));
            this.repaint();
            this.revalidate();
        }
        playerNameField.setText("");
    }

    /**
     * Passes list of names to Game scene then, the game begins as we switch scenes.
     */
    private void createGame() {
        // Can't start game with less than 2 players.
        if (virologists.size() > 1) {

            Window.addScene(Scene.GAME, new GameScene(Window.getInstance().getCity(), virologists));
            Window.changeScene(Scene.GAME);


        }
    }
}

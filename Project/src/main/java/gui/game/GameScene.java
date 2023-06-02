package gui.game;

import agent.Agent;
import agent.Vaccine;
import city.City;
import city.Field;
import effect.Effect;
import effect.Stealable;
import gui.Window;
import virologist.Virologist;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;


public class GameScene extends JPanel implements ActionListener {
    /**
     * A static int that corresponds to the maximum
     * number of actions a player can make in one turn
     */
    private static final int noActions = 3;
    private final JButton useAxeButton = new JButton("Use axe");
    private final JButton useAgentButton = new JButton("Use agent");
    private final JButton craftAgentButton = new JButton("Craft agent");
    private final JButton stealItemsButton = new JButton("Steal item");
    private final JButton moveButton = new JButton("Move");
    private final JButton collectButton = new JButton("Collect Item");
    private final JButton nextTurnButton = new JButton("End Turn");
    private final JButton inventoryButton = new JButton("View Inventory");
    /**
     * The current player's remaining number of actions
     */
    private int actionsRemaining = noActions;
    /**
     * The current player's id we are controlling
     */
    private int playerID = 0;
    private final ArrayList<Virologist> players;
    // Main container
    private JSplitPane splitPane;
    // Game Panel & Activity log
    private JPanel gamePanel;
    private InfoPanel sideBar;
    private final JLabel topLabel = new JLabel();
    private JLabel background = new JLabel();

    public GameScene(City city, ArrayList<String> playerNames) {
        try {
            initializeComponents();
        } catch (Exception ignored) {
            // Big problem :)
        }
        players = city.spawnPlayers(playerNames);
        topLabel.setText("Current player: " + players.get(playerID).getCharacterName() + " - Actions remaining: " + actionsRemaining);
        renderField(playerID);
    }

    /**
     * Initialize components for game scene.
     */
    private void initializeComponents() throws Exception {
        this.setLayout(new BorderLayout());
        gamePanel = new JPanel();
        gamePanel.setLayout(new BorderLayout());
        sideBar = new InfoPanel();

        // SplitPanel
        splitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, gamePanel, sideBar);
        splitPane.setOneTouchExpandable(true);

        //Add action listener to buttons
        useAxeButton.addActionListener(this);
        useAgentButton.addActionListener(this);
        stealItemsButton.addActionListener(this);
        moveButton.addActionListener(this);
        nextTurnButton.addActionListener(this);
        collectButton.addActionListener(this);
        craftAgentButton.addActionListener(this);
        inventoryButton.addActionListener(this);

        //Add buttons to button row
        JPanel buttonRow = new JPanel();
        buttonRow.add(useAxeButton);
        buttonRow.add(useAgentButton);
        buttonRow.add(craftAgentButton);
        buttonRow.add(stealItemsButton);
        buttonRow.add(moveButton);
        buttonRow.add(collectButton);
        buttonRow.add(inventoryButton);
        buttonRow.add(nextTurnButton);

        gamePanel.add(buttonRow, BorderLayout.SOUTH);

        this.add(splitPane, BorderLayout.CENTER);
        JPanel topPanel = new JPanel();
        topPanel.add(topLabel);

        gamePanel.add(background);
        gamePanel.add(topPanel, BorderLayout.NORTH);
    }

    public void limitSplitPaneSize() {
        splitPane.getRightComponent().setMaximumSize(new Dimension(this.getWidth() / 5, this.getHeight()));
        splitPane.setDividerLocation(getWidth());
    }

    /**
     * Disables all the buttons if the actions taken in the current round
     * reached the maximum amount.
     */
    private void DisableButtons() {
        useAxeButton.setEnabled(false);
        useAgentButton.setEnabled(false);
        stealItemsButton.setEnabled(false);
        moveButton.setEnabled(false);
        collectButton.setEnabled(false);
    }

    /**
     * Enables all buttons
     */
    private void EnableButtons() {
        useAxeButton.setEnabled(true);
        useAgentButton.setEnabled(true);
        stealItemsButton.setEnabled(true);
        moveButton.setEnabled(true);
        inventoryButton.setEnabled(true);
        collectButton.setEnabled(true);
    }

    public void logAction(String message) {
        sideBar.logAction(message + " - " + " " + java.time.LocalDateTime.now().format(DateTimeFormatter.ISO_LOCAL_TIME) + "\n");
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (e.getSource() == useAxeButton) {

            if (players.get(playerID).getInventory().hasAxe()) {

                PlayerSelector dialog = new PlayerSelector(Window.getInstance(), players.get(playerID).getField(), players.get(playerID), "Select player to Attack with Axe:");
                dialog.setVisible(true);
                if (dialog.isSelectionDone()) {
                    Virologist target = dialog.getPlayerSelected();
                    if (target != null) {
                        players.get(playerID).useAxe(target.getId());
                        actionsRemaining--;
                    }

                }
                dialog.dispose();

            } else {
                JOptionPane.showMessageDialog(this, "A virológus nem rendelkezik baltával!");
            }

        }
        else if (e.getSource() == useAgentButton) {
            AgentSelector agentSelector = new AgentSelector(Window.getInstance(), players.get(playerID));
            agentSelector.setVisible(true);
            if (agentSelector.isSelectionDone()) {
                Agent agent = agentSelector.getAgentSelected();
                if (agent instanceof Vaccine) {
                    players.get(playerID).use(agent.getId(), players.get(playerID).getId());
                    actionsRemaining--;
                } else if (agent != null) {
                    PlayerSelector playerDialog = new PlayerSelector(Window.getInstance(), players.get(playerID).getField(), players.get(playerID), "Select player to use Agent on");
                    playerDialog.setVisible(true);
                    if (playerDialog.isSelectionDone()) {
                        Virologist target = playerDialog.getPlayerSelected();
                        playerDialog.dispose();
                        if (target != null) {
                            players.get(playerID).use(agent.getId(), target.getId());
                            actionsRemaining--;
                        }

                    }
                    playerDialog.dispose();
                }
            }
            agentSelector.dispose();

        }
        else if (e.getSource() == stealItemsButton) {
            Effect stealable = players.get(playerID).getAppliedEffects().stream().filter(x -> x instanceof Stealable).findFirst().orElse(null);
            if (stealable != null)
                players.get(playerID).removeEffect(stealable);
            ItemSelector dialog = new ItemSelector(Window.getInstance(), players.get(playerID).getField(), players.get(playerID));
            dialog.setVisible(true);
            if (dialog.isSelectionDone()) {
                Virologist target = dialog.getTarget();
                if (target != null) {
                    players.get(playerID).steal(dialog.getSelectedItem(), target);
                }
            }
            if (stealable != null) {
                players.get(playerID).addEffect(stealable);
                actionsRemaining--;
            }
            dialog.dispose();
        }
        else if (e.getSource() == moveButton) {
            FieldSelector dialog = new FieldSelector(Window.getInstance(), players.get(playerID).getField());
            dialog.setVisible(true);
            if (dialog.isSelectionDone()) {
                Field nextField = dialog.getField();
                if (nextField != null) {
                    players.get(playerID).move(nextField);
                    actionsRemaining--;
                }
            }

            dialog.dispose();
        }
        else if (e.getSource() == collectButton) {
            //TODO check if collect was successful
            players.get(playerID).collect();
            actionsRemaining--;
        }
        else if (e.getSource() == nextTurnButton) {
            actionsRemaining = noActions;
            if (playerID < players.size() - 1) {
                playerID++;
            }
            else {
                playerID = 0;
            }
            EnableButtons();
        }
        else if (e.getSource() == craftAgentButton) {
            RecipeSelector dialog = new RecipeSelector(Window.getInstance(), players.get(playerID));
            dialog.setVisible(true);
            if (dialog.isSelectionDone()) {
                logAction(dialog.getAgent() + "was crafted by " + players.get(playerID));
            }
        }
        else if (e.getSource() == inventoryButton) {
            JOptionPane.showMessageDialog(Window.getInstance(), players.get(playerID).getInventory().toString());
        }
        topLabel.setText("Current player: " + players.get(playerID).getCharacterName() + " - Actions remaining: " + actionsRemaining);
        if (actionsRemaining == 0) {
            DisableButtons();
        }
        renderField(playerID);
    }

    private void renderField(int playerID) {
        Field currentField = players.get(playerID).getField();
        String type = currentField.toString().split("#")[0];

        switch (type) {
            case "Laboratory":
                loadBackground("lab.jpeg");
                break;
            case "Warehouse":
                loadBackground("warehouse.gif");
                break;
            case "Shelter":
                loadBackground("shelter.png");
                break;
            case "Field":
                loadBackground("bliss.png");
                break;
        }

        this.revalidate();
        this.repaint();
    }

    private void loadBackground(String fileName) {
        Image backgroundImage;
        try {
            backgroundImage = ImageIO.read(new File("res", fileName));
            backgroundImage = backgroundImage.getScaledInstance(1280, 720, Image.SCALE_SMOOTH);
            background.setIcon(new ImageIcon(backgroundImage));
            background.setBounds(0, 0, gamePanel.getWidth(), gamePanel.getHeight());
            background.setMinimumSize(new Dimension(0,0));
        }
        catch (IOException ignored) {}
    }
}

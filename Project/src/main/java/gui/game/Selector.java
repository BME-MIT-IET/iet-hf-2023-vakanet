package gui.game;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public abstract class Selector<T> extends JDialog implements ActionListener {
    protected JComboBox<T> comboBox;
    protected JTextArea textArea;
    protected JPanel lowerPanel;
    JButton selectButton = new JButton();
    JButton exitButton = new JButton();
    boolean selectionDone = false;

    public Selector(JFrame parent, String selectionReason) {
        super(parent, ModalityType.APPLICATION_MODAL);
        initializeBase(selectionReason);
    }

    public void initializeBase(String selectionReason) {
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        int w = screenSize.width;
        int h = screenSize.height;
        this.setSize(w / 3, h / 3);
        this.setResizable(false);

        JPanel labelPanel = new JPanel();
        labelPanel.add(new JLabel(selectionReason));
        this.add(labelPanel, BorderLayout.NORTH);
        comboBox = new JComboBox<>();

        JPanel mainPanel = new JPanel();
        mainPanel.setLayout(new BorderLayout());
        JPanel selectorPanel = new JPanel();
        selectorPanel.add(comboBox);
        mainPanel.add(selectorPanel, BorderLayout.SOUTH);

        //Buttons
        exitButton.setText("Exit");
        selectButton.setText("Select");
        exitButton.setSize(selectButton.getSize());
        lowerPanel = new JPanel();
        lowerPanel.setLayout(new BoxLayout(lowerPanel, BoxLayout.Y_AXIS));
        JPanel buttonPanel = new JPanel();
        buttonPanel.add(exitButton);
        buttonPanel.add(selectButton);
        lowerPanel.add(buttonPanel);
        this.add(lowerPanel, BorderLayout.SOUTH);


        //List players
        JPanel playerPanel = new JPanel();
        playerPanel.setLayout(new BorderLayout());

        textArea = new JTextArea();
        textArea.setEditable(false);
        JScrollPane scrollPane = new JScrollPane(textArea);
        playerPanel.add(scrollPane, BorderLayout.CENTER);
        mainPanel.add(playerPanel);

        this.add(mainPanel, BorderLayout.CENTER);
        exitButton.addActionListener(this);
        selectButton.addActionListener(this);


    }

    public boolean isSelectionDone() {
        return selectionDone;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (e.getActionCommand().equals(exitButton.getActionCommand())) {
            this.setVisible(false);
        } else if (e.getActionCommand().equals(selectButton.getActionCommand())) {
            this.setVisible(false);
            selectionDone = true;
        }
    }
}

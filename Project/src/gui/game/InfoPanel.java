package gui.game;

import javax.swing.*;
import javax.swing.text.BadLocationException;
import javax.swing.text.DefaultEditorKit;
import javax.swing.text.StyledDocument;
import java.awt.*;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.awt.event.HierarchyEvent;
import java.awt.event.HierarchyListener;
import java.io.InvalidClassException;

/**
 * InfoPanel that will be displayed as a sidebar
 * Every player action will be "logged" here
 */
public class InfoPanel extends JPanel implements ComponentListener, HierarchyListener {
    JTextPane logInfo;
    JScrollPane scrollPane;

    public InfoPanel() throws InvalidClassException {
        if (getParent() != null && !getParent().getClass().equals(JSplitPane.class))
            throw new InvalidClassException("The InfoPanel can only be added to JSplitPane components!");
        this.addComponentListener(this);
        this.setLayout(new BorderLayout());
        logInfo = new JTextPane();
        logInfo.setEditable(false);

        scrollPane = new JScrollPane(logInfo);
        scrollPane.setAutoscrolls(true);
        scrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_ALWAYS);

        this.add(scrollPane, BorderLayout.CENTER);
    }

    public void logAction(String action) {

        StyledDocument doc = logInfo.getStyledDocument();
        doc.putProperty(DefaultEditorKit.EndOfLineStringProperty, "\n");

        //TODO change styling
        try {
            doc.insertString(doc.getLength(), action + "\n", null);
        } catch (BadLocationException ex) {
            ex.printStackTrace();
        }
        logInfo.repaint();
        logInfo.revalidate();
    }

    /**
     * Overridden componentResized ComponentListener, so if the
     * panel is resized, we check its dimensions, and if
     * it is greater than the maximum allowed, we reset it to its
     * maximum allowed size
     *
     * @param e
     */
    @Override
    public void componentResized(ComponentEvent e) {
        if (e.getComponent().getSize().width > this.getMaximumSize().getWidth()) {
            this.setSize(getMaximumSize());
            JSplitPane parent = (JSplitPane) this.getParent();
            parent.setDividerLocation(parent.getParent().getWidth() - getMaximumSize().width);
        }
        scrollPane.repaint();
        logInfo.repaint();
        scrollPane.revalidate();
        logInfo.revalidate();
    }

    @Override
    public void componentMoved(ComponentEvent e) {

    }

    @Override
    public void componentShown(ComponentEvent e) {

    }

    @Override
    public void componentHidden(ComponentEvent e) {

    }

    @Override
    public void hierarchyChanged(HierarchyEvent e) {
        if (getParent() != null) {
            if (!getParent().getClass().equals(JSplitPane.class))
                try {
                    throw new InvalidClassException("The InfoPanel can only be added to JSplitPane components!");
                } catch (InvalidClassException ex) {
                    throw new AssertionError();
                }
        }

    }
}

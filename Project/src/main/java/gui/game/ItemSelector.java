package gui.game;

import base.Item;
import city.Field;
import virologist.Virologist;

import javax.swing.*;
import java.awt.*;

public class ItemSelector extends Selector<Item> {

    Field currentField;
    Virologist currentVirologist;

    public ItemSelector(JFrame parent, Field currentField, Virologist currentVirologist) {

        super(parent, "Select Item");
        this.currentField = currentField;
        this.currentVirologist = currentVirologist;

        var stealable = currentField.getStealable();
        for (var itemVirologistSimpleEntry : stealable) {
            comboBox.addItem(itemVirologistSimpleEntry.getKey());
            textArea.append(itemVirologistSimpleEntry + "\n");
        }
        comboBox.setRenderer(new DefaultListCellRenderer() {
            @Override
            public Component getListCellRendererComponent(JList<?> list,
                                                          Object value,
                                                          int index,
                                                          boolean isSelected,
                                                          boolean cellHasFocus) {
                return super.getListCellRendererComponent(list, value, index, isSelected, cellHasFocus);
            }
        });

        setLocationRelativeTo(null);
    }

    public Item getSelectedItem() {
        return (Item) comboBox.getSelectedItem();
    }

    public Virologist getTarget() {
        var stealable = currentField.getStealable();
        for (var itemVirologistSimpleEntry : stealable) {
            if (itemVirologistSimpleEntry.getKey().equals(getSelectedItem()))
                return itemVirologistSimpleEntry.getValue();
        }
        return null;
    }

}

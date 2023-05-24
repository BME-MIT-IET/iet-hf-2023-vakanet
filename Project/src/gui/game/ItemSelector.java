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
        for (int i = 0; i < stealable.size(); i++) {
            comboBox.addItem(stealable.get(i).getKey());
            textArea.append(stealable.get(i) + "\n");
        }
        comboBox.setRenderer(new DefaultListCellRenderer() {

            @Override
            public Component getListCellRendererComponent(JList<?> list,
                                                          Object value,
                                                          int index,
                                                          boolean isSelected,
                                                          boolean cellHasFocus) {
                value = value;
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
        for (int i = 0; i < stealable.size(); i++) {
            if (stealable.get(i).getKey().equals(getSelectedItem()))
                return stealable.get(i).getValue();
        }
        return null;
    }

}

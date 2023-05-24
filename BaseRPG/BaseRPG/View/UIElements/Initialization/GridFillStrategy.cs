using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.UIElements.Initialization
{
    public delegate FrameworkElement ItemCreation(int index);
    public class GridFillStrategy
    {

        public void Fill(Grid grid, ItemCreation itemCreation,int numberOfColumns, int numberOfItems) {
            for (int i = 0; i < numberOfColumns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = itemCreation(i);
                grid.Children.Add(item);
                if (i % numberOfColumns == 0) grid.RowDefinitions.Add(new RowDefinition());

                Grid.SetColumn(item, i % numberOfColumns);
                Grid.SetRow(item, i / numberOfColumns);
            }
        }
    }
}

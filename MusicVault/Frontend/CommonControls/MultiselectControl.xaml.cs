using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace MusicVault.Frontend.CommonControls;

public partial class MultiselectControl : UserControl {
    public MultiselectControl() {
        InitializeComponent();
    }

    public ObservableCollection<MultiSelectItem> Items {
        get { return (ObservableCollection<MultiSelectItem>)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register("Items", typeof(ObservableCollection<MultiSelectItem>), typeof(MultiselectControl), new PropertyMetadata(new ObservableCollection<MultiSelectItem>()));

    public new double Width {
        get { return (double)GetValue(WidthProperty); }
        set { SetValue(WidthProperty, value); }
    }

    public static new readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(MultiselectControl), new PropertyMetadata(200.0));

    private void ComboBox_DropDownClosed(object? sender, System.EventArgs e) {
        comboBox.Text = "Izbor";
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        comboBox.SelectedValue = null;
        comboBox.Text = "Izbor";
    }
}

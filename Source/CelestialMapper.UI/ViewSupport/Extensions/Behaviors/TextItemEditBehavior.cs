using System.Windows.Documents;
using System.Windows.Input;

namespace CelestialMapper.UI;

public static class TextItemEditBehavior
{
    public static bool GetIsEditingEnabled(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsEditingEnabledProperty);
    }

    public static void SetIsEditingEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(IsEditingEnabledProperty, value);
    }

    public static readonly DependencyProperty IsEditingEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEditingEnabled",
            typeof(bool),
            typeof(TextItemEditBehavior),
            new PropertyMetadata(false, OnIsEditingEnabledChanged));

    public static TextBox GetEditTextBox(DependencyObject obj)
    {
        return (TextBox)obj.GetValue(EditTextBoxProperty);
    }

    public static void SetEditTextBox(DependencyObject obj, TextBox value)
    {
        obj.SetValue(EditTextBoxProperty, value);
    }

    public static readonly DependencyProperty EditTextBoxProperty =
        DependencyProperty.RegisterAttached("EditTextBox", typeof(TextBox), typeof(TextItemEditBehavior), new PropertyMetadata(null, OnEditTextBoxChanged));

    private static void OnEditTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is TextBox textBox)
        {
            textBox.LostFocus += TextBox_LostFocus;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
        }
    }

    private static void OnIsEditingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Panel panel && (bool)e.NewValue)
        {
            panel.MouseDown += Grid_MouseDown;
            panel.KeyDown += Grid_KeyDown;
        }
    }

    private static void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Grid grid && e.ClickCount == 2 && grid.DataContext is CelestialMapper.ViewModel.TextItem textItem)
        {
            // Check if the double-click occurred on the TextBlock (not the TextBox)
            if (e.OriginalSource is TextBlock or Run)
            {
                textItem.IsEditing = true;

                var textBox = GetEditTextBox(grid);
                textBox.Focus();
                textBox.SelectAll();

                e.Handled = true;
            }
        }
    }

    private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && textBox.DataContext is CelestialMapper.ViewModel.TextItem textItem)
        {
            textItem.IsEditing = false;
        }
    }

    private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is TextBox textBox && textBox.DataContext is CelestialMapper.ViewModel.TextItem textItem)
        {
            // Exit edit mode on Escape
            if (e.Key == Key.Escape && textItem.IsEditing)
            {
                ExitEditMode(textBox, textItem);
                e.Handled = true;
                return;
            }

            // Exit edit mode on Enter (without Shift)
            if (e.Key == Key.Return && textItem.IsEditing)
            {
                bool isShiftPressed = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;

                if (!isShiftPressed)
                {
                    ExitEditMode(textBox, textItem);
                    e.Handled = true;
                }
                // If Shift is pressed, allow the default behavior (newline)
            }
        }
    }

    private static void ExitEditMode(TextBox textBox, CelestialMapper.ViewModel.TextItem textItem)
    {
        // Move focus away from the TextBox to the parent Grid
        if (textBox.Parent is Grid grid)
        {
            grid.Focus();
        }

        // Now set IsEditing to false to hide the TextBox
        textItem.IsEditing = false;
    }

    private static void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (sender is Grid grid && grid.DataContext is CelestialMapper.ViewModel.TextItem textItem)
        {
            if (e.Key == Key.Escape && textItem.IsEditing)
            {
                textItem.IsEditing = false;
                e.Handled = true;
            }
        }
    }
}



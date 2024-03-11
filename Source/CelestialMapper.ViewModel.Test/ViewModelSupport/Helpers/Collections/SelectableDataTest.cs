namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class SelectableDataTest
{
    [Test]
    public void Selected_DefaultValue()
    {
        ISelectableData<string> selectableData = new SelectableData<string>();

        Assert.That(selectableData.Selected, Is.Null);
    }

    [Test]
    public void FirstItemSelectedByDefault_SetToTrue_SelectsFirstItem()
    {
        var items = new List<string> { "1", "2", "3" };
        ISelectableData<string> selectableData = new SelectableData<string> { FirstItemSelectedByDefault = true };

        selectableData.UpdateItems(items);

        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.EquivalentTo(items));
            Assert.That(selectableData.Selected, Is.EqualTo("1"));
        });
    }

    [Test]
    public void FirstItemSelectedByDefault_SetToFalse_SelectedIsNull()
    {
        var items = new List<string> { "1", "2", "3" };
        ISelectableData<string> selectableData = new SelectableData<string> { FirstItemSelectedByDefault = false };

        selectableData.UpdateItems(items);

        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.EquivalentTo(items));
            Assert.That(selectableData.Selected, Is.Null);
        });
    }

    [Test]
    public void UpdateItems_SetsItemsAndSelectedToNull_WhenNoItemsAndFirstItemSelectedByDefault()
    {
        ISelectableData<string> selectableData = new SelectableData<string> { FirstItemSelectedByDefault = true };

        selectableData.UpdateItems(Enumerable.Empty<string>());
        
        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.Empty);
            Assert.That(selectableData.Selected, Is.Null);
        });
    }

    [Test]
    public void UpdateItems_SetsItemsAndSelectedToNull_WhenNoItems()
    {
        ISelectableData<string> selectableData = new SelectableData<string>();

        selectableData.UpdateItems(Enumerable.Empty<string>());
        
        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.Empty);
            Assert.That(selectableData.Selected, Is.Null);
        });
    }

    [Test]
    public void UpdateItems_SetsItemsOnly_WhenNoFirstItemSelectedByDefault()
    {
        var items = new List<string> { "1", "2", "3" };
        ISelectableData<string> selectableData = new SelectableData<string> { FirstItemSelectedByDefault = false };

        selectableData.UpdateItems(items);
        
        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.EquivalentTo(items));
            Assert.That(selectableData.Selected, Is.Null);
        });
    }

    [Test]
    public void UpdateItems_UpdatesItemsAndSelected_WhenFirstItemSelectedByDefault()
    {
        var initialItems = new List<string> { "1", "2", "3" };
        ISelectableData<string> selectableData = new SelectableData<string>(initialItems) { FirstItemSelectedByDefault = true };

        var updatedItems = new List<string> { "4", "5", "6" };
        selectableData.UpdateItems(updatedItems);
        
        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.EquivalentTo(updatedItems));
            Assert.That(selectableData.Selected, Is.EqualTo("4"));
        });
    }

    [Test]
    public void Items_Reset_EmptyList_WhenUpdatingItems()
    {
        ISelectableData<string> selectableData = new SelectableData<string> { FirstItemSelectedByDefault = true };

        selectableData.UpdateItems(Enumerable.Empty<string>());
        
        Assert.Multiple(() =>
        {
            Assert.That(selectableData.Items, Is.Empty);
            Assert.That(selectableData.Selected, Is.Null);
        });
    }
}

using System.Runtime.CompilerServices;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class NotifyPropertyChangedBaseTests
{

    private class MockNotifyPropertyChanged : NotifyPropertyChangedBase
    {
    }

    [Test]
    public void GetPropertyValue_ExistingProperty_ReturnsValue()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        string propertyName = "Age";
        viewModel.SetPropertyValue(42, propertyName);

        // Act
        var result = viewModel.GetPropertyValue<int>(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void GetPropertyValue_NonExistingProperty_ReturnsDefault()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        string propertyName = "Age";

        // Act
        var result = viewModel.GetPropertyValue<int>(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(default(int)));
    }

    [Test]
    public void SetPropertyValue_ExistingPropertyWithDifferentValue_ReturnsTrueAndUpdatesValue()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        string propertyName = "Age";
        viewModel.SetPropertyValue(42, propertyName);

        // Act
        var result = viewModel.SetPropertyValue(43, propertyName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(viewModel.GetPropertyValue<int>(propertyName), Is.EqualTo(43));
        });
    }

    [Test]
    public void SetPropertyValue_ExistingPropertyWithSameValue_ReturnsFalseAndDoesNotUpdateValue()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        string propertyName = "Age";
        viewModel.SetPropertyValue(42, propertyName);

        // Act
        var result = viewModel.SetPropertyValue(42, propertyName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(viewModel.GetPropertyValue<int>(propertyName), Is.EqualTo(42));
        });
    }

    [Test]
    public void SetPropertyValue_NonExistingPropertyWithDefaultValue_ReturnsFalseAndDoesNotAddProperty()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        string propertyName = "Name";

        // Act
        var result = viewModel.SetPropertyValue(default(string), propertyName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(viewModel.GetPropertyValue<string>(propertyName), Is.Null);
        });
    }

    [Test]
    public void SetPropertyValue_NonExistingPropertyWithNonNullValue_ReturnsTrueAndAddsProperty()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        string propertyName = "Name";
        string value = "John Doe";

        // Act
        var result = viewModel.SetPropertyValue(value, propertyName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(viewModel.GetPropertyValue<string>(propertyName), Is.EqualTo(value));
        });
    }

    [Test]
    public void RisePropertyChanged_PropertyChangedEventIsRaised()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        bool eventRaised = false;

        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == "Age")
            {
                eventRaised = true;
            }
        };

        // Act
        viewModel.SetPropertyValue(63, "Age");

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void RisePropertyChanged_PropertyChangedEventIsNotRaisedForUnrelatedProperty()
    {
        // Arrange
        var viewModel = new MockNotifyPropertyChanged();
        bool eventRaised = false;

        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == "Age")
            {
                eventRaised = true;
            }
        };

        // Act
        viewModel.SetPropertyValue("Ivan", "Name"); // An unrelated property

        // Assert
        Assert.That(eventRaised, Is.False);
    }

}

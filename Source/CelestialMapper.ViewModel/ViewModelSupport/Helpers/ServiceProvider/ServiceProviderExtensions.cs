using Microsoft.Extensions.DependencyInjection;

namespace CelestialMapper.ViewModel;

public static class ServiceProviderExtensions
{

    public static T ResolveViewModel<T>(this IServiceProvider serviceProvider, string featureName)
        where T : IViewModel
    {
        var viewModel = serviceProvider.GetRequiredService<T>();

        ThrowWhenServiceIsNull(viewModel, typeof(T));

        viewModel!.Initialize(viewModel.GetViewModelConfigurator(featureName));

        return viewModel;
    }

    public static IViewModel ResolveViewModel(this IServiceProvider serviceProvider, Type vmType, string featureName)
    {
        var viewModel = serviceProvider.GetRequiredService(vmType) as IViewModel;

        ThrowWhenServiceIsNull(viewModel, vmType);

        viewModel!.Initialize(viewModel.GetViewModelConfigurator(featureName));

        return viewModel;
    }

    private static void ThrowWhenServiceIsNull(IViewModel? viewModel, Type type)
    {
        if (viewModel is null)
        {
            throw new InvalidOperationException($"Could not find service of {type.FullName} type");
        }
    }

}

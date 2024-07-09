using Microsoft.Extensions.DependencyInjection;

namespace CelestialMapper.ViewModel;

public static class ServiceProviderExtensions
{

    public static T ResolveViewModel<T>(this IServiceProvider serviceProvider, FeatureName featureName, Action<T>? postConfigure = null)
        where T : IViewModel
    {
        var viewModel = serviceProvider.GetRequiredService<T>();

        ThrowWhenServiceIsNull(viewModel, typeof(T));

        viewModel!.Initialize(viewModel.GetViewModelConfigurator(featureName));

        postConfigure?.Invoke(viewModel);

        return viewModel;
    }

    public static IViewModel ResolveViewModel(this IServiceProvider serviceProvider, Type vmType, FeatureName featureName, Action<IViewModel>? postConfigure = null)
    {
        var viewModel = serviceProvider.GetRequiredService(vmType) as IViewModel;

        ThrowWhenServiceIsNull(viewModel, vmType);

        viewModel!.Initialize(viewModel.GetViewModelConfigurator(featureName));

        postConfigure?.Invoke(viewModel);

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

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanArchitecture.Core.Converters;

public class CustomModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));
        if (context.Metadata.ModelType == typeof(string) && context.BindingInfo.BindingSource != BindingSource.Body)
            return new StringTrimmerBinder();
        return null;
    }
}

public class StringTrimmerBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext is null)
            throw new ArgumentNullException(nameof(bindingContext));
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;
        bindingContext.Result = ModelBindingResult.Success(valueProviderResult.FirstValue.Trim());
        return Task.CompletedTask;
    }
}
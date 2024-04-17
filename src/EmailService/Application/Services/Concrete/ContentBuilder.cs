namespace EmailWorkerService.Application.Services.Concrete;

public class ContentBuilder : IContentBuilder, IDisposable
{
    private readonly IResourceService _resourceService;
    private string _content;
    public ContentBuilder(IResourceService resourceService)
    {
        this._resourceService = resourceService;
        _content = string.Empty;
    }

    public string Build(EmailRequest model)
    {
        Type modelType = model.GetType();
        PropertyInfo[] properties = modelType.GetProperties();

        _content = _resourceService.GetContent(model.EmailType);

        foreach (PropertyInfo property in properties)
        {
            var excludedAttr = property.GetCustomAttribute<ExcludeAttribute>();
            var descriptionAttr = property.GetCustomAttribute<DescriptionAttribute>();
            var customElementAttr = property.GetCustomAttribute<CustomElementAttribute>();

            if (excludedAttr != null)
                continue;

            if (customElementAttr == null)
            {
                if (descriptionAttr != null)
                {
                    string parameterName = descriptionAttr.Description;
                    object propertyValue = property.GetValue(model);
                    _content = _content.Replace(parameterName, propertyValue == null ? string.Empty : propertyValue.ToString());
                }
            }
            else
            {
                var propertyInfos = property.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var propInfo in propertyInfos)
                {
                    descriptionAttr = propInfo.GetCustomAttribute<DescriptionAttribute>();

                    if (descriptionAttr != null)
                    {
                        string parameterName = descriptionAttr.Description;
                        object propertyValue = propInfo.GetValue(property.GetValue(model));
                        _content = _content.Replace(parameterName, propertyValue == null ? string.Empty : propertyValue.ToString());
                    }
                }
            }
        }

        return _content;
    }
    public IContentBuilder AddSubject(string subject)
    {
        _content = _content.Replace("#SUBJECT#", subject);
        return this;
    }

    public void Dispose()
    {
        _content = string.Empty;
    }
}

using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using System.ComponentModel;
using System.Reflection;
using static Clicco.Domain.Shared.Global;

namespace Clicco.EmailServiceAPI.Services
{
    public class TemplateParser : ITemplateParser
    {
        private readonly IContentBuilder templateFinder;
        public TemplateParser(IContentBuilder templateFinder)
        {
            this.templateFinder = templateFinder;
        }
        public string ToContent(EmailTemplateModel model)
        {
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();

            var htmlTemplate = templateFinder.GetContent(model.EmailType);

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
                        htmlTemplate = htmlTemplate.Replace(parameterName, propertyValue == null ? string.Empty : propertyValue.ToString());
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
                            htmlTemplate = htmlTemplate.Replace(parameterName, propertyValue == null ? string.Empty : propertyValue.ToString());
                        }
                    }
                }
            }

            return htmlTemplate;
        }
    }
}

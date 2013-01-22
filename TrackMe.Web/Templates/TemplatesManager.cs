using System.Collections.Generic;
using System.Web;
using System.IO;

namespace TrackMe.Web.Templates
{
    public class TemplatesManager : ITemplatesManager
    {
        private readonly Dictionary<string,string> templates = new Dictionary<string, string>();

        public string GetTemplate(string templateName)
        {
            if (templates.ContainsKey(templateName))
            {
                return templates[templateName];
            }
            else
            {
                string path = "~/Templates/" + templateName;
                string template = ReadFile(HttpContext.Current.Server.MapPath(path));
                templates.Add(templateName,template);
                return template;
            }
        }

        private string ReadFile(string filePath)
        {
            string text;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                text = streamReader.ReadToEnd();
            }
            return text;
        }
    }

    public interface ITemplatesManager
    {
        string GetTemplate(string templateName);
    }
}
using System.Text;

using Logger.Models.Contracts;

namespace Logger.Models.Layouts
{
    public class XmlLayout : ILayout
    {
        public string Format => GetDataFormat();

        private string GetDataFormat()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .AppendLine("<log>")
                    .AppendLine("<date>{0}</date>")
                    .AppendLine("<level>{1}</level>")
                    .AppendLine("<message>{2}</message>")
                .AppendLine("</log>");

            return stringBuilder.ToString().TrimEnd();
        }
    }
}

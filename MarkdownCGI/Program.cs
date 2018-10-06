using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkdownCGI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Content-type: text/html\n");
            Console.Write("X-Powered-By: MarkdownCGI 1.0\n");
            Console.Write("\n"); // Close header
            Console.Write("<!DOCTYPE html><html><head></head><body>\n");
            StreamReader mdFile = new StreamReader(args[0], Encoding.GetEncoding("utf-8"));
            // string body = ScriptFile.ReadToEnd();
            // StreamReader mdFile = new StreamReader(@"C:\inetpub\wwwroot\cgi-test\test.md", Encoding.GetEncoding("utf-8"));
            string body = string.Empty;
            while (mdFile.Peek() >= 0)
            {
                string mdLine = mdFile.ReadLine();

                // Replase Head <hN>
                mdLine = Regex.Replace(mdLine, @"^#{1}\x20(.+)$", "<h1>$1</h1>");
                mdLine = Regex.Replace(mdLine, @"^#{2}\x20(.+)$", "<h2>$1</h2>");
                mdLine = Regex.Replace(mdLine, @"^#{3}\x20(.+)$", "<h3>$1</h3>");
                mdLine = Regex.Replace(mdLine, @"^#{4}\x20(.+)$", "<h4>$1</h4>");
                mdLine = Regex.Replace(mdLine, @"^#{5}\x20(.+)$", "<h5>$1</h5>");
                mdLine = Regex.Replace(mdLine, @"^#{6}\x20(.+)$", "<h6>$1</h6>");

                // Replase Break <br>
               mdLine = Regex.Replace(mdLine, @"\x20\x20", "<br>");

                // Replase Strong <strong>
                mdLine = Regex.Replace(mdLine, @"\x20\x2a\x2a(.+)\x2a\x2a\x20", "<strong>$1</strong>");
                mdLine = Regex.Replace(mdLine, @"\x20\x5f\x5f(.+)\x5f\x5f\x20", "<strong>$1</strong>");

                // Replase Head <hr>
                mdLine = Regex.Replace(mdLine, @"^(\x2a|\x2a\x20+){3,}|(\x20){3,}|(\x2d|\x2d\x20+){3,}|(\x5f|\x5f\x20+){3,}$", "<hr>");

                // Replase HyperLink <href>
                mdLine = Regex.Replace(mdLine, @"\[(.+)\]\((.+)\)", "<a href=\"$2\">$1</a>");

                body += mdLine + Environment.NewLine;
            }
            mdFile.Close();
            Console.Write(body);
            Console.Write("</body></html>");
            // System.Threading.Thread.Sleep(100000);
        }
    }
}

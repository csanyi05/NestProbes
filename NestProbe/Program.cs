using Nest;
using System;
using System.Collections.Generic;
using System.IO;

namespace NestProbe
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\Lenovo\eclipse-workspace-old\";
            var fileExtension = "*.java";
            List<Homework> homeworks = new List<Homework>();
            string[] directories = Directory.GetDirectories(path);
            for(int i = 0; i<directories.Length; i++)
            {
                string[] files = Directory.GetFiles(directories[i], fileExtension, SearchOption.AllDirectories);
                if(files.Length > 0)
                {
                    var homework = new Homework(directories[i]);
                    int lineNum = 0;
                    for (int j = 0; j < files.Length; j++)
                    {
                        string[] lines = System.IO.File.ReadAllLines(files[j]);
                        for (int k = 0; k < lines.Length; k++)
                        {
                            if (!String.IsNullOrWhiteSpace(lines[k]))
                            {
                                homework.Lines.Add(new Line(lineNum, lines[k]));
                                lineNum++;
                            }
                        }
                    }
                    homeworks.Add(homework);
                }
            }
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
    .DefaultIndex("homework1");
            var client = new ElasticClient(settings);
            foreach (var hw in homeworks)
            {
                client.IndexDocument(hw);
            }
        }
    }
}

using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestProbe
{
    public class QueryManager
    {
        ElasticClient client;
        List<Homework> homeworks;
        public QueryManager(ElasticClient client, List<Homework> homeworks)
        {
            this.client = client;
            this.homeworks = homeworks;
        }

        Dictionary<Homework, Dictionary<Homework, List<string>>> results = new Dictionary<Homework, Dictionary<Homework, List<string>>>();

        public void run()
        {
            foreach(var baseHW in homeworks)
            {
                var match = new Dictionary<Homework, List<string>>();
                foreach(var line in baseHW.Lines)
                {
                    var response = client.Search<Homework>(s => s
                .From(0)
                .Size(1000)
                .Query(q => q
                    .Match(m => m.Field(f => f.Lines).Query(line.Text)
            )));
                    foreach (var document in response.Documents)
                        if (match.TryGetValue(document, out List<string> matchedLines))
                            matchedLines.Add(line.Text);
                        else
                            match.Add(document, new List<string>() { line.Text });
                }
                results.Add(baseHW, match);
            }
            foreach (var baseHW in results)
            {
                Console.WriteLine(baseHW.Key.Id);
                Console.WriteLine("");
                foreach(var dictionaryHomeworks in baseHW.Value)
                        Console.WriteLine(dictionaryHomeworks.Key.Id +  "   " + (double)((double)dictionaryHomeworks.Value.Count/ (double)baseHW.Key.Lines.Count));
            }
        }

    }
}

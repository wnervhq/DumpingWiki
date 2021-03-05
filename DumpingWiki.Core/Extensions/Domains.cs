using DumpingWiki.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DumpingWiki.Extensions
{
    public static class Domains
    {
        public static List<DomainData> Get()
        {
            return new List<DomainData>()
            {
                new DomainData ( "Wikipedia","", ".wikipedia.org"),
                new DomainData ( "Wikibooks","b", ".wikibooks.org"),
                new DomainData ( "Wiktionary","d", ".wiktionary.org"),
                new DomainData ( "Wikimedia Foundation","f", ".wikimediafoundation.org"),
                new DomainData ( "Wikimedia","m", ".wikimedia.org"),
                new DomainData ( "Whitelisted Project","mv", ".m.${WHITELISTED_PROJECT}.org"),
                new DomainData ( "Wikinews","n", ".wikinews.org"),
                new DomainData ( "Wikiquote","q", ".wikiquote.org"),
                new DomainData ( "Wikisource", "s", ".wikisource.org"),
                new DomainData ( "Wikiversity","v", ".wikiversity.org"),
                new DomainData ( "Wikivoyage","voy", ".wikivoyage.org"),
                new DomainData ( "MediaWiki","w", ".mediawiki.org"),
                new DomainData ( "Wikidata","wd", ".wikidata.org")
            };
        }
    }
}

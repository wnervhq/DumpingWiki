using System;
using System.Collections.Generic;
using System.Text;

namespace DumpingWiki.Model
{
    public class DomainData
    {
        public DomainData(string name,string code,string trailingPart)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = code;
            TrailingPart = trailingPart;
        }

        private Guid Id { get; set; }
        private string Name{ get; set; }
        private string Code { get; set; }
        private string TrailingPart { get; set; }

        public Guid GetID()
        { 
            return Id;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetCode()
        {
            return Code;
        }
        public string GetTrailingPart()
        {
            return TrailingPart;
        }

    }
}

using ADASProject.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class GetModel
    {
        public GetModel()
        {
            Values = new Dictionary<string, Dictionary<string, string>>();
            StandartValues = new Dictionary<string, Dictionary<string, string>>();
            Comments = new List<Tuple<Comment, string>>();
        }

        public int Id { get; set; }
        public Dictionary<string, Dictionary<string, string>> Values { get; set; }
        public Dictionary<string, Dictionary<string, string>> StandartValues { get; set; }
        public List<Tuple<Comment, string>> Comments { get; set; }
        public byte[] Image { get; set; }
        public bool CanVote { get; set; }
    }
}

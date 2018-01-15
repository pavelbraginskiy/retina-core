﻿using System.Collections.Generic;
using System.Numerics;

namespace Retina.Replace.Nodes
{
    public class UpperCaseAll : Node
    {
        public Node Child { get; set; }

        public UpperCaseAll(Node child)
        {
            Child = child;
        }

        public override string GetString(string input, List<MatchContext> matches, List<MatchContext> separators, int index)
        {
            return Child.GetString(input, matches, separators, index).ToUpperInvariant();
        }

        public override BigInteger GetLength(string input, List<MatchContext> matches, List<MatchContext> separators, int index)
        {
            return Child.GetLength(input, matches, separators, index);
        }
    }
}

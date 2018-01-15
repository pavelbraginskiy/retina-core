﻿using Retina.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Retina.Stages
{
    class SplitStage : AtomicStage
    {
        public SplitStage(Config config, History history, List<string> patterns, List<string> substitutions, string separatorSubstitutionSource)
            : base(config, history, patterns, substitutions, separatorSubstitutionSource) { }

        protected override string Process(string input, TextWriter output)
        {
            var result = new List<string>();

            for (int i = 0; i < Separators.Count; ++i)
            {
                var sep = Separators[i].Match.Value;
                if (!Config.OmitEmpty || sep != "")
                    result.Add(sep);

                if (i == Matches.Count)
                    break;
                
                if (!Config.OmitGroups)
                {
                    var match = Matches[i].Match;
                    var groups = Matches[i].Regex.GetGroupNumbers().Skip(1);

                    foreach (var num in groups)
                        if (match.Groups[num].Success && Config.GetLimit(2).IsInRange(num - 1, groups.Last()))
                            result.Add(match.Groups[num].Value);
                }
            }
            
            result = result.Where((_, i) => Config.GetLimit(1).IsInRange(i, result.Count)).ToList();

            if (Config.Random && result.Count > 0)
            {
                var chosenResult = result[Random.RNG.Next(result.Count)];
                result = new List<string>();
                result.Add(chosenResult);
            }

            if (Config.Reverse)
                result.Reverse();
            
            return Config.FormatAsList(result);
        }
    }
}

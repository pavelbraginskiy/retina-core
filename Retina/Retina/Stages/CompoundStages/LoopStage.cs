﻿using Retina.Configuration;
using Retina.Replace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Retina.Stages
{
    public class LoopStage : Stage
    {
        public Stage ChildStage { get; set; }
        private History History;

        public LoopStage(Config config, History history, Stage childStage)
            : base(config)
        {
            History = history;
            ChildStage = childStage;
        }

        public override string Execute(string input, TextWriter output)
        {
            string result = input;

            if (Config.Random)
            {
                while (true)
                {
                    if (Random.RNG.Next(2) == 0)
                        break;
                    result = ChildStage.Execute(result, output).ToString();
                }
            }
            else if (Config.RegexParam != null)
            {
                while (Config.RegexParam.Match(result).Success ^ Config.Reverse)
                    result = ChildStage.Execute(result, output).ToString();
            }
            else
            {
                bool limitedIterations = false;
                int iterationCount = 0;
                if (Config.StringParam != null)
                {
                    limitedIterations = true;

                    var replacer = new Replacer(Config.StringParam, History);

                    // A bit awkward, but we need to set up a match against the whole input
                    // along with the resulting separators and everything, because that's
                    // what the Replacer eats.
                    var regex = new Regex(@"\A(?s:.*)\z");
                    var matches = new List<MatchContext>();
                    matches.Add(new MatchContext(regex.Match(input), regex, replacer));
                    var separators = new List<MatchContext>();
                    var startRegex = new Regex(@"\A");
                    separators.Add(new MatchContext(startRegex.Match(input), startRegex, replacer));
                    var endRegex = new Regex(@"\z");
                    separators.Add(new MatchContext(endRegex.Match(input), endRegex, replacer));

                    string countString = replacer.Process(input, matches, separators, 0);

                    var countRegex = new Regex(@"-?\d+");
                    var countMatch = countRegex.Match(countString);
                    
                    iterationCount = countMatch.Success ? int.Parse(countMatch.Value) : 0;
                }
                else if (Config.GetLimitCount() > 0)
                {
                    limitedIterations = true;

                    iterationCount = Config.GetLimit(0).End;
                }

                if (limitedIterations && iterationCount < 0)
                {
                    for (int i = 0; i < -iterationCount; ++i)
                        result = ChildStage.Execute(result, output).ToString();
                }
                else
                {
                    int i = 0;
                    string lastResult;
                    do
                    {
                        if (limitedIterations && i >= iterationCount)
                            break;
                        ++i;
                        lastResult = result;
                        result = ChildStage.Execute(lastResult, output).ToString();
                    } while (lastResult != result);
                }
            }

            return result;
        }
    }
}
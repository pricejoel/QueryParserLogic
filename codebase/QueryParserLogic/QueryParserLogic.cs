using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QueryParserLogic
{
    public class SqlServerKeywords
    {
        internal const string SELECT = "select";
        internal const string FROM = "from";
    }

    public class QueryParserLogic
    {
        const string SELECT_COLUMNS_REGEX = @"select(?<fields>.+)from";
        const string JOINS_REGEX = @"(inner |outer |left |right ){0,2}join.+?on";
        const string WHERE_COLUMNS_REGEX = @"where.+?(group by|order by|\Z)";
        public static string ParseQuery(string input)
        {
            //ParseSelectColumns(input);
            return ParseJoins(input);
        }

        private static string ParseJoins(string input)
        {
            StringBuilder retVal = new StringBuilder();
            MatchCollection mc = Regex.Matches(input, JOINS_REGEX, 
                RegexOptions.IgnoreCase |
                RegexOptions.Singleline |
                RegexOptions.Multiline 
                );
            foreach (Match m in mc)
            {
                if (m.Success)
                {
                    Group g = m.Groups[0];
                    retVal.AppendLine(g.Value);
                }
            }
            return retVal.ToString();
        }

        internal static string ParseSelectColumns(string input)
        {
            StringBuilder retVal = new StringBuilder();
            Match m = Regex.Match(input, SELECT_COLUMNS_REGEX, 
                RegexOptions.IgnoreCase |
                RegexOptions.Singleline |
                RegexOptions.Multiline 
                );
            if (m.Success)
            {
                string fieldLines = m.Groups["fields"].Value;
                string[] lines = fieldLines.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


                retVal.AppendLine(SqlServerKeywords.SELECT);
                foreach (string line in lines)
                {
                    retVal.AppendLine("\t"+line.Trim());
                }
                retVal.AppendLine(SqlServerKeywords.FROM);
            }

            return retVal.ToString();
        }
    }
}

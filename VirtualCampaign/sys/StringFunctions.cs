using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using VirtualCampaign.net;

namespace VirtualCampaign.sys {
    static class StringFunctions {
        private static List<string> foundCategories = new List<string>();

        public static string resolveToHTML(string inStr) {
            string outStr = inStr;
            foundCategories.Clear();
            MatchEvaluator eval = new MatchEvaluator(formatMatcher);
            outStr = Regex.Replace(outStr, "(\\[\\[)(.*?)(\\]\\])", eval);
            outStr = Regex.Replace(outStr, "(\\{\\{)(.*?)(\\}\\})", eval, RegexOptions.Singleline);
            
            outStr = Regex.Replace(outStr, "(```)(.*?)(```)", eval);
            outStr = Regex.Replace(outStr, "(``)(.*?)(``)", eval);
            outStr = Regex.Replace(outStr, "(===)(.*?)(===)", eval);
            outStr = Regex.Replace(outStr, "(==)(.*?)(==)", eval);

            outStr = outStr.Replace(">\r", ">");
            outStr = outStr.Replace(">\n", ">");
            outStr = outStr.Replace("\n", "<br>");
            outStr = outStr.Replace(Environment.NewLine, "<br>");

            if(foundCategories.Count > 0) {
                string catString = "Categories:";
                for(int i = 0; i < foundCategories.Count; ++i) {
                    if (i > 0) catString += ",";
                    catString += " <a href=\"syscat:" + foundCategories[i] + "\">" + foundCategories[i] + "</a>";
                }
                outStr += "<br><br><div class=\"toolbar\" style=\"height: 20px; text-align:left; font-size:.8em; margin-bottom:5px; background-color:#eeeeee;\">" + catString + "</div>";
            }

            return outStr;
        }

        public static string stripSystemTags(string inStr) {
            string outStr = inStr;
            MatchEvaluator eval = new MatchEvaluator(stripMatcher);
            outStr = Regex.Replace(outStr, "(\\[\\[)(.*?)(\\]\\])", eval);
            outStr = Regex.Replace(outStr, "(\\{\\{)(.*?)(\\}\\})", eval, RegexOptions.Singleline);
            outStr = Regex.Replace(outStr, "(==)(.*?)(==)", eval);

            return outStr;
        }

        private static string stripMatcher(Match match) {
            string type = match.Groups[1].Value;
            string s = match.Groups[2].Value;
            if (type.Equals("{{")) {
                return "";
            } else if (type.Equals("[[")) {
                if (s.Contains('|')) {
                    string[] split = s.Split('|');
                    if (split.Length >= 2) {
                        return split[1];
                    }
                } else {
                    return s;
                }
            } else if(type.Equals("==")) {
                return "";
            }
            return match.Groups[2].Value;
        }
        
        private static string formatMatcher(Match match) {
            string type = match.Groups[1].Value;
            string s = match.Groups[2].Value;
            if(type.Equals("{{")) {
                if (s.IndexOf("float-right|", StringComparison.CurrentCultureIgnoreCase) == 0) {
                    string content = s.Substring("float-right|".Length);
                    return "<div style=\"float:right; width:25%; margin:5px; padding:3px 3px 3px 3px; border:2px solid; background-color:#eeeeee; font-size:0.875em; \">" + content + "</div>";
                } else if (s.IndexOf("float-left|", StringComparison.CurrentCultureIgnoreCase) == 0) {
                    string content = s.Substring("float-left|".Length);
                    return "<div style=\"float:left; width:25%; margin:5px; padding:3px 3px 3px 3px; border:2px solid; background-color:#eeeeee; font-size:0.875em; \">" + content + "</div>";
                } else if (s.IndexOf("span|", StringComparison.CurrentCultureIgnoreCase) == 0) {
                    string content = s.Substring("span|".Length);
                    return "<div style=\"width:95%; margin:auto; padding:3px 3px 3px 3px; border:2px solid; background-color:#eeeeee; font-size:0.875em; \">" + content + "</div>";
                } else if(s.IndexOf("category|", StringComparison.CurrentCultureIgnoreCase) == 0) {
                    foundCategories.Add(s.Substring("category|".Length));
                    return "";
                } else if(s.IndexOf("list|", StringComparison.CurrentCultureIgnoreCase) == 0) {
                    string result = "";
                    string content = s.Substring("list|".Length);
                    content = SQLManager.filterForSQL(content);
                    List<Dictionary<string, object>> d = SQLManager.runImmediateQuery("vc_articles", 
                        new string[] { "title", "creator", "whitelist_protected", "whitelist" }, 
                        "`description` LIKE '%{{category|" + SQLManager.filterForSQL(content) + "}}%' ORDER BY `title` ASC");
                    char leadLetter = ' ';
                    char lastLead = ' ';
                    bool categorize = d.Count > 15;
                    foreach(Dictionary<string, object> entry in d) {
                        bool auth = false;
                        if(entry["whitelist_protected"].ToString() == "1") {
                            if (entry["creator"].ToString() == UserManager.currentUser.userID.ToString()) {
                                auth = true;
                            } else {
                                if(WhitelistHandler.getCurrentUserPermissionLevel(entry["whitelist"].ToString()) > 0) {
                                    auth = true;
                                }
                            }
                        } else {
                            auth = true;
                        }
                        if(auth) {
                            if(categorize) {
                                leadLetter = entry["title"].ToString().ToUpper().ToCharArray()[0];
                                if (leadLetter != lastLead) {
                                    if (result != "") result += "<br>";
                                    result += "<b>" + leadLetter + "</b><br>";
                                }
                                lastLead = leadLetter;
                            }
                            result += "&ensp;&#8226; <u><a href = \"" + entry["title"].ToString() + "\">" + entry["title"].ToString() + "</a></u></br>";
                        }
                    }
                    return result;
                } else if(s.IndexOf("recent|", StringComparison.CurrentCultureIgnoreCase) == 0) {
                    string result = "";
                    string content = s.Substring("recent|".Length);
                    content = SQLManager.filterForSQL(content);
                    int recentCount = int.TryParse(content, out recentCount) ? recentCount : 5;
                    List<Dictionary<string, object>> d = SQLManager.runImmediateQuery("vc_articles",
                        new string[] { "title", "creator", "editor", "edit_time", "whitelist_protected", "whitelist" },
                        "`whitelist_protected` = '0' ORDER BY `edit_time` DESC LIMIT " + recentCount);
                    List<long> unidUsers = new List<long>();

                    foreach (Dictionary<string, object> entry in d) {
                        if(long.TryParse(entry["editor"].ToString(), out long eID)) {
                            if(!unidUsers.Contains(eID)) {
                                if(!UserManager.isUserKnown(eID)) {
                                    unidUsers.Add(eID);
                                }
                            }
                        }
                    }

                    if(unidUsers.Count > 0) {
                        UserManager.identifyUsers(unidUsers.ToArray<long>());
                    }

                    foreach (Dictionary<string, object> entry in d) {
                        bool auth = false;
                        if (entry["whitelist_protected"].ToString() == "1") {
                            if (entry["creator"].ToString() == UserManager.currentUser.userID.ToString()) {
                                auth = true;
                            } else {
                                if (WhitelistHandler.getCurrentUserPermissionLevel(entry["whitelist"].ToString()) > 0) {
                                    auth = true;
                                }
                            }
                        } else {
                            auth = true;
                        }
                        if (auth) {
                            string udn = long.TryParse(entry["editor"].ToString(), out long l) ? UserManager.getUserDisplayName(l) : "Unknown";
                            result += "<u><a href = \"" + entry["title"].ToString() + "\">" + entry["title"].ToString() + "</a></u> by " + udn + " on " + entry["edit_time"] + "</br>";
                        }
                    }
                    return result;
                }
            } else if (type.Equals("[[")) {
                if (s.Contains('|')) {
                    string[] split = s.Split('|');
                    if (split.Length >= 2) {
                        return "<u><a href=\"" + split[0] + "\">" + split[1] + "</a></u>";
                    }
                } else {
                    return "<u><a href=\"" + s + "\">" + s + "</a></u>";
                }
                return "";
            } else if(type.Equals("```")) {
                return "<b>" + s + "</b>";
            } else if(type.Equals("``")) {
                return "<i>" + s + "</i>";
            } else if(type.Equals("===")) {
                return "<h3>" + s + "</h3>";
            } else if(type.Equals("==")) {
                return "<h2>" + s + "</h2><hr>";
            }
            return s;
        }

        public static List<string> findCategories(string inString) {
            List<string> result = new List<string>();

            return result;
        }

        public static string ReadTag(string input, string tag) {
            if (input == null) return "";
            MatchCollection mc = Regex.Matches(input, "(\\[" + tag + "\\]|\\[/" + tag + "\\])");
            int count = 0;
            int dex = 0;
            string found = "";
            foreach (Match m in mc) {

                if (m.Groups[1].ToString().Equals("[" + tag + "]")) {
                    if (count == 0) {
                        dex = m.Index + tag.Length + 2;
                    }
                    ++count;
                } else if (m.Groups[1].ToString().Equals("[/" + tag + "]")) {
                    --count;
                    if (count == 0) {
                        found = input.Substring(dex, m.Index - dex);
                        return found;
                    }
                }
            }
            return "";
        }

        public static string[] ReadTagMulti(string input, string tag) {
            List<string> results = new List<string>();
            MatchCollection mc = Regex.Matches(input, "(\\[" + tag + "\\]|\\[/" + tag + "\\])");
            int count = 0;
            int dex = 0;
            string found = "";
            foreach(Match m in mc) {
                
                if(m.Groups[1].ToString().Equals("[" + tag + "]")) {
                    if(count == 0) {
                        dex = m.Index + tag.Length + 2;
                    }
                    ++count;
                } else if(m.Groups[1].ToString().Equals("[/" + tag + "]")) {
                    --count;
                    if(count == 0) {
                        found = input.Substring(dex, m.Index - dex);
                        results.Add(found);
                    }
                }
            }
            return results.ToArray();
        }

        public static string[] TagFinder(string input) {
            List<string> results = new List<string>();
            if(input == null) {
                Console.Out.WriteLine("Null input in TagFinder");
                return results.ToArray();
            }
            MatchCollection mc = Regex.Matches(input, "(\\[)(.*?)(\\])");
            int count = 0;
            string tag = null;
            foreach (Match m in mc) {
                if(tag == null) {
                    if(m.Groups[2].ToString().Length > 0 && !m.Groups[2].ToString().Contains('/')) {
                        tag = m.Groups[2].ToString();
                        count = 1;
                        continue;
                    }
                } else {
                    if(m.Groups[2].ToString().Equals(tag)) {
                        ++count;
                    } else if(m.Groups[2].ToString().Equals("/" + tag)) {
                        --count;
                    }
                    if(count == 0) {
                        if(!String.IsNullOrEmpty(tag)) {
                            if(!results.Contains(tag)) {
                                results.Add(tag);
                            }
                        }
                        tag = null;
                    }
                }

                /*
                tag = m.Groups[2].ToString();
                tag = tag.Trim('/');
                if(!String.IsNullOrEmpty(tag)) {
                    if(!results.Contains(tag)) {
                        results.Add(tag);
                    }
                }
                */
            }

            return results.ToArray();
        }

        public static int resolveCost(string input, string costType) {
            int result = 0;
            string s = Regex.Replace(input.ToLower(), @"\s+", "");
            Match m = Regex.Match(s, "(\\d*)" + Regex.Replace(costType.ToLower(), @"\s+", "") + "");
            if(m.Groups.Count >= 2) {
                result = (int.TryParse(m.Groups[1].ToString(), out int i) ? i : 0);
            }
            return result;
        }

        public static string simplifyDiceString(string input) {
            return simplifyDiceString(input, false).Replace('―', '-');
        }

        private static string simplifyDiceString(string input, bool nested) {
            if (input == null) return "";
            string result = Regex.Replace(input, @"\s+", "").ToLower();
            result = result.Replace('\\', '/');
            result = result.Replace('*', 'x');
            char[] breakdown = result.ToCharArray();
            bool valid = true;

            // Differentiate between - (negative number) and ― (subtraction) where necessary
            for (int i = 0; i < breakdown.Length; ++i) {
                if (breakdown[i] == '-') {
                    if (i > 0) {
                        switch (breakdown[i - 1]) {
                            case 'x':
                            case '/':
                            case '+':
                            case '^':
                            case '(':
                                break;
                            default:
                                breakdown[i] = '―';
                                break;
                        }
                    }
                }
            }
            result = new string(breakdown);

            // Find parentheses pairs, resolve them recursively - DONE!
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < result.Length; ++i) {
                if (result[i] == '(') {
                    stack.Push(i);
                } else if (result[i] == ')') {
                    int start = stack.Any() ? stack.Pop() : -1;
                    if (start == -1) {
                        valid = false;
                        break;
                    }
                    if (!stack.Any()) {

                        string sub = result.Substring(start + 1, i - start - 1);
                        string replace = simplifyDiceString(sub, true);
                        result = result.Replace("(" + sub + ")", replace);
                        i = start + replace.Length;
                    }
                }
            }
            if (!valid) return input;

            List<object> sequences = new List<object>();
            int last = 0;
            for (int i = 0; i < result.Length; ++i) {
                switch (result[i]) {
                    case '+':
                    case '―':
                    case 'x':
                    case '/':
                    case '^':
                        string newSequence = result.Substring(last, i - last);
                        sequences.Add(newSequence);
                        sequences.Add(new Operation(result.Substring(i, 1)));
                        last = i + 1;
                        break;
                }
                if (i == result.Length - 1 && last < result.Length) {
                    sequences.Add(result.Substring(last));
                }
            }

            Equation eq;
            bool simplified = true;

            for (int i = 1; i < sequences.Count - 1; ++i) {
                if(sequences[i] is Operation) {
                    if(sequences[i].ToString() == "^") {
                        eq = new Equation(sequences[i - 1].ToString(), sequences[i + 1].ToString(), new Operation(Operation.Type.EXPONENTIATION));
                        if (eq.resolvable) {
                            sequences[i - 1] = eq.solution;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                        } else {
                            sequences[i - 1] = eq;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                            simplified = false;
                        }
                    }
                }
            }
            if (simplified) {
                for (int i = 1; i < sequences.Count - 1; ++i) {
                    if (sequences[i].ToString() == "x") {
                        eq = new Equation(sequences[i - 1].ToString(), sequences[i + 1].ToString(), new Operation(Operation.Type.MULTIPLY));
                        if (eq.resolvable) {
                            sequences[i - 1] = eq.solution;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                        } else {
                            sequences[i - 1] = eq;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                            simplified = false;
                        }
                    } else if (sequences[i].ToString() == "/") {
                        eq = new Equation(sequences[i - 1].ToString(), sequences[i + 1].ToString(), new Operation(Operation.Type.DIVIDE));
                        if (eq.resolvable) {
                            sequences[i - 1] = eq.solution;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                        } else {
                            sequences[i - 1] = eq;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                            simplified = false;
                        }
                    }
                }
            }

            if (simplified) {

                List<object> newSequences = new List<object>();
                List<DieRoll> rolls = new List<DieRoll>();
                double numTotal = 0;
                List<KeyValuePair<string, string>> others = new List<KeyValuePair<string, string>>();
                for (int i = 0; i < sequences.Count; ++i) {
                    if (!(sequences[i] is Operation)) {
                        if (double.TryParse(sequences[i].ToString(), out double d)) {
                            if (i == 0) {
                                numTotal += d;
                            } else {
                                if (sequences[i - 1].ToString() == "+") {
                                    numTotal += d;
                                } else if (sequences[i - 1].ToString() == "―") {
                                    numTotal -= d;
                                }
                            }
                        } else if (DieRoll.TryParse(sequences[i].ToString(), out DieRoll dr)) {
                            bool found = false;
                            foreach (DieRoll r in rolls) {
                                if (r.die == dr.die) {
                                    if (i == 0) {
                                        r.count += dr.count;
                                    } else if (sequences[i - 1].ToString() == "+") {
                                        r.count += dr.count;
                                    } else if (sequences[i - 1].ToString() == "―") {
                                        r.count -= dr.count;
                                    }
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) {
                                if (i == 0) {
                                    rolls.Add(dr);
                                } else if (sequences[i - 1].ToString() == "+") {
                                    rolls.Add(dr);
                                } else if (sequences[i - 1].ToString() == "―") {
                                    rolls.Add(new DieRoll(dr.count * -1, dr.die));
                                }
                            }
                        } else {
                            if (i == 0) {
                                others.Add(new KeyValuePair<string, string>("+", sequences[i].ToString()));
                            } else if (sequences[i - 1].ToString() == "+" || sequences[i - 1].ToString() == "―") {
                                others.Add(new KeyValuePair<string, string>(sequences[i - 1].ToString(), sequences[i].ToString()));
                            }
                        }
                    }
                }

                for (int i = 0; i < others.Count; ++i) {
                    if ((i == 0 && others[i].Key == "―") || i > 0) newSequences.Add(others[i].Key);
                    newSequences.Add(others[i].Value);
                    simplified = false;
                }

                for (int i = 0; i < rolls.Count; ++i) {
                    if (newSequences.Count > 0 && rolls[i].count >= 0) newSequences.Add("+");
                    newSequences.Add(rolls[i].ToString());
                    simplified = false;
                }

                if (numTotal > 0) {
                    if (newSequences.Count > 0) newSequences.Add("+");
                    newSequences.Add(numTotal.ToString());
                } else if (numTotal < 0) {
                    newSequences.Add("―");
                    newSequences.Add(Math.Abs(numTotal).ToString());
                }

                if (newSequences.Count == 0) newSequences.Add("0");

                sequences = newSequences;



                /*
                for (int i = 1; i < sequences.Count - 1; ++i) {
                    if (sequences[i] == "+") {
                        eq = new Equation(sequences[i - 1], sequences[i + 1], Equation.Operation.ADD);
                        if (eq.valid) {
                            sequences[i - 1] = eq.solution;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                        }
                    } else if (sequences[i] == "―") {
                        eq = new Equation(sequences[i - 1], sequences[i + 1], Equation.Operation.SUBTRACT);
                        if (eq.valid) {
                            sequences[i - 1] = eq.solution;
                            sequences.RemoveAt(i);
                            sequences.RemoveAt(i);
                            i--;
                        }
                    }
                }
                */


            }

            result = "";
            if (nested && !simplified) result += "(";
            foreach (object o in sequences) {
                result += o.ToString();
            }
            if (nested && !simplified) result += ")";
            return result;
        }
    }

    class Equation {
        public enum Resolution { INVALID, NUMERIC, ROLL }
        private string _l, _r, _s;
        private Operation _op;
        private Resolution _resolution;
        public string l { get { return _l; } set { SetLeft(value); } }
        public string r { get { return _r; } set { SetRight(value); } }
        public Operation op { get { return _op; } set { SetOperation(value); } }
        public string solution { get { return _s; } }
        public Resolution resolution { get { return _resolution; } }
        public bool resolvable { get { return resolution == Resolution.NUMERIC || resolution == Resolution.ROLL; } }

        public Equation() {
            l = "";
            r = "";
            op = new Operation(Operation.Type.ADD);
            _resolution = Evaluate(out _s);
        }

        public Equation(string left, string right, Operation operation) {
            this.l = left;
            this.r = right;
            this.op = operation;
            _resolution = Evaluate(out _s);
        }

        public void SetLeft(string val) {
            _l = val;
            _resolution = Evaluate(out _s);
        }

        public void SetRight(string val) {
            _r = val;
            _resolution = Evaluate(out _s);
        }

        public void SetOperation(Operation operation) {
            _op = operation;
            _resolution = Evaluate(out _s);
        }

        public Resolution Evaluate(out string result) {
            result = "0";
            double left, right;

            if(DieRoll.TryParse(l, out DieRoll dl)) {
                if(DieRoll.TryParse(r, out DieRoll dr)) {
                    if(dl.die == dr.die) {
                        switch(op.type) {
                            case Operation.Type.EXPONENTIATION:
                                result = (int)Math.Pow(dl.count, dr.count) + "d" + dl.die;
                                break;
                            case Operation.Type.MULTIPLY:
                                result = (dl.count * dr.count) + "d" + dl.die;
                                break;
                            case Operation.Type.DIVIDE:
                                result = (dl.count / dr.count) + "d" + dl.die;
                                break;
                            case Operation.Type.ADD:
                                result = (dl.count + dr.count) + "d" + dl.die;
                                break;
                            case Operation.Type.SUBTRACT:
                                result = (dl.count - dr.count) + "d" + dl.die;
                                break;
                        }
                        return Resolution.ROLL;
                    } else {
                        return Resolution.INVALID;
                    }
                }
            }

            if(double.TryParse(l, out double d)) {
                left = d;
            } else {
                result = "0";
                return Resolution.INVALID;
            }
            if(double.TryParse(r, out d)) {
                right = d;
            } else {
                result = "0";
                return Resolution.INVALID;
            }
            switch(op.type) {
                case Operation.Type.EXPONENTIATION:
                    result = Math.Pow(left, right).ToString();
                    break;
                case Operation.Type.MULTIPLY:
                    result = (left * right).ToString();
                    break;
                case Operation.Type.DIVIDE:
                    result = (left / right).ToString();
                    break;
                case Operation.Type.ADD:
                    result = (left + right).ToString();
                    break;
                case Operation.Type.SUBTRACT:
                    result = (left - right).ToString();
                    break;
            }

            return Resolution.NUMERIC;
        }

        public override string ToString() {
            string result = "";
            result += l;
            switch(op.type) {
                case Operation.Type.ADD:
                    result += "+";
                    break;
                case Operation.Type.DIVIDE:
                    result += "/";
                    break;
                case Operation.Type.EXPONENTIATION:
                    result += "^";
                    break;
                case Operation.Type.MULTIPLY:
                    result += "x";
                    break;
                case Operation.Type.SUBTRACT:
                    result += "―";
                    break;
            }
            result += r;
            return result;
        }
    }

    class Operation {
        public enum Type { EXPONENTIATION, MULTIPLY, DIVIDE, ADD, SUBTRACT };

        public Type type;

        public Operation(Type t) {
            type = t;
        }

        public Operation(string t) {
            if(t == "^") {
                type = Type.EXPONENTIATION;
            } else if(t == "x" || t == "*") {
                type = Type.MULTIPLY;
            } else if(t == "/" || t == "\\") {
                type = Type.DIVIDE;
            } else if(t == "+") {
                type = Type.ADD;
            } else if(t == "-" || t == "―") {
                type = Type.SUBTRACT;
            }
        }

        public override string ToString() {
            switch (type) {
                case Type.ADD:
                    return "+";
                case Type.DIVIDE:
                    return "/";
                case Type.EXPONENTIATION:
                    return "^";
                case Type.MULTIPLY:
                    return "x";
                case Type.SUBTRACT:
                    return "―";
            }
            return "";
        }
    }
}

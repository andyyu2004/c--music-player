using System.Collections.Generic;
using CMusicPlayer.Util.Extensions;

namespace CMusicPlayer.CLI.Lexing
{
    internal class Lexer
    {
        private readonly CommandLineTextInterface clti;
        private readonly List<Token> tokens = new List<Token>();
        private int i;
        private string xs = "";

        public Lexer(CommandLineTextInterface clti)
        {
            this.clti = clti;
        }

        // Lex: string -> List<Token>
        public List<Token> Lex(string s)
        {
            xs = s;
            try
            {
                Scan();
                tokens.Print();
                return tokens;
            }
            catch (LexError e)
            {
                clti.WriteError(e.Message);
                return new List<Token>();
            }
        }

        private void Scan()
        {
            i = 0;
            tokens.Clear();
            while (i < xs.Length)
            {
                var start = i;
                switch (xs[i])
                {
                    // i is incremented in loop, don't do it twice!
                    case ' ': break;
                    case '\"':
                    {
                        // Doesn't deal with spaces immediately after '\"'
                        if (i == xs.Length - 1)
                            throw new LexError("Unterminated string, expected closing '\"'");
                        // Use switch expression in c#8
                        var t = TokenType.Argument;
                        switch (xs[i + 1])
                        {
                            case '-':
                                t = TokenType.Flag;
                                break;
                            case '&':
                                t = TokenType.ArgFlag;
                                break;
                        }

                        AdvanceUntil('\"');
                        if (i == xs.Length - 1 && xs[i] != '\"')
                            throw new LexError("Unterminated string, expected closing '\"'");
                        AddToken(t, start + 1, i - 1);
                        break;
                    }

                    case '&':
                    {
                        if (i + 1 >= xs.Length || xs[i + 1] == ' ')
                            throw new LexError($"Flag identifier expected at '{xs[i]}");
                        AdvanceUntil(' ');
                        AddToken(TokenType.ArgFlag, start, i);
                        break;
                    }

                    case '-':
                    {
                        if (i + 1 >= xs.Length || xs[i + 1] == ' ')
                            throw new LexError($"Flag identifier expected at '{xs[i]}");
                        AdvanceUntil(' ');
                        AddToken(TokenType.Flag, start, i);
                        break;
                    }

                    default:
                    {
                        AdvanceUntil(' ');
                        AddToken(TokenType.Argument, start, i);
                        break;
                    }
                }

                i++;
            }
        }

        private void AdvanceUntil(char x)
        {
            while (i + 1 < xs.Length && xs[++i] != x)
            {
            }
        }

        private void AddToken(TokenType type, int start, int end)
        {
//            var substring = xs.Substring(start, xs.Length - end);
//            var substring = xs[start..(end + 1)].Trim();
            var substring = new string(xs.ToCharArray()[start..(end + 1)]).Trim();
            tokens.Add(new Token(substring, type));
        }
    }
}
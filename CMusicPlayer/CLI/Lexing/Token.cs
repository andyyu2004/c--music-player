namespace CMusicPlayer.CLI.Lexing
{
    internal struct Token
    {
        public string Lexeme { get; }
        public TokenType Type { get; }

        public Token(string lexeme, TokenType type)
        {
            Lexeme = lexeme;
            Type = type;
        }

        public bool Equals(Token other)
        {
            return string.Equals(Lexeme, other.Lexeme) && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (obj is Token t) return Equals(t);
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Lexeme != null ? Lexeme.GetHashCode() : 0) * 397) ^ (int) Type;
            }
        }


        public override string ToString()
        {
            return $"{Lexeme} ({Type})";
        }
    }
}
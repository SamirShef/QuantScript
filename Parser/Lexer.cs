using System.Text;

public class Lexer
{
    private static string OPERATOR_CHARS = "+-*/(){}[]=<>!&|,;.";
    private static Dictionary<string, TokenType> OPERATORS = InitOperators();
    private static Dictionary<string, TokenType> InitOperators()
    {
        OPERATORS = new Dictionary<string, TokenType>();
        OPERATORS.Add("+", TokenType.Plus);
        OPERATORS.Add("-", TokenType.Minus);
        OPERATORS.Add("*", TokenType.Multiple);
        OPERATORS.Add("/", TokenType.Division);
        OPERATORS.Add("(", TokenType.LParen);
        OPERATORS.Add(")", TokenType.RParen);
        OPERATORS.Add("{", TokenType.LBrace);
        OPERATORS.Add("}", TokenType.RBrace);
        OPERATORS.Add("[", TokenType.LBracket);
        OPERATORS.Add("]", TokenType.RBracket);
        OPERATORS.Add("=", TokenType.EQ);
        OPERATORS.Add("<", TokenType.LT);
        OPERATORS.Add(">", TokenType.GT);

        OPERATORS.Add("!", TokenType.EXCL);
        OPERATORS.Add("&", TokenType.AMP);
        OPERATORS.Add("|", TokenType.BAR);

        OPERATORS.Add("==", TokenType.EQEQ);
        OPERATORS.Add("!=", TokenType.EXCLEQ);
        OPERATORS.Add("<=", TokenType.LTEQ);
        OPERATORS.Add(">=", TokenType.GTEQ);

        OPERATORS.Add("&&", TokenType.AMPAMP);
        OPERATORS.Add("||", TokenType.BARBAR);
        OPERATORS.Add(",", TokenType.COMMA);
        OPERATORS.Add(";", TokenType.SEMIPOINT);
        OPERATORS.Add(".", TokenType.DOT);

        return OPERATORS;
    }

    private string input;
    private int pos;
    private int length;
    private List<Token> tokens;

    public Lexer (string input)
    {
        this.input = input;
        length = input.Length;
        pos = 0;
        tokens = new List<Token>();
    }

    public List<Token> Tokenize ()
    {
        while (pos < length)
        {
            char current = input[pos];

            if (char.IsDigit(current)) TokenizeNumbers();
            else if (char.IsLetter(current)) TokenizeWord();
            else if (current == '"') TokenizeText();
            else if (OPERATOR_CHARS.IndexOf(current) != -1) TokenizeOperators();
            else Next();
        }
        return tokens;
    }

    private void AddToken(TokenType type, string text)
    {
        tokens.Add(new Token(type, text));
    }

    private void AddToken(TokenType type)
    {
        tokens.Add(new Token(type, ""));
    }

    private char Next()
    {
        pos++;
        return Peek(0);
    }

    private char Peek(int relativePos)
    {
        int position = this.pos + relativePos;
        if (position >= length) return '\0';
        return input[position];
    }

    private void TokenizeNumbers()
    {
        string value = "";
        char current = Peek(0);
        while (true)
        {
            if (current == '.')
            {
                if (value.Contains('.')) throw new Exception("Invalid float number");
            }
            else if (!char.IsDigit(current)) break;
            value += current;
            current = Next();
        }
        AddToken(TokenType.Number, value);
    }

    private void TokenizeWord()
    {
        StringBuilder buffer = new StringBuilder();
        char current = Peek(0);
        while (true)
        {
            if (!char.IsLetterOrDigit(current) && current != '_' && current != '$') break;
            buffer.Append(current);
            current = Next();
        }
        string word = buffer.ToString();
        switch (word)
        {
            case "class":
                AddToken(TokenType.CLASS);
                break;
            case "new":
                AddToken(TokenType.NEW);
                break;
            case "this":
                AddToken(TokenType.THIS);
                break;
            case "var":
                AddToken(TokenType.VAR);
                break;
            case "using":
                AddToken(TokenType.USING);
                break;
            case "if":
                AddToken(TokenType.IF);
                break;
            case "else":
                AddToken(TokenType.ELSE);
                break;
            case "while":
                AddToken(TokenType.WHILE);
                break;
            case "for":
                AddToken(TokenType.FOR);
                break;
            case "do":
                AddToken(TokenType.DO);
                break;
            case "break":
                AddToken(TokenType.BREAK);
                break;
            case "continue":
                AddToken(TokenType.CONTINUE);
                break;
            case "void":
                AddToken(TokenType.VOID);
                break;
            case "return":
                AddToken(TokenType.RETURN);
                break;
            default:
                AddToken(TokenType.Word, word);
                break;
        }
    }

    private void TokenizeText()
    {
        Next();
        StringBuilder buffer = new StringBuilder();
        char current = Peek(0);
        while (true)
        {
            if (current == '\\')
            {
                current = Next();
                switch (current)
                {
                    case '"':
                        current = Next();
                        buffer.Append('"');
                        continue;
                    case 'n':
                        current = Next();
                        buffer.Append('\n');
                        continue;
                    case 't':
                        current = Next();
                        buffer.Append('\t');
                        continue;
                }
                buffer.Append('\\');
                continue;
            }
            if (current == '"') break;
            buffer.Append(current);
            current = Next();
        }
        Next();
        AddToken(TokenType.Text, buffer.ToString());
    }

    private void TokenizeOperators()
    {
        char current = Peek(0);
        if (current == '/')
        {
            if (Peek(1) == '/')
            {
                Next();
                Next();
                TokenizeComment();
                return;
            }
            else if (Peek(1) == '*')
            {
                Next();
                Next();
                TokenizeMultilineComment();
                return;
            }
        }

        StringBuilder buffer = new StringBuilder();
        while (true)
        {
            string text = buffer.ToString();
            if (!OPERATORS.ContainsKey(text + current) && text != string.Empty)
            {
                AddToken(OPERATORS[text]);
                return;
            }
            buffer.Append(current);
            current = Next();
        }
    }

    private void TokenizeComment()
    {
        char current = Peek(0);
        while ("\r\n\0".IndexOf(current) == -1)
        {
            current = Next();
        }
    }

    private void TokenizeMultilineComment()
    {
        char current = Peek(0);
        while (true)
        {
            if (current == '\0') throw new Exception("Missing closing tag");
            if (current == '*' && Peek(1) == '/') break;
            current = Next();
        }
        Next();
        Next();
    }
}
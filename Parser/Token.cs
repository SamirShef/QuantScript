public enum TokenType
{
    Number,
    Word,
    Text,
    EQ,
    EQEQ,
    EXCL,
    EXCLEQ,

    VAR,
    USING,
    CLASS,
    NEW,
    THIS,
    DOT,
    IF,
    ELSE,
    WHILE,
    FOR,
    DO,
    BREAK,
    CONTINUE,
    FUNC,
    RETURN,

    Plus,
    PlusEQ,
    Minus,
    MinusEQ,
    Multiple,
    Division,
    LParen,
    RParen,
    LBrace,
    RBrace,
    LBracket,
    RBracket,
    LT,
    LTEQ,
    GT,
    GTEQ,

    BAR,
    BARBAR,
    AMP,
    AMPAMP,
    COMMA, // ,
    SEMIPOINT, // ;

    Unknown,
    EOF
}

public class Token
{
    public TokenType type;
    public string value;

    public Token (TokenType type, string value)
    {
        this.type = type;
        this.value = value;
    }

    public new TokenType GetType()
    {
        return type;
    }

    public void SetType(TokenType type)
    {
        this.type = type;
    }

    public string GetValue()
    {
        return value;
    }

    public void SetValue(string value)
    {
        this.value = value;
    }
}
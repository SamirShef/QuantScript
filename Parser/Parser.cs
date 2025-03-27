using System.Reflection.Metadata.Ecma335;

public class Parser
{
    private Token EOF = new Token(TokenType.EOF, "");
    private List<Token> tokens;
    private int pos;
    private int count;

    public Parser (List<Token> tokens)
    {
        this.tokens = tokens;
        count = tokens.Count;
    }

    public Statement Parse()
    {
        BlockStatement result = new BlockStatement();
        while (Match(TokenType.USING))
        {
            result.Add(ParseUsingDirective());
        }   
        while (!Match(TokenType.EOF))
        {
            result.Add(Statement());
        }

        return result;
    }

    private UsingDirective ParseUsingDirective()
    {
        string Namespace = Consume(TokenType.Word).GetValue();
        LibraryLoader libraryLoader = new LibraryLoader();
        libraryLoader.Load(Namespace);
    
        return new UsingDirective(Namespace);
    }

    private BlockStatement Block()
    {
        BlockStatement block = new BlockStatement();
        Consume(TokenType.LBrace);
        while (!Match(TokenType.RBrace))
        {
            block.Add(Statement());
        }

        return block;
    }

    private Statement StatementORBlock()
    {
        if (Get(0).GetType() == TokenType.LBrace) return Block();
        else return Statement();
    }

    private Statement Statement()
    {
        if (Match(TokenType.CLASS))
        {
            return ClassDeclaration();
        }
        if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.LParen)
        {
            return new FunctionStatement(Function());
        }
        if (Match(TokenType.IF) && Match(TokenType.LParen))
        {
            return IF_ELSE();
        }
        if (Match(TokenType.WHILE) && Match(TokenType.LParen))
        {
            return WhileStatement();
        }
        if (Match(TokenType.DO))
        {
            return DoWhileStatement();
        }
        if (Match(TokenType.BREAK))
        {
            return new BreakStatement();
        }
        if (Match(TokenType.CONTINUE))
        {
            return new ContinueStatement();
        }
        if (Match(TokenType.RETURN))
        {
            return new ReturnStatement(Expression());
        }
        if (Match(TokenType.FOR) && Match(TokenType.LParen))
        {
            return ForStatement();
        }
        if (Match(TokenType.FUNC))
        {
            return FunctionDefine();
        }

        return AssignmentStatement();
    }

    private BlockStatement ClassBlock()
    {
        BlockStatement block = new BlockStatement();
        Consume(TokenType.LBrace);
        while (!Match(TokenType.RBrace))
        {
            block.Add(ClassMember()); // Обрабатываем только члены класса
        }
        return block;
    }

    private Statement ClassDeclaration()
    {
        string name = Consume(TokenType.Word).GetValue();
        BlockStatement body = ClassBlock(); // Используем новый метод
        return new ClassDeclaration(name, body);
    }

    private Statement ClassMember()
    {
        if (Match(TokenType.VAR))
        {
            string name = Consume(TokenType.Word).GetValue();
            Expression initializer = null;
            if (Match(TokenType.EQ)) initializer = Expression();
            else throw new Exception("The value of the field has not been declared");
            return new FieldDeclaration(name, initializer);
        }
        else if (Match(TokenType.FUNC))
        {
            return MethodDeclaration(isVoid: true);
        }
        else if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.LParen)
        {
            return MethodDeclaration(isVoid: false);
        }
        throw new Exception($"Invalid class member: {Get(0).GetValue()}");
    }
    
    private Statement MethodDeclaration(bool isVoid)
    {
        string name = Consume(TokenType.Word).GetValue();
        Consume(TokenType.LParen);
        
        List<string> parameters = new List<string>();
        while (!Match(TokenType.RParen))
        {
            parameters.Add(Consume(TokenType.Word).GetValue());
            Match(TokenType.COMMA);
        }
        
        Statement body = StatementORBlock();
        return new MethodDeclaration(name, parameters, body, isVoid);
    }

    private Statement AssignmentStatement()
    {
        if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.EQ)
        {
            string variable = Consume(TokenType.Word).GetValue();
            Consume(TokenType.EQ);
            return new AssignmentStatement(variable, Expression());
        }
        if (Match(TokenType.VAR))
        {
            if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.EQ)
            {
                string variable = Consume(TokenType.Word).GetValue();
                Consume(TokenType.EQ);
                return new AssignmentStatement(variable, Expression());
            }
        }
        if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.DOT)
        {
            Expression target = new VariablesExpression(Consume(TokenType.Word).GetValue());
            while (Match(TokenType.DOT))
            {
                string member = Consume(TokenType.Word).GetValue();
                target = new MemberAccessExpression(target, member);
            }
            Consume(TokenType.EQ);
            return new MemberAssignmentStatement((MemberAccessExpression)target, Expression());
        }
        if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.LBracket)
        {
            string variable = Consume(TokenType.Word).GetValue();
            Consume(TokenType.LBracket);
            Expression index = Expression();
            Consume(TokenType.RBracket);
            Consume(TokenType.EQ);
            return new ArrayAssignmentStatement(variable, index, Expression());
        }
        if (Get(0).GetType() == TokenType.RParen)
        {
            pos++; // Пропустить RParen
            return new EmptyStatement();
        }
        
        throw new Exception($"Unknown statement: {Get(0).GetType()}");
    }

    private Statement IF_ELSE()
    {
        Expression condition = Expression();
        Consume(TokenType.RParen);
        Statement IF_Statement = StatementORBlock();
        Statement ELSE_Statement;
        if (Match(TokenType.ELSE)) ELSE_Statement = StatementORBlock();
        else ELSE_Statement = null;
        return new IF_Statement(condition, IF_Statement, ELSE_Statement);
    }

    private Statement WhileStatement()
    {
        Expression condition = Expression();
        Consume(TokenType.RParen);
        Statement statement = StatementORBlock();
        return new WhileStatement(condition, statement);
    }

    private Statement DoWhileStatement()
    {
        Statement statement = StatementORBlock();
        Consume(TokenType.WHILE);
        Consume(TokenType.LParen);
        Expression condition = Expression();
        Consume(TokenType.RParen);
        return new DoWhileStatement(condition, statement);
    }

    private Statement ForStatement()
    {
        Statement initialization = StatementORBlock();
        Consume(TokenType.SEMIPOINT);
        Expression termination = Expression();
        Consume(TokenType.SEMIPOINT);
        Statement increment = StatementORBlock();
        Consume(TokenType.RParen);
        Statement block = StatementORBlock();
        return new ForStatement(initialization, termination, increment, block);
    }

    private FunctionDefineStatement FunctionDefine()
    {
        string name = Consume(TokenType.Word).GetValue();
        Consume(TokenType.LParen);
        List<string> argsNames = new List<string>();
        while (!Match(TokenType.RParen))
        {
            argsNames.Add(Consume(TokenType.Word).GetValue());
            Match(TokenType.COMMA);
        }
        Statement body = StatementORBlock();

        return new FunctionDefineStatement(name, argsNames, body);
    }

    private FunctionalExpression Function()
    {
        string name = Consume(TokenType.Word).GetValue();
        Consume(TokenType.LParen);
        FunctionalExpression func = new FunctionalExpression(name);
        while (!Match(TokenType.RParen))
        {
            Expression expr = Expression();
            func.AddArgument(expr);
            Match(TokenType.COMMA);
        }

        return func;
    }

    private Expression Array()
    {
        Consume(TokenType.LBracket);
        List<Expression> elements = new List<Expression>();
        while (!Match(TokenType.RBracket))
        {
            Expression expr = Expression();
            elements.Add(expr);
            Match(TokenType.COMMA);
        }
        return new ArrayExpression(elements);
    }

    private Expression Element()
    {
        string variable = Consume(TokenType.Word).GetValue();
        Consume(TokenType.LBracket);
        Expression index = Expression();
        Consume(TokenType.RBracket);
        return new ArrayAccessExpression(variable, index);
    }

    private Expression Expression()
    {
        return LogicalOR();
    }

    private Expression LogicalOR()
    {
        Expression result = LogicalAND();

        while (true)
        {
            if (Match(TokenType.BARBAR))
            {
                result = new ConditionalExpression(ConditionalExpression.Operator.OR, result, LogicalAND());
                continue;
            }
            break;
        }

        return result;
    }

    private Expression LogicalAND()
    {
        Expression result = Ecuality();

        while (true)
        {
            if (Match(TokenType.AMPAMP))
            {
                result = new ConditionalExpression(ConditionalExpression.Operator.AND, result, Ecuality());
                continue;
            }
            break;
        }

        return result;
    }

    private Expression Ecuality()
    {
        Expression result = Conditional();
        
        if (Match(TokenType.EQEQ))
        {
            return new ConditionalExpression(ConditionalExpression.Operator.EQUALS, result, Conditional());
        }
        if (Match(TokenType.EXCLEQ))
        {
            return new ConditionalExpression(ConditionalExpression.Operator.NOT_EQUALS, result, Conditional());
        }

        return result;
    }

    private Expression Conditional()
    {
        Expression result = Additive();

        while (true)
        {
            if (Match(TokenType.LT))
            {
                result = new ConditionalExpression(ConditionalExpression.Operator.LT, result, Additive());
                continue;
            }
            if (Match(TokenType.LTEQ))
            {
                result = new ConditionalExpression(ConditionalExpression.Operator.LTEQ, result, Additive());
                continue;
            }
            if (Match(TokenType.GT))
            {
                result = new ConditionalExpression(ConditionalExpression.Operator.GT, result, Additive());
                continue;
            }
            if (Match(TokenType.GTEQ))
            {
                result = new ConditionalExpression(ConditionalExpression.Operator.GTEQ, result, Additive());
                continue;
            }
            break;
        }

        return result;
    }

    private Expression Additive()
    {
        Expression result = Multiplicative();

        while (true)
        {
            if (Match(TokenType.Plus))
            {
                result = new BinaryExpression('+', result, Multiplicative());
                continue;
            }
            if (Match(TokenType.Minus))
            {
                result = new BinaryExpression('-', result, Multiplicative());
                continue;
            }
            break;
        }

        return result;
    }

    private Expression Multiplicative()
    {
        Expression result = Unary();

        while (true)
        {
            if (Match(TokenType.Multiple))
            {
                result = new BinaryExpression('*', result, Unary());
                continue;
            }
            if (Match(TokenType.Division))
            {
                result = new BinaryExpression('/', result, Unary());
                continue;
            }
            break;
        }

        return result;
    }

    private Expression Unary()
    {
        if (Match(TokenType.Minus)) return new UnaryExpression('-', Primary());
        return Primary();
    }

    private Expression Primary()
    {
        Expression expr = PrimaryBase();
        
        while (Match(TokenType.DOT))
        {
            string memberName = Consume(TokenType.Word).GetValue();
            List<Expression> args = new List<Expression>();
            
            // Проверяем наличие аргументов вызова метода
            if (Match(TokenType.LParen))
            {
                while (!Match(TokenType.RParen))
                {
                    args.Add(Expression());
                    Match(TokenType.COMMA);
                }
            }
            
            expr = new MemberAccessExpression(expr, memberName, args);
        }
        
        return expr;
    }

    private Expression PrimaryBase()
    {
        Token current = Get(0);
        if (Match(TokenType.NEW))
        {
            return NewExpression();
        }
        if (Match(TokenType.Number))
        {
            return new ValueExpression(Convert.ToDouble(current.GetValue().Replace(".", ",")));
        }
        if (current.GetType() == TokenType.EQ)
        {
            throw new Exception("Unexpected '=' in expression");
        }
        if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.LParen) return Function();
        if (Get(0).GetType() == TokenType.Word && Get(1).GetType() == TokenType.LBracket) return Element();
        if (Get(0).GetType() == TokenType.LBracket) return Array();
        if (Match(TokenType.Word)) return new VariablesExpression(current.GetValue());
        if (Match(TokenType.Text)) return new ValueExpression(current.GetValue());
        if (Match(TokenType.LParen))
        {
            Expression result = Expression();
            Match(TokenType.RParen);
            return result;
        }
        throw new Exception($"Unknown expression {current.GetType()}");
    }

    private Expression NewExpression()
    {
        string className = Consume(TokenType.Word).GetValue();
        Consume(TokenType.LParen); // Ожидаем скобки (можно сделать опциональными)
        Consume(TokenType.RParen);
        return new ObjectCreationExpression(className);
    }

    private Token Consume(TokenType type)
    {
        Token current = Get(0);
        if (current.GetType() != type) throw new Exception($"Token \'{current.GetType()}\' does not match in \'{type}\'");
        pos++;
        return current;
    }

    private bool Match(TokenType type)
    {
        Token current = Get(0);
        if (current.GetType() != type) return false;
        pos++;
        return true;
    }

    private Token Get(int relativePos)
    {
        int position = this.pos + relativePos;
        if (position >= count) return EOF;
        return tokens[position];
    }
}
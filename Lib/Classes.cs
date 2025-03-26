public static class Classes
{
    private static Dictionary<string, ClassDeclaration> classes = new Dictionary<string, ClassDeclaration>();
    
    public static void Set(string name, ClassDeclaration classDecl)
    {
        classes[name] = classDecl;
    }
    
    public static ClassDeclaration Get(string name)
    {
        if (classes.TryGetValue(name, out var cls)) return cls;
        throw new Exception($"Class '{name}' not found");
    }
}
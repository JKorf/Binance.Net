using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Binance.Net.SourceGenerator
{
    [Generator]
    public class JsonModelGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<RecordDeclarationSyntax> recordAttributeDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsRecordSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetRecordTargetForGeneration(ctx));

            IncrementalValueProvider<(Compilation, ImmutableArray<RecordDeclarationSyntax>)> compilationAndRecords
                    = context.CompilationProvider.Combine(recordAttributeDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndRecords,
                (spc, source) => Execute(source.Item1, source.Item2, spc));


            IncrementalValuesProvider<EnumDeclarationSyntax> enumAttributeDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsEnumSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetEnumTargetForGeneration(ctx));

            IncrementalValueProvider<(Compilation, ImmutableArray<EnumDeclarationSyntax>)> compilationAndEnums
                    = context.CompilationProvider.Combine(enumAttributeDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndEnums,
                (spc, source) => Execute(source.Item1, source.Item2, spc));
        }


        public static bool IsEnumSyntaxTargetForGeneration(SyntaxNode syntaxNode)
        {
            return syntaxNode is EnumDeclarationSyntax classDeclarationSyntax &&
                classDeclarationSyntax.AttributeLists.Count > 0 &&
                classDeclarationSyntax.AttributeLists
                    .Any(al => al.Attributes
                        .Any(a => a.Name.ToString() == "SerializationModel"));
        }

        public static bool IsRecordSyntaxTargetForGeneration(SyntaxNode syntaxNode)
        {
            return syntaxNode is RecordDeclarationSyntax classDeclarationSyntax &&
                classDeclarationSyntax.AttributeLists.Count > 0 &&
                classDeclarationSyntax.AttributeLists
                    .Any(al => al.Attributes
                        .Any(a => a.Name.ToString() == "SerializationModel"));
        }

        public static RecordDeclarationSyntax GetRecordTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (RecordDeclarationSyntax)context.Node;
            return classDeclarationSyntax;
        }

        public static EnumDeclarationSyntax GetEnumTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (EnumDeclarationSyntax)context.Node;
            return classDeclarationSyntax;
        }

        public void Execute(Compilation compilation, ImmutableArray<RecordDeclarationSyntax> classes, SourceProductionContext context)
        {
            foreach (var classSyntax in classes)
            {
                // Converting the class to a semantic model to access much more meaningful data.
                var model = compilation.GetSemanticModel(classSyntax.SyntaxTree);
                // Parse to declared symbol, so you can access each part of code separately,
                // such as interfaces, methods, members, contructor parameters etc.
                var symbol = model.GetDeclaredSymbol(classSyntax);
                var className = symbol.Name;
                var classNamespace = symbol.ContainingNamespace?.ToDisplayString();
                var classAssembly = symbol.ContainingAssembly?.Name;

                if (symbol.GetMembers().Any(x => x.Name == className))
                    continue;

                var sourceCode = new StringBuilder();
                sourceCode.AppendLine("namespace Binance.Net.Converters");
                sourceCode.AppendLine("{");
                sourceCode.AppendLine("    internal partial class BinanceSourceGenerationAggregator");
                sourceCode.AppendLine("    {");
                sourceCode.AppendLine($"        public {classNamespace}.{className} {className}{Guid.NewGuid().ToString().Replace("-", "")} {{ get; set; }}");
                sourceCode.AppendLine("    }");
                sourceCode.AppendLine("}");

                context.AddSource(
                    $"BinanceSourceGenerationContext.{className}.g.cs",
                    SourceText.From(sourceCode.ToString(), Encoding.UTF8)
                );
            }
        }

        public void Execute(Compilation compilation, ImmutableArray<EnumDeclarationSyntax> classes, SourceProductionContext context)
        {
            foreach (var classSyntax in classes)
            {
                // Converting the class to a semantic model to access much more meaningful data.
                var model = compilation.GetSemanticModel(classSyntax.SyntaxTree);
                // Parse to declared symbol, so you can access each part of code separately,
                // such as interfaces, methods, members, contructor parameters etc.
                var symbol = model.GetDeclaredSymbol(classSyntax);
                var className = symbol.Name;
                var classNamespace = symbol.ContainingNamespace?.ToDisplayString();
                var classAssembly = symbol.ContainingAssembly?.Name;

                if (symbol.GetMembers().Any(x => x.Name == className))
                    continue;

                var sourceCode = new StringBuilder();
                sourceCode.AppendLine("namespace Binance.Net.Converters");
                sourceCode.AppendLine("{");
                sourceCode.AppendLine("    internal partial class BinanceSourceGenerationAggregator");
                sourceCode.AppendLine("    {");
                sourceCode.AppendLine($"        public {classNamespace}.{className} {className}{Guid.NewGuid().ToString().Replace("-", "")} {{ get; set; }}");
                sourceCode.AppendLine("    }");
                sourceCode.AppendLine("}");

                context.AddSource(
                    $"BinanceSourceGenerationContext.{className}.g.cs",
                    SourceText.From(sourceCode.ToString(), Encoding.UTF8)
                );
            }
        }
    }
}

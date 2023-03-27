using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ptarmigan.Utils.Roslyn
{
    public static class RoslynAnalysisUtils
    {
        public static IEnumerable<IMethodSymbol> GetMethods(this INamedTypeSymbol type)
            => type.GetMembers().OfType<IMethodSymbol>();

        public static IEnumerable<IMethodSymbol> GetExtensionMethods(this INamedTypeSymbol type)
            => type.GetMethods().Where(m => m.IsExtensionMethod);

        public static INamedTypeSymbol GetTypeByMetaDataName(this Compilation compilation, string name)
            => compilation.Compiler.GetTypeByMetadataName(name);

        public static IEnumerable<ISymbol> GetNamedSymbolDeclarations(this Compilation compilation, string name)
            => compilation.Compiler.GetSymbolsWithName(name);

        public static IEnumerable<INamespaceOrTypeSymbol> GetAllLinkedNamespacesAndTypes(this Compilation compilation)
            => compilation.Compiler.GlobalNamespace.GetAllNested();

        public static IEnumerable<INamespaceOrTypeSymbol> GetAllNested(this INamespaceOrTypeSymbol symbol)
        {
            yield return symbol;
            foreach (var child in symbol
                         .GetMembers()
                         .OfType<INamespaceOrTypeSymbol>()
                         .SelectMany(GetAllNested))
            {
                yield return child;
            }
        }

        public static IEnumerable<INamespaceSymbol> GetAllLinkedNamespaces(this Compilation compilation)
            => compilation.GetAllLinkedNamespacesAndTypes().OfType<INamespaceSymbol>();

        public static IEnumerable<ITypeSymbol> GetAllLinkedTypes(this Compilation compilation)
            => compilation.GetAllLinkedNamespacesAndTypes().OfType<ITypeSymbol>();

        public static IEnumerable<ICompilationUnitSyntax> GetCompilationUnits(this Compilation compilation)
            => compilation.SyntaxTrees.Select(st => st.GetRoot() as CompilationUnitSyntax);
        
        public static IEnumerable<(TypeDeclarationSyntax, INamedTypeSymbol)> GetTypeDeclarationsWithSymbols(
            this Compilation compilation)
        {
            foreach (var st in compilation.SyntaxTrees)
            {
                var model = compilation.Compiler.GetSemanticModel(st);
                foreach (var n in st.GetRoot().DescendantNodesAndSelf().OfType<TypeDeclarationSyntax>())
                {
                    yield return (n, model.GetDeclaredSymbol(n));
                }
            }
        }

        public static IEnumerable<ISymbol> GetAllSymbols(this Compilation compilation)
        {
            foreach (var st in compilation.SyntaxTrees)
            {
                var model = compilation.Compiler.GetSemanticModel(st);
                foreach (var node in st.GetRoot().DescendantNodesAndSelf())
                {
                    switch (node)
                    {
                        case StatementSyntax _:
                        case TypeDeclarationSyntax _:
                        case MemberDeclarationSyntax _:
                            continue;
                        default:
                            yield return model.GetSymbolInfo(node).Symbol;
                            break;
                    }
                }
            }
        }
    }
}
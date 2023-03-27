using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

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

        public static IEnumerable<ISymbol> GetDeclaredAndBaseMembers(this ITypeSymbol sym)
        {
            foreach (var m in sym.GetMembers())
                yield return m;
            if (sym.BaseType != null)
                foreach (var m in sym.BaseType.GetDeclaredAndBaseMembers())
                    yield return m;
        }

        public static IEnumerable<ITypeSymbol> GetFieldTypes(this ITypeSymbol sym)
            => sym.GetDeclaredAndBaseMembers().OfType<IFieldSymbol>().Select(f => f.Type);

        public static IEnumerable<ISymbol> GetExpressionAndTypeSymbols(this Compilation compilation)
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

        // https://stackoverflow.com/questions/69636558/determining-if-a-private-field-is-read-using-roslyn
        private static bool IsFieldRead(SyntaxNodeAnalysisContext context, IFieldSymbol fieldSymbol)
        {
            var classDeclarationSyntax = context.Node.Parent;
            while (!(classDeclarationSyntax is ClassDeclarationSyntax))
            {
                classDeclarationSyntax = classDeclarationSyntax.Parent;
                if (classDeclarationSyntax == null)
                {
                    throw new InvalidOperationException("You have somehow traversed up and out of the syntax tree when determining if a private member field is being read.");
                }
            }

            //get all methods in the class
            var methodsInClass = classDeclarationSyntax.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var method in methodsInClass)
            {
                //get all member references in those methods
                if (context.SemanticModel.GetOperation(method).Descendants().OfType<IMemberReferenceOperation>().Any(x => x.Member.Equals(fieldSymbol)))
                {
                    return true;
                }
            }

            return false;
        }

        // https://stackoverflow.com/questions/44142421/roslyn-check-if-field-declaration-has-been-assigned-to
        // https://stackoverflow.com/questions/64009302/roslyn-c-how-to-get-all-fields-and-properties-and-their-belonging-class-acce/64014939#64014939
        public static IEnumerable<IMemberReferenceOperation> GetMemberReferenceOperations(SemanticModel model,
            SyntaxNode node)
        {
            return model.GetOperation(node).Descendants().OfType<IMemberReferenceOperation>();
        }

        public static bool IsPublicMember(this ISymbol symbol)
            => symbol.DeclaringSyntaxReferences.Any(dsr => dsr.GetSyntax().IsPublicMember());

        public static bool IsPublicMember(this SyntaxNode node)
            => node is MemberDeclarationSyntax mds
               && mds.Modifiers.Any(st => st.Kind() == SyntaxKind.PublicKeyword);
    }
}
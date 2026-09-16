using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
var files = Directory.GetFiles(Path.Combine(root, "Assets"), "*.cs", SearchOption.AllDirectories);
int errors = 0;
foreach (var file in files)
{
    var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file), new CSharpParseOptions(LanguageVersion.CSharp9), file);
    foreach (var diagnostic in tree.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error)) { Console.WriteLine(diagnostic); errors++; }
}
Console.WriteLine($"C# syntax: {files.Length} files, {errors} errors. Unity API/type validation requires Unity Editor.");
return errors == 0 ? 0 : 1;

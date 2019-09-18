﻿using System.Collections;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace CommandLineTools
{
    public static class CommandLineOptions
    {
        public const string InFileHelpText = "The file in which to ";

        public const string OutFileHelpText = "If set will not overwrite the source file but write the result into the out file. Replaces eventually existing files with the same name";
    }

    public abstract class BaseOptions
    {
        [Option("verbose", Required = false, HelpText = "Print extra output to console")]
        public bool Verbose { get; set; }
        [Option("debugLog", Required = false, HelpText = "Turn on debug info")]
        public bool DebugLog { get; set; }
    }


    [Verb("inFileReplace", HelpText = "Replace every occurence of a string in a file with a new string")]
    public class InFileReplaceOptions : BaseOptions
    {
        [Option('f', "find", Required = true, HelpText = "The text to replace in the file")]
        public string TextToFind { get; set; }
        [Option('r', "replaceWith", Required = true, HelpText = "The text that replaces the text found")]
        public string ReplacementText { get; set; }
        [Option('n', "parseAsNewline", Required = false, HelpText = "Give a unique sequence of characters that will internally be replaced with the char sequence for 'newline'")]
        public string ParseAsNewline { get; set; }

        [Option('i', "in", Required = true, HelpText = CommandLineOptions.InFileHelpText + "find and replace text")]
        public string InputFile { get; set; }
        [Option('o', "out", Required = false, HelpText = CommandLineOptions.OutFileHelpText)]
        public string OutputFile { get; set; }
    }

    [Verb("removeLines", HelpText = "Remove all lines matching either at least one or all of the specified patterns")]
    public class RemoveLinesOptions : BaseOptions
    {
        [Option('p', "patterns", Required = true, HelpText = "The patterns to determine which lines are to be deleted. Write continuously separating with '#'. If Using the '#'-symbol inside a pattern is required, escape it in the pattern (e.g. '\\#'")]
        public string Patterns { get; set; }
        [Option('c', "conjunctive", Required = false, HelpText = "If set lines will only be removed if they match ALL the patterns")]
        public bool ConjunctivePatterns { get; set; }

        [Option('i', "in", Required = true, HelpText = CommandLineOptions.InFileHelpText + "remove lines")]
        public string InputFile { get; set; }
        [Option('o', "out", Required = false, HelpText = CommandLineOptions.OutFileHelpText)]
        public string OutputFile { get; set; }
    }

    [Verb("executeBatch", HelpText = "Execute a number of batch instructions providing a json file execution manual")]
    public class ExecuteBatchOptions : BaseOptions
    {
        [Option('i', "instructions", Required = true, HelpText = "Json file containing instructions which command to execute where. Example: [ { Location: \"C:\\Program Files\", Command: \"hello.bat\" }]")]
        public string InstructionFile { get; set; }
    }

    [Verb("statisticalTable", HelpText = "Create table over various dimensions")]
    public class StatisticalTableOptions : BaseOptions
    {
        [Option("--verbose", Required = false, HelpText = "Print additional info")]
        public bool Verbose { get; set; } = false;

        [Option('d', "database", Required = true, HelpText = "Path to the database file")]
        public string DatabaseFile { get; set; }

        [Option('t', "table", Required = true, HelpText = "Database table to query")]
        public IEnumerable<string> DatabaseTable { get; set; }

        [Option('o', "out", Required = true, HelpText = "Where to write the resulting table")]
        public string OutputFile { get; set; }

        [Option('f', "outputFormat", Required = false, HelpText = "Currently defaults to only available 'latex' (tabular)")]
        public string OutputFormat { get; set; }

        [Option('v', "value", Required = true, HelpText = "Name of the value column. Can be an expression")]
        public string Value { get; set; }

        [Option('m', "main", Required = true, HelpText = "The main trait to group by")]
        public string Main { get; set; }

        [Option("mainFont", Required = false, HelpText = "Special font to use for the main traits")]
        public string MainFont { get; set; }

        [Option('g', "mainGroup", Required = false, HelpText = "Value to group the Main values by")]
        public string MainGroup { get; set; }

        [Option("mainGroupSep", Required = false, HelpText = @"How to separate the main groups. Default is \hline")]
        public string MainGroupSeparator { get; set; }

        [Option('s', "secondaries", Required = false, HelpText = "The secondary traits to distinguish by. Default is all available")]
        public IEnumerable<string> Secondaries { get; set; }

        [Option("secondaryAliases", Required = false, HelpText = "Alias list for the secondary traits for displaying the trait name")]
        public IEnumerable<string> SecondaryAliases { get; set; }

        [Option("metric", Required = false, HelpText = "The metric to use on the data. Can be 'average', 'median', 'min', 'max'")]
        public string Metric { get; set; }

        [Option("printAbsoluteValues", Required = false, HelpText = "Print the absolute values in the columns instead of the relative ones")]
        public bool PrintAbsoluteValues { get; set; } = false;
    }

    [Verb("statisticalFunctions", HelpText = "calculates various different statistical functions on a set of data")]
    public class StatisticalFunctionsOptions : BaseOptions
    {
        [Option('d', "database", Required = true, HelpText = "Path to the database file")]
        public string DatabaseFile { get; set; }

        [Option('t', "table", Required = true, HelpText = "Database table to query")]
        public IEnumerable<string> DatabaseTable { get; set; }

        [Option('o', "out", Required = true, HelpText = "Database table to write to")]
        public string DatabaseTableOut { get; set; }

        [Option('v', "value", Required = true, HelpText = "Name of the value column. Can be an expression")]
        public string Value { get; set; }

        [Option('g', "groups", Required = false,
            HelpText = "The columns to group the values by. Default is one single group with all items")]
        public IEnumerable<string> Groups { get; set; }

        [Option('f', "functions", Required = false,
            HelpText = "Functions to apply to the data. Available are 'average', 'median', 'variance'.")]
        public IEnumerable<string> Functions { get; set; }

        [Usage]
        public static IEnumerable<Example> Examples =>
            new List<Example>
            {
                new Example(
                    "Calculate the average and median over the table 'stats' on database 'db.sqlite' grouping by 'sorter' where 'value / numberOfCycles' gives the value to get the median of, and write to table 'statsMedian'",
                    new StatisticalFunctionsOptions
                    {
                        DatabaseFile = "db.sqlite",
                        DatabaseTable = new[] { "stats" },
                        DatabaseTableOut = "statsMedian",
                        Value = "value / numberOfCycles",
                        Groups = new[] { "sorter" },
                        Functions = new[] { "average", "median" }
                    })
            };
    }

    [Verb("sqlPlotToolsHack", HelpText = "calculates various different statistical functions on a set of data")]
    public class SqlPlotToolsHackOptions : BaseOptions
    {
        [Option('i', "input", Required = true, HelpText = "Path to the input file")]
        public string InputFile { get; set; }

        [Option('d', "database", Required = true, HelpText = "Path to the database file")]
        public string DatabaseFile { get; set; }

        [Option('t', "table", Required = true, HelpText = "Database table to query")]
        public string DatabaseTable { get; set; }
    }

    [Verb("twoFish", HelpText = "encrypt or decrypt files with the twoFish algorithm")]
    public class TwoFishOptions : BaseOptions
    {
        [Option('i', "in", Required = true, HelpText = "File to encrypt or decrypt")]
        public string InputFile { get; set; }
        [Option('o', "out", Required = false, HelpText = "File to write to. If not given it is the input file with an encrypt/decrypt postfix")]
        public string OutputFile { get; set; }
        [Option("encrypt", Required = false, HelpText = "Encrypt the file")]
        public bool Encrypt { get; set; }
        [Option("decrypt", Required = false, HelpText = "Decrypt the file")]
        public bool Decrypt { get; set; }
        [Option('t', "temp", Required = false, HelpText = "Encrypt or Decrypt only until the service is shut down")]
        public bool Temporarily { get; set; }
        [Option("keep", Required = false, HelpText = "Do not keep original file for non-temporary encryption/decryption")]
        public bool Keep { get; set; }
    }
}
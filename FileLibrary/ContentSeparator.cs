﻿using System.Collections.ObjectModel;

namespace FileLibrary;

public class ContentSeparator
{
    private readonly FileEntity _file;
    private readonly IFormatProvider _format;

    public ContentSeparator(FileEntity file, IFormatProvider format)
    {
        _file = file;
        _format = format;

        SeparatedText = new ReadOnlyCollection<List<string>>(GetAllText());
        SeparatedSum = new ReadOnlyCollection<decimal>(GetSum(out var brokenList));
        SeparatedBroken = new ReadOnlyCollection<int>(brokenList);
    }

    public ReadOnlyCollection<decimal> SeparatedSum { get; }
    
    public ReadOnlyCollection<int> SeparatedBroken { get; }

    public ReadOnlyCollection<List<string>> SeparatedText { get; }

    private List<string?> GetAllLines()
    {
        var lines = new List<string?>();
        
        try
        {
            using (var sr = new StreamReader(_file.Path))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }        
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return lines;
    }

    private List<List<string>> GetAllText()
    {
        var result = new List<List<string>>();
        
        foreach (var line in GetAllLines())
        {
            result.Add(new List<string>(line!.Trim().Split(',').ToArray()));
        }

        return result;
    }

    private List<decimal> GetSum(out List<int> brokenList)
    {
        brokenList = new List<int>();

        var sumList = new List<decimal>();
        
        var converter = new ContentConverter();

        for (var i = 0; i < SeparatedText.Count; i++)
        {
            var sum = 0m;
            
            foreach (var number in SeparatedText[i])
            {
                if (converter.ToDecimal(number, _format, out var res))
                {
                    sum += res;
                }
                else
                {
                    brokenList.Add(i);
                    break;
                }
            }

            sumList.Add(sum);
        }

        return sumList;
    }
}
string aliceQuote = (
    "Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing " +
    "to do: once or twice she had peeped into the book her sister was reading, but it had no pictures " +
    "or conversations in it, ‘and what is the use of a book,’ thought Alice ‘without pictures or " +
    "conversation?’"
);

Console.WriteLine("What word do you wish to find:");
string searchTerm = Console.ReadLine();

string aliceLowerQuote = aliceQuote.ToLowerInvariant();
bool foundTerm = aliceLowerQuote.Contains(searchTerm.ToLowerInvariant());

Console.WriteLine($"It is {foundTerm} that the term {searchTerm} is within the quote.");

// Count how many times the term appears (non-overlapping matches)
int matchCount = 0;
if (searchTerm.Length > 0)
{
    int searchFrom = 0;
    while (true)
    {
        int next = aliceQuote.IndexOf(searchTerm.ToLowerInvariant(), searchFrom);
        if (next == -1)
        {
            break;
        }
        matchCount++;
        searchFrom = next + searchTerm.Length;
    }
}

Console.WriteLine($"The term {searchTerm} appears {matchCount} time(s).");


/*
DEBUGGING NOTES

Bug: the match count is wrong. Searching for "alice" prints that the term IS in the
quote (foundTerm is True) but then says it appears 0 time(s). Those two results
contradict each other, so one of them has to be wrong.

Debugging process:
1. Reproduce it. Run the program, enter "alice", and note the two conflicting lines of output.
2. Narrow it down. The first line (foundTerm) uses aliceLowerQuote and gives the right
   answer, so the problem is probably in the counting code that comes after it.
3. Set a breakpoint on the IndexOf line inside the while loop (line 23) and start the debugger.
4. Step through and watch the variables. next comes back as -1 on the very first pass,
   so the loop breaks immediately and matchCount stays 0.
5. Compare what IndexOf is searching with what it is looking for. The search term was
   converted to lowercase (searchTerm.ToLowerInvariant()), but the text being searched is
   aliceQuote, the original text, where the word starts with a capital "A". Lowercase
   "alice" is never found in it.
6. Fix: search aliceLowerQuote instead of aliceQuote on line 23, so both sides are lowercase:
   aliceLowerQuote.IndexOf(searchTerm.ToLowerInvariant(), searchFrom)
7. Re-run with "alice". Expected: True, and 2 time(s).
*/

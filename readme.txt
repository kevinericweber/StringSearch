StringSearch

Searches file systems and MS SQL Server entries for a specified search term.
Can recursively search file systems for a string (either text or regex).
Can search through jobs, table/view names and definitions, stored procedures, user logins, and indexes
in MS SQL Server

***** This is a work in progress. *****

It's a rewrite of an existing app I wrote that that performs searches within files and SQL entities
I decided to do a clean rewrite because:

  A) the GUI feedback mechanism was extremely suboptimal.  There was an artificial delay during updates
      that would greatly slow down more intensive seearches
  B) the searching didn't take advantage of multithreading.  This was particular terrible
      for file system searching, since the separate tasks it needed to perform didn't conflict at all.
  C) I wasn't happy with the architecture.  The two existing search types (MS SQL and File System)
      were pretty much hard-coded into the program, OOP was pretty much non-existent, and it definitely
      wasn't clean code (one function was actually 130 lines long... ugh...)

Anyway, like I mentioned, this program is currently a work-in-progress.  There are sections which could
use tweaking, and sections that aren't even programmed yet.  But the core of the program is usuable at
this point (which is good, because this app is incredibly useful!)
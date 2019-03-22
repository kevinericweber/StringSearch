SQL Injection is the #1 security vulnerability per OWASP in what seems like the millionth year running.
That's because programmers create apps that dynamically generate SQL statements the incorrect way, like:

    string sqlCmd = "Select Column from Table where Name = '" + searchName + "'";

This app dynamically generates SQL statements.  Worse, it generates some parts of those sql statements
in a way that the parser then *interpets*.

However, this app isn't currently vulnerable.  The elements that are user-sourced
are fed in as parameterized inputs.  Aka:

    string sqlCmd = "Select Column from Table where Name = @Name";

... which doesn't allow a malicious user to do anything.  Likewise, any
things which *are* parsed by the SQL engine:

    string sqlCmd = "Select Column from " + TableName + " where Name = @Name";

... are *not* coming from the user in any fashion (either directly or from a DB entry a user populated)

However, this brings me to the point of this text file:

DO NOT MODIFY THIS APP UNLESS YOU HAVE READ UP ON SQL INJECTION AND KNOW HOW TO AVOID IT.

Seriously, it would be easy to make tweaks to this app and expose a vulnerability.
Granted, it wouldn't be the end of the world, since this app already requires the user to
have an authorized SQL account - so they could simply connect in to the database and do
the same commands manually.

... but on the other hand, it doesn't take a whole lot of imagination to think of tweaks
that might make that moot ("Hey, what if we made this a service?", "Hey, what if we
hard coded an account that would let non-elevated users also search?", etc)

Plus, there's the principle of the thing.  Don't open up SQL Injection holes.  Regardless of the app.

(Anyway, the reason I'm using dynamic SQL like this is because I didn't want to have to require
a specific Stored Proc on every server this program could touch, nor can typical frameworks like
EntityFramework accomplish what this program needs to do)
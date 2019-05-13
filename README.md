# Scaffolding Stored Procedures with EFCore
This is a sample for the issue https://github.com/aspnet/EntityFrameworkCore/issues/15105 of the aspnet/EntityFrameworkCore project.
This code is based on the EFCore tutorial for 'Getting started with EF Core on .NET Framework with an Existing Database' (
https://docs.microsoft.com/en-us/ef/core/get-started/full-dotnet/existing-db). Please use this tutorial for migrating the database.


## Issue post
I have created a [small project](https://github.com/Lupin1st/efcore_scaffold_stored_procedures) with a simple use case for a procedure mapping that fits our needs. Functions could be generated in a similar way but should return an IQueryable object.

We currently use EF6 and will surely port to EF6 on .Net Core 3.0 when its available. But we still want to migrate new Modules to EFCore as well.

Concerning the way procedures should get generated from EFCore scaffold. For us the procedures and functions generated from the Entity framework 6 work well enough but using EFCore could probably make a meaningful difference for our project.

Our application has many hundred tables and Views and even more procedures and functions. Most of our legacy code uses only procedures to access data. For newer Modules however, we decided to use procedures only for performance reasons and query with EF6 queries. We want both, queries and procedure/function calls to get replaced by EF Core equivalents, at least for new modules. We do not make changes to the generated EF6 Models, so we use the designer only for selecting which objects should get imported.

The reasons we do not want to stick with EF6 are as following (ordered by priority):
- Larger EF6 contexts need a lot of time to instantiate and therefor our contexts must stay small, so we have many small EF6 contexts which are not very convenient to use.
- The edmx designer is quite slow and inconvenient to use. Instead a single command that generates all db objects as well as a faster one that updates just one db object would be perfect for us.
- Merge conflicts often force us to select all objects for a context again.
- The generated procedures are not async.
- The ObjectParameter is not generic.

For generating the procedures and functions, it would be enough for us to use the following SQL-Server objects.

**[sys].[dm_exec_describe_first_result_set_for_object]**:
For the result sets for each programmability object.
This has however some restrictions in comparison to the way EF6 searches for result sets which are no deal breaker for our project. Some restrictions are: Only available for SQLServer 2012+ and throws error for procedures that use TempTables directly or indirectly

**INFORMATION_SCHEMA.ROUTINES and INFORMATION_SCHEMA.PARAMETERS**:
For the method parameters for the programmability objects.


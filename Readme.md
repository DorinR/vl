### evolving db schema

1. Make changes to your DbContext class
2. (Optional) Add new property to your DbContext class if new entity was added
3. Generate new migration: `dotnet ef migrations add <migration_name>`
4. Apply migration: `dotnet ef database update`

### when not possible to automatically cast a column to another type

Re-write the migrations code to drop and recreate the column as the new type.
This will obviously cause loss of data.
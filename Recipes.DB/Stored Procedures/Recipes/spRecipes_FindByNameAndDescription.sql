CREATE PROCEDURE [dbo].[spRecipes_FindByNameAndDescription]
    @TextQuery NVARCHAR(50),
    @Page INT = 1
AS
BEGIN
    DECLARE @PageSize INT = 100;

SELECT DISTINCT [Id], [Name], [Description]
FROM [dbo].[Recipes]
WHERE [Deleted] = 0
  AND (LOWER([Name]) LIKE LOWER('%'+@TextQuery+'%') OR LOWER([Description]) LIKE LOWER('%'+@TextQuery+'%'))
ORDER BY [Id]
OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
-- Devido á base ser uma localdb tive que adaptar para LIKE. Deve ser substituído por um full text search.	
END
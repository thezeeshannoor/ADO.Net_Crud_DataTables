select * from Employes
WITH EmployeCTE AS (
    SELECT *, ROW_NUMBER() OVER (ORDER BY Age) AS ctr
    FROM Employes
)
select * from EmployeCTE;

alter procedure UpdateField
@val nvarchar(100),
@fieldName nvarchar(100),
@id INT
as
begin
DECLARE @sql NVARCHAR(MAX);

    SET @sql = 'UPDATE student SET ' + QUOTENAME(@fieldName) + ' = @val WHERE id = @id;';

    EXEC sp_executesql @sql, N'@val NVARCHAR(100), @id INT', @val, @id;

end

create procedure DeleteField
@id INT
as
begin
DELETE FROM student
WHERE id = @id;
end

alter PROCEDURE GetStudentS
   @pageNumber INT ,
     @pageSize INT ,
	 @sortColumn nvarchar(100) ,
	 @sortDirection nvarchar(100) ,
	 @SearchKeyword NVARCHAR(100) = NULL
	
AS

BEGIN
  
    SET NOCOUNT ON; 

  
    DECLARE @start INT = (@pageNumber - 1) * @pageSize + 1;
    DECLARE @end INT = @pageNumber * @pageSize;
    
    WITH stdCtr AS (
        SELECT *, ROW_NUMBER() OVER (ORDER BY 
		case when @sortColumn='fname' and @sortDirection='asc' then fname end asc,
		case when @sortColumn='fname' and @sortDirection='desc' then fname end desc
		
		) AS rowNum FROM student WHERE (@SearchKeyword IS NULL OR fname LIKE '%' + @SearchKeyword + '%')
    )
    SELECT * FROM stdCtr WHERE rowNum BETWEEN @start AND @end;
END
GO

exec GetStudentS;

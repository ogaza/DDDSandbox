USE [Blog]
GO

DECLARE @Id INT = NULL;
DECLARE @Text nvarchar(max);
DECLARE @Author nvarchar(200);
DECLARE @PostId int = 1;
DECLARE @Created datetime;
IF (@Id IS NULL)
--IF NOT EXISTS(SELECT * FROM Comments WHERE Id = @Id)
  BEGIN
  INSERT INTO
    Comments
    (
      Text,
      Author,
      PostId,
      Created
    )
  OUTPUT INSERTED.Id
  VALUES
  (
    @Text,
    @Author,
    @PostId,
    @Created
  )
  -- SELECT SCOPE_IDENTITY()
  END
ELSE
  UPDATE 
    Comments
  SET 
    Text = @Text,
    Author = @Author,
    PostId = @PostId,
    Created = @Created
  WHERE 
    Id = @Id
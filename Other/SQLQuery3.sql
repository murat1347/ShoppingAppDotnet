create PROC [dbo].[sp_DeleteValue] (
  @Id int output,
  @Name nvarchar(50),
  @Description nvarchar(1000),
  @Stock int,
  @Price int,
  @CategoryId int,
  @AddedDate datetime,
  @ModifiedDate datetime,
  @AddedBy nvarchar(50)
  )
  AS
BEGIN
    Delete from Products where Id=@Id

   
END
CREATE FUNCTION [dbo].[FEmail]
(
	@Email NVARCHAR(100)
	
)
RETURNS INT
AS
BEGIN
	DECLARE @UserId INT
	SET @UserId = (SELECT Id FROM Utilisateur WHERE Email = @Email)
	RETURN @UserId
END

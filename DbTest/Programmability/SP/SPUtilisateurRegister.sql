CREATE PROCEDURE [dbo].[SPUtilisateurRegister]
	@Nom NVARCHAR(80),
	@Prenom NVARCHAR(80),
	@Email NVARCHAR(100),
	@DateNAissance DATE,
	@Password VARCHAR(50)
AS

BEGIN
	
	DECLARE @PasswordHash BINARY(64), @SecurityStamp UNIQUEIDENTIFIER, @UserId INT, @BodyMessage NVARCHAR(150)
	SET @SecurityStamp = NEWID()
	SET @PasswordHash = dbo.FHasher(TRIM(@Password), @SecurityStamp)
	INSERT INTO [Utilisateur] (Nom, Prenom, Email, DateNaissance, PasswordHash, SecurityStamp)
	VALUES (TRIM(@Nom), TRIM(@Prenom), TRIM(@Email), @DateNaissance, @PasswordHash, @SecurityStamp)
	SET @UserId = dbo.FEmail(@Email)
	SET @BodyMessage = 'Your id is ' + CAST(@UserId AS NVARCHAR(MAX))
	EXEC msdb.dbo.sp_send_dbmail   
	@profile_name = 'Nicolas',  
	@recipients = @Email,  
	@body = @BodyMessage,
	@subject = 'Automated Success Message';
END